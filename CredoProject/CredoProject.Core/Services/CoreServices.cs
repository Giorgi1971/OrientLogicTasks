using System;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Validations;
using CredoProject.Core.Repositories;
using CredoProject.Core.Models.Requests;

namespace CredoProject.Core.Services
{
    public interface ICoreServices
    {
        Task<UserEntity> RegisterCustomerAsync(CreateCustomerRequest request);
        Task<AccountEntity> RegisterAccountAsync(CreateAccountRequest request);
        Task<CardEntity> RegisterCardAsync(CreateCardRequest request);
    }

    public class CoreServices : ICoreServices
    {
        private readonly IBankRepository _bankRepository;
        private readonly IValidate _validate;

        public CoreServices(IValidate validate, IBankRepository repository)
        {
            _validate = validate;
            _bankRepository = repository;
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

        public async Task<AccountEntity> RegisterAccountAsync(CreateAccountRequest request)
        {
            var account = _validate.ValidateAccount(request);
            await _bankRepository.AddAccountToDbAsync(account);
            await _bankRepository.SaveChangesAsync();
            return account;
        }

        public async Task<CardEntity> RegisterCardAsync(CreateCardRequest request)
        {
            var account = _bankRepository.GetAccountById(request.AccountEntityId).Result;
            var customer = _bankRepository.GetCustomerById(account.CustomerEntityId).Result;
            var card = new CardEntity()
            {
                AccountEntityId = request.AccountEntityId,
                CardNumber = request.CardNumber,
                PIN = request.PIN,
                CVV = request.CVV,
                OwnerName = customer.FirstName,
                OwnerLastName = customer.LastName,
                RegistrationDate = DateTime.Now,
                ExpiredDate = DateTime.Now.AddYears(3),
                Status = Status.Active
            };
            await _bankRepository.AddCardToDbAsync(card);
            await _bankRepository.SaveChangesAsync();
            return card;
        }
    }
}
