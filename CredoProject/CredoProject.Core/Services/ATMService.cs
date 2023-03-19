using System;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Validations;
using CredoProject.Core.Calculates;
using CredoProject.Core.Repositories;
using CredoProject.Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using CredoProject.Core.Models.Requests.Card;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CredoProject.Core.Services
{
    public interface IATMServices
    {
        Task<string> WithdrawManyFromCardAsync(WithdrawManyFromCardRequest request);
        Task<string> CardAuthorizationAsync(CardAutorizationRequest request);
        Task<string> GetCardBalanceAsync(CardAutorizationRequest request);
        Task<string> ChangeCardPinAsync(ChangeCardPinRequest request);
    }

    public class ATMService : IATMServices
    {
        private readonly ICardRepository _cardRepository;
        private readonly ICalculate _calculate;
        private readonly IValidate _validate;
        private readonly int _atmLimit24;

        public ATMService(IValidate validate, ICardRepository repository, ICalculate calculate)
        {
            _validate = validate;
            _calculate = calculate;
            _cardRepository = repository;
            _atmLimit24 = 2000;
        }

        // Tanxis gamotana baraTidan თანხის გამოტანა ბარათიდან
        public async Task<string> WithdrawManyFromCardAsync(WithdrawManyFromCardRequest request)
        {
            // ვნახულობთ ბარათს ბაზაში
            var card = await _cardRepository.GetCardByNumberAndPinAsync(request.CardNumber, request.PIN);
            if (card == null) return "Card Not valid, Contact bank manager";
            var str = await CheckCard(card);
            if (str != null) return str;
            // ვიღებს ანგარიშს რომელზეც მიბმულია ბარათი
            var account = card.AccountEntity;
            // ვნახულობთ რამდენი აქვს გამოტანილი ბოლო 24 საათში
            var getAccountLast24 = _cardRepository.withdraw24Db(card).Result;
            // ვგებულობთ კრუსს
            var rate = _cardRepository.CalculateRate(account.Currency, Currency.GEL).Result;
            // fee რაოდენობა ანგარიშის ვალუტით 
            var fee = request.WithdrawAmount * 2/100;
            // ვნახულობთ თუ აქვს საკმარისი თანხა
            if (request.WithdrawAmount+fee > account.Amount) return "You don't have enough money";
            // ვნახულობთ ლარში ხომ არ არის 2000 ლარზე მეტი გამოსატან თანხასთან ერთად
            if ((getAccountLast24+request.WithdrawAmount)*rate > _atmLimit24) return "You have limit 2000 gel in last 24 hours";
            // ანგარიშის თანხას ვაკლებთ გამოსატან თანხას და საკომისიოს
            account.Amount -= (request.WithdrawAmount + fee);
            var transaction1 = new TransactionEntity()
            {
                AccountEntityFrom = account,
                AccountEntityTo = account,
                AmountTransaction = request.WithdrawAmount,
                CreatedAt = DateTime.Now,
                TransType = "ATM",
                CurrencyFrom = account.Currency,
                CurrencyTo = account.Currency,
                // საკომისიოს ვინახავთ ლარებში
                Fee = fee*rate,
                CardId = card.CardEntityId,
                AccountFromId = account.AccountEntityId,
                AccountToId = account.AccountEntityId,
                ExecutionAt = DateTime.Now,
                CurrentRate = rate
            };
            await _cardRepository.AddTransactionInDb(transaction1);
            await _cardRepository.SaveChangesAsync();

            return $"Welcome! {account.CustomerEntity.FirstName.ToString()}, Your Card Balance is {account.Amount} {account.Currency}";
        }


        public async Task<string> CardAuthorizationAsync(CardAutorizationRequest request)
        {
            var card = await _cardRepository.GetCardByNumberAndPinAsync(request.CardNumber, request.PIN);
            if (card == null) return "Card Not valid. Contact bank Manager!";
            var str = await CheckCard(card);
            return str ?? $"Welcome! {card.AccountEntity.CustomerEntity.FirstName.ToString()}";
        }

        public async Task<string> ChangeCardPinAsync(ChangeCardPinRequest request)
        {
            var card = await _cardRepository.GetCardByNumberAndPinAsync(request.CardNumber, request.PIN);
            if (card == null) return "Invalid Card Credentials!";
            var str = await CheckCard(card);
            if( str == null)
            {
                card.PIN = request.newPIN;
                await _cardRepository.SaveChangesAsync();
            }
            return str ?? "Pin Changed";
        }

        public async Task<string> GetCardBalanceAsync(CardAutorizationRequest request)
        {
            var card = await _cardRepository.GetCardByNumberAndPinAsync(request.CardNumber, request.PIN);
            if (card == null) return "Card Not valid. Contact bank Manager!";
            var str = await CheckCard(card);
            var ac = card.AccountEntity;
            return str ?? $"Welcome! {ac.CustomerEntity.FirstName.ToString()}, Your Card Balance is {ac.Amount} {ac.Currency}";
        }

        public async Task<string?> CheckCard(CardEntity card)
        {
            if (card.Status == Status.Blocked) return "Card Blocked. Contact bank Manager!";
            if (card.Status == Status.Expired) return "Card date expered. Contact bank Manager!";
            if (DateTime.ParseExact(card.ExpiredDate, "MM-yyyy", CultureInfo.InvariantCulture) < DateTime.Now)
            {
                card.Status = Status.Expired;
                await _cardRepository.SaveChangesAsync();
                return "Card date expered. Contact bank Manager!";
            }
            return null;
        }
    }
}

