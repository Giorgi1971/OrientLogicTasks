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

namespace CredoProject.Core.Services
{
    public interface ICoreServices
    {
        Task<UserEntity> RegisterCustomerAsync(CreateCustomerRequest request);
        Task<AccountResponse> RegisterAccountAsync(CreateAccountRequest request);
        Task<CardEntity> RegisterCardAsync(CreateCardRequest request);
        Task<UserEntity> GetUserEntity(int id);
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
                ExpiredDate = DateTime.Now.AddYears(3),
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
