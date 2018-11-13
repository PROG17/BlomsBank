using System.Linq;
using AndreasBank.Models;

namespace AndreasBank.Repositories
{
    public interface IBankRepository
    {
        IQueryable<Account> Accounts { get; }
        IQueryable<Customer> Customers { get; }

        Account GetAccountById(int id);
        Customer GetCustomerById(int id);

        bool SaveAccount(Account account);
        bool SaveCustomer(Customer customer);
    }
}
