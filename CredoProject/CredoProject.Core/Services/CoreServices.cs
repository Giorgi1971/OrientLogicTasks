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
        Task<CustomerEntity> RegisterCustomerAsync(CreateCustomerRequest request);
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

        public async Task<CustomerEntity> RegisterCustomerAsync(CreateCustomerRequest request)
        {
            var customer = _validate.ValidateCustomer(request);
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
            var card = _validate.ValidateCard(request);
            await _bankRepository.AddCardToDbAsync(card);
            await _bankRepository.SaveChangesAsync();
            return card;
        }
    }
}
