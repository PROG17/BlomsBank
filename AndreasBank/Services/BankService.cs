using AndreasBank.Exceptions;
using AndreasBank.Models;
using AndreasBank.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndreasBank.Services
{
    public class BankService : IBankService
    {
        private readonly IBankRepository _bankRepository;

        public BankService(IBankRepository bankRepo)
        {
            _bankRepository = bankRepo;
        }

        public bool Withdraw(int accountId, decimal sum)
        {
            var account = _bankRepository.Accounts.FirstOrDefault(a => a.Id == accountId);

            if (account == null) throw new AccountNotFoundException("Det finns inget konto med angivet kontonummer.");

            if (sum <= 0) throw new NonPositiveValueException("Summan måste var större än noll.");

            if (account.ActualSum - sum < 0) throw new InsufficientFundsException("Det finns för lite pengar på kontot för att ta ut angiven summa.");

            account.Balance -= sum;

            return _bankRepository.SaveAccount(account);
        }

        public bool Deposit(int accountId, decimal sum)
        {
            var account = _bankRepository.Accounts.FirstOrDefault(a => a.Id == accountId);

            if (account == null) throw new AccountNotFoundException("Det finns inget konto med angivet kontonummer.");

            if (sum <= 0) throw new NonPositiveValueException("Summan måste var större än noll.");

            account.Balance += sum;

            return _bankRepository.SaveAccount(account);
        }

        public bool Transfer(int fromAccountId, int toAccountId, decimal sum)
        {
            var fromAccount = _bankRepository.Accounts.FirstOrDefault(a => a.Id == fromAccountId);
            if (fromAccount == null) throw new AccountNotFoundException("Det finns inget konto med angivet kontonummer.");

            var toAccount = _bankRepository.Accounts.FirstOrDefault(a => a.Id == toAccountId);
            if (toAccount == null) throw new AccountNotFoundException("Det finns inget konto med angivet kontonummer.");

            if (sum > fromAccount.Balance) return false;
            toAccount.Balance += sum;
            fromAccount.Balance -= sum;

            _bankRepository.SaveAccount(fromAccount);
            _bankRepository.SaveAccount(toAccount);

            return true;
        }


        public Account GetAccountById(int id)
        {
            return _bankRepository.GetAccountById(id);
        }

        

        public Customer GetCustomerById(int id)
        {
            return _bankRepository.GetCustomerById(id);
        }
    }
}
