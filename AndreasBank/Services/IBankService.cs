using AndreasBank.Models;

namespace AndreasBank.Services
{
    public interface IBankService
    {
        bool Deposit(int accountId, decimal sum);
        bool Withdraw(int accountId, decimal sum);
        Account GetAccountById(int id);
        Customer GetCustomerById(int id);
    }
}
