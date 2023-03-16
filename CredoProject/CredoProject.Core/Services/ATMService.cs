//using System;
//using CredoProject.Core.Db;
//using CredoProject.Core.Db.Entity;
//using CredoProject.Core.Validations;
//using CredoProject.Core.Repositories;
//using CredoProject.Core.Models.Requests;
//using Microsoft.AspNetCore.Mvc;
//using System.Security.Principal;


//namespace CredoProject.Core.Services
//{
//    public interface IATMServices
//    {
//        Task<UserEntity> RegisterCustomerAsync(CreateCustomerRequest request);
//        Task<AccountEntity> RegisterAccountAsync(CreateAccountRequest request);
//        Task<CardEntity> RegisterCardAsync(CreateCardRequest request);
//        Task<UserEntity> GetUserEntity(int id);
//    }

//    public class ATMService : IATMServices
//    {
//        private readonly IBankRepository _bankRepository;
//        private readonly IValidate _validate;

//        public CoreServices(IValidate validate, IBankRepository repository)
//        {
//            _validate = validate;
//            _bankRepository = repository;
//        }

//        public async Task<UserEntity> RegisterCustomerAsync(CreateCustomerRequest request) {
//        }
//    }
//}

