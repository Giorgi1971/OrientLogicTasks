using System;
using IbanNet;
using CredoProject.Core.Models.Requests;
using CredoProject.Core.Db.Entity;

namespace CredoProject.Core.Validations
{
    public interface IValidate 
    {
        UserEntity ValidateCustomer(CreateCustomerRequest request);
        AccountEntity ValidateAccount(CreateAccountRequest request);
        CardEntity ValidateCard(CreateCardRequest request);
    }

    public class Validate: IValidate
    {
        public UserEntity ValidateCustomer(CreateCustomerRequest request)
        {
            var customer = new UserEntity()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Email = request.Email,
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
                CustomerEntityId = request.CustomerId,
                CreateAt = DateTime.Now,
            };
            return account;
        }

        public CardEntity ValidateCard(CreateCardRequest request)
        {
            var card = new CardEntity()
            {
                CardNumber = request.CardNumber,
                PIN = request.PIN,
                CVV = request.CVV,
                AccountEntityId = request.AccountEntityId,
                RegistrationDate = DateTime.Now,
                ExpiredDate = DateTime.Now.AddYears(3),
                Status = Status.Active
            };
            return card;
        }
    }
}
