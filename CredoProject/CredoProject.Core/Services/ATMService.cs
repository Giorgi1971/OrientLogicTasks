using System;
using CredoProject.Core.Db;
using CredoProject.Core.Db.Entity;
using CredoProject.Core.Validations;
using CredoProject.Core.Repositories;
using CredoProject.Core.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using CredoProject.Core.Models.Requests.Card;
using Microsoft.EntityFrameworkCore;

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
        private readonly IValidate _validate;

        public ATMService(IValidate validate, ICardRepository repository)
        {
            _validate = validate;
            _cardRepository = repository;
        }

        public async Task<string> WithdrawManyFromCardAsync(WithdrawManyFromCardRequest request)
        {
            var card = await _cardRepository.GetCardByNumberAndPinAsync(request.CardNumber, request.PIN);
            var account = card.AccountEntity;
            account.Amount -= request.WithdrawAmount;
            var tran1 = new TransactionEntity()
            {
                AccountEntityFrom = account,
                AccountEntityTo = account,
                AmountTransaction = request.WithdrawAmount,
                CreatedAt = DateTime.Now,
                TransType = "ATM",
                CurrencyFrom = account.Currency,
                CurrencyTo = request.currencyWithdraw,
                Fee = CalculateFee(),
                AccountFromId = account.AccountEntityId,
                AccountToId = account.AccountEntityId,
                ExecutionAt = DateTime.Now
            };
            await _cardRepository.SaveChangesAsync();

            return $"Welcome! {account.CustomerEntity.FirstName.ToString()}, Your Card Balance is {account.Amount} {account.Currency}";
        }

        private decimal CalculateFee()
        {
            throw new NotImplementedException();
        }

        public async Task<string> CardAuthorizationAsync(CardAutorizationRequest request)
        {
            var card = await _cardRepository.GetCardByNumberAndPinAsync(request.CardNumber, request.PIN);
            if (card == null) return "Card Not valid. Contact bank Manager!";
            if (card.Status == Status.Blocked) return "Card Blocked. Contact bank Manager!";
            if (card.Status == Status.Expired) return "Card date expered. Contact bank Manager!";
            if (card.ExpiredDate < DateTime.Now)
            {
                card.Status = Status.Expired; return "Card date expered. Contact bank Manager!";
            }
            return $"Welcome! {card.AccountEntity.CustomerEntity.FirstName.ToString()}";
        }

        public async Task<string> ChangeCardPinAsync(ChangeCardPinRequest request)
        {
            var card = await _cardRepository.GetCardByNumberAndPinAsync(request.CardNumber, request.PIN);
            if (card == null) return "Invalid Card Credentials!";
            card.PIN = request.newPIN;
            await _cardRepository.SaveChangesAsync();
            return "Pin Changed";
        }

        public async Task<string> GetCardBalanceAsync(CardAutorizationRequest request)
        {
            var card = await _cardRepository.GetCardByNumberAndPinAsync(request.CardNumber, request.PIN);
            // ესენი სხვაგანაცაა და უნდა გადავიტანო ვალიდაციაში.
            if (card == null) return "Card Not valid. Contact bank Manager!";
            if (card.Status == Status.Blocked) return "Card Blocked. Contact bank Manager!";
            if (card.Status == Status.Expired) return "Card date expered. Contact bank Manager!";
            if (card.ExpiredDate < DateTime.Now)
            {
                card.Status = Status.Expired; return "Card date expered. Contact bank Manager!";
            }
            var ac = card.AccountEntity;
            return $"Welcome! {ac.CustomerEntity.FirstName.ToString()}, Your Card Balance is {ac.Amount} {ac.Currency}";
        }
    }
}

