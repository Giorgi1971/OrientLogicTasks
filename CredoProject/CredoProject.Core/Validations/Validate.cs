using System;
using IbanNet;
using CredoProject.Core.Models.Requests;
using CredoProject.Core.Db.Entity;

namespace CredoProject.Core.Validations
{

    public interface IValidate 
    {
        CustomerEntity ValidateCustomer(CreateCustomerRequest request);
        AccountEntity ValidateAccount(CreateAccountRequest request);
        CardEntity ValidateCard(CreateCardRequest request);
    }

    public class Validate: IValidate
    {
        public CustomerEntity ValidateCustomer(CreateCustomerRequest request)
        {
            var customer = new CustomerEntity()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Email = request.Email,
                Hash = GetPasswordHash(request.Password),
                PersonalNumber = request.PersonalNumber
            };
            return customer;
        }


        public AccountEntity ValidateAccount(CreateAccountRequest request)
        {
            Console.WriteLine(request.IBAN);
            var validator = new IbanValidator();
            var iban = request.IBAN;
            var validationResult = validator.Validate(iban);

            if (!validationResult.IsValid)
                throw new Exception("IBAN not valid!");

            var account = new AccountEntity()
            {
                IBAN = iban,
                Amount = request.Amount,
                Currency = request.Currency,
                CustomerId = request.CustomerId,
                CreateAt = DateTime.Now,
            };
            return account;
        }


        public CardEntity ValidateCard(CreateCardRequest request)
        {
            var card = new CardEntity()
            {
                //FirstName = request.FirstName,
                //LastName = request.LastName,
                //BirthDate = request.BirthDate,
                //Email = request.Email,
                //Hash = GetPasswordHash(request.Password),
                //PersonalNumber = request.PersonalNumber
            };
            return card;
        }
        private string GetPasswordHash(string? password)
        {
            if (password == null) 
                throw new NotImplementedException();
            return "HashedPassword";
        }
    }
}
