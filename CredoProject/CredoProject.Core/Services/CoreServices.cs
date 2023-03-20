using System;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Validations;
using CredoProject.Core.Repositories;
using CredoProject.Core.Models.Requests;
using CredoProject.Core.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using IbanNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CredoProject.Core.Models.Requests.Customer;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CredoProject.Core.Services
{
    public interface ICoreServices
    {
        Task<UserEntity> RegisterCustomerAsync(CreateCustomerRequest request);
        Task<AccountResponse> RegisterAccountAsync(CreateAccountRequest request);
        Task<CardEntity> RegisterCardAsync(CreateCardRequest request);
        Task<UserEntity> GetUserEntity(int id);
        Task<string> TransferMonnyInnerAsync(TransferRequest request, int id);

        Task<List<CustomerAccountsResponse>> GetUserAccounts(int id);
        Task<List<CardsResponse>> GetUserCardsAsync(int id);
    }

    public class CoreServices : ICoreServices
    {
        private readonly IBankRepository _bankRepository;
        private readonly IValidate _validate;
        private readonly ICardRepository _cardRepository;
        //private readonly UserManager<UserEntity> _userManager;

        public CoreServices(IValidate validate, IBankRepository repository, UserManager<UserEntity> userManager, ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
            _validate = validate;
            _bankRepository = repository;
            //_userManager = userManager;
        }

        // კარგი იქნება თუ შეეძლება თავისი აიბანებიდან ამოარჩიოს გასაგზავნის და მისაღებიც.
        public async Task<string> TransferMonnyInnerAsync(TransferRequest request, int id)
        {
            AccountEntity aFrom;
            AccountEntity aTo;
            //var id = 2;
            //List<string> checkAccounts = await _bankRepository.GetAccountsById(id);
            //if (!checkAccounts.Contains(request.AccountFrom.IBAN) || !checkAccounts.Contains(request.AccountTo.IBAN))
            try {
                aFrom = await _bankRepository.GetAccountByIBAN(request.AccountFrom);
                aTo = await _bankRepository.GetAccountByIBAN(request.AccountTo);
            }
            catch
            {
                return "IBAN is not Correct";
            }
            if (aFrom.CustomerEntityId != id) return "This is not account number!";
            var rate = _cardRepository.CalculateRate(aFrom.Currency, aTo.Currency).Result;
            var fee = 0m;
            var feeProcent = 0m;
            var amount = request.TransferAmount;
            if (request.TransType == TransType.Outer)
            {
                feeProcent = 1;
                fee = request.TransferAmount * feeProcent / 100 + 0.5m;
            }
            // ვნახულობთ თუ აქვს საკმარისი თანხა
            if (request.TransferAmount + fee > aFrom.Amount) return "You don't have enough money";
            // ანგარიშის თანხას ვაკლებთ გამოსატან თანხას და საკომისიოს
            aFrom.Amount -= (request.TransferAmount + fee);
            aTo.Amount += request.TransferAmount * rate;
            var transaction1 = new TransactionEntity()
            {
                AccountEntityFrom = aFrom,
                AccountEntityTo = aTo,
                AmountTransaction = request.TransferAmount,
                CreatedAt = DateTime.Now,
                TransType = request.TransType,
                CurrencyFrom = aFrom.Currency,
                CurrencyTo = aTo.Currency,
                // საკომისიოს ვინახავთ ლარებში
                Fee = fee * rate,
                AccountFromId = aFrom.AccountEntityId,
                AccountToId = aTo.AccountEntityId,
                ExecutionAt = DateTime.Now,
                CurrentRate = rate,
            };
            await _cardRepository.AddTransactionInDb(transaction1);
            await _cardRepository.SaveChangesAsync();

            return $"Welcome! Your Account ({aFrom.IBAN}) Balance is {aFrom.Amount} {aFrom.Currency}";
        }

        public async Task<List<CustomerAccountsResponse>> GetUserAccounts(int id)
        {
            var userAccounts = await _bankRepository
                .GetUserAccountsFromDbAsync(id);
            return userAccounts;
        }

        public async Task<List<CardsResponse>> GetUserCardsAsync(int id)
        {
            var userCards = await _bankRepository.GetUserCardsFromDbAsync(id);
            List<CardsResponse> result = new List<CardsResponse>() { };
            foreach (var card in userCards)
            {
                var expDate = DateTime.ParseExact(card.ExpiredDate, "MM-yyyy", CultureInfo.InvariantCulture);
                var cardResponse = new CardsResponse()
                {
                    CardAmount = card.AccountEntity.Amount,
                    Currency = card.AccountEntity.Currency,
                    CardNumber = card.CardNumber,
                    ExpiredDate = card.ExpiredDate,
                    Status = card.Status,
                    //info = "Welcome"
                };
                if (expDate < DateTime.Now)
                    cardResponse.info = $"Your card has expired";
                else if (expDate < DateTime.Now.AddMonths(3))
                    cardResponse.info = $"Your card expires in {(expDate - DateTime.Now).Days} a days";
                result.Add(cardResponse);
            }
            return result;
        }

        public async Task<UserEntity> RegisterCustomerAsync(CreateCustomerRequest request)
        {
            var customer = new UserEntity()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Email = request.Email,
                PersonalNumber = request.PersonalNumber
            };
            
            await _bankRepository.AddCustomerToDbAsync(customer);
            await _bankRepository.SaveChangesAsync();
            return customer;
        }

        public async Task<AccountResponse> RegisterAccountAsync(CreateAccountRequest request)
        {
            var result = _validate.ValidateAccount(request.IBAN);
            var ac = new AccountEntity()
            {
                IBAN = request.IBAN,
                Amount = request.Amount,
                Currency = request.Currency,
                CustomerEntityId = request.CustomerId,
                CreateAt = DateTime.Now,
            };
            await _bankRepository.AddAccountToDbAsync(ac);
            await _bankRepository.SaveChangesAsync();
            var response = new AccountResponse()
            {
                AccountEntityId = ac.AccountEntityId,
                IBAN = ac.IBAN,
                Amount = ac.Amount,
                CardEntities = ac.CardEntities,
                Currency = ac.Currency,
                UserId = ac.CustomerEntityId,
                CreateAt = ac.CreateAt.ToString("d")
            };
                return response;
            }

        public async Task<CardEntity> RegisterCardAsync(CreateCardRequest request)
        {
            var account = _bankRepository.GetAccountById(request.AccountEntityId).Result;
            var customer = await _bankRepository.GetUserByIdAsync(account.CustomerEntityId);
            var card = new CardEntity()
            {
                AccountEntityId = request.AccountEntityId,
                CardNumber = request.CardNumber,
                PIN = request.PIN,
                CVV = request.CVV,
                OwnerName = customer.FirstName,
                OwnerLastName = customer.LastName,
                RegistrationDate = DateTime.Now,
                ExpiredDate = DateTime.Now.AddYears(3).ToString("MM-yyyy"),
                Status = Status.Active
            };
            await _bankRepository.AddCardToDbAsync(card);
            await _bankRepository.SaveChangesAsync();
            return card;
        }

        public async Task<UserEntity> GetUserEntity(int id)
        {
            var customer = await _bankRepository.GetUserByIdAsync(id);
            return customer;
        }
    }
}
