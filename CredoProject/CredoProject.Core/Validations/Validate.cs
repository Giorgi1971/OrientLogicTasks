using System;
using IbanNet;
using CredoProject.Core.Models.Requests;
using CredoProject.Core.Db.Entity;

namespace CredoProject.Core.Validations
{
    public interface IValidate 
    {
        UserEntity ValidateCustomer(CreateCustomerRequest request);
        bool ValidateAccount(string str);
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


        public bool ValidateAccount(string iban)
        {
            var validator = new IbanValidator();
            var validationResult = validator.Validate(iban);

            if (!validationResult.IsValid)
                throw new Exception("IBAN not valid!");
            return true;
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
                ExpiredDate = DateTime.Now.AddYears(3).ToString("MM-yyyy"),
                Status = Status.Active
            };
            return card;
        }
    }
}
