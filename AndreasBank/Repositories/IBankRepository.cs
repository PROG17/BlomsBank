using System.Linq;
using AndreasBank.Models;

namespace AndreasBank.Repositories
{
    public interface IBankRepository
    {
        IQueryable<Account> Accounts { get; }
        IQueryable<Customer> Customers { get; }

        bool SaveAccount();
        bool SaveCustomer();
    }
}