using AndreasBank.Exceptions;
using AndreasBank.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndreasBank.Repositories
{
    public class BankRepository : IBankRepository
    {
        public BankRepository(List<Customer> customers)
        {
            Customers = customers.AsQueryable();
        }

        public BankRepository()
        {
            Customers = GetTestCustomers().AsQueryable();
        }

        public IQueryable<Customer> Customers { get; private set; }
        public IQueryable<Account> Accounts
            => Customers.SelectMany(c => c.Accounts);

        public Account GetAccountById(int id)
        {
            return Accounts.FirstOrDefault(a => a.Id == id);
        }

        public Customer GetCustomerById(int id)
        {
            return Customers.FirstOrDefault(a => a.Id == id);
        }

        public bool SaveCustomer(Customer customer)
        {
            return true;
        }

        public bool SaveAccount(Account account)
        {
            return true;
        }

        private List<Customer> GetTestCustomers()
        {
            var customer1 = new Customer(1, "Andreas Blom");
            var customer2 = new Customer(2, "Olivia Bechuchi Denbu Wilhelmsson");
            var customer3 = new Customer(3, "Anna-Maria Nordström");

            var customer1Account = new Account(1, 1000, 1000, customer1);
            var customer2Account = new Account(2, 2000, 1000, customer2);
            var customer3Account1 = new Account(3, 3000, 1000, customer3);
            var customer3Account2 = new Account(4, 10000, 0, customer3);

            customer1.Accounts.Add(customer1Account);
            customer2.Accounts.Add(customer2Account);
            customer3.Accounts.Add(customer3Account1);
            customer3.Accounts.Add(customer3Account2);

            return new List<Customer>
            {
                customer1,
                customer2,
                customer3
            };
        }
    }
}
