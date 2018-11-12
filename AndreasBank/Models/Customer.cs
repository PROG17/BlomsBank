using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndreasBank.Models
{
    public class Customer
    {
        public Customer()
        {
            Accounts = new List<Account>();
        }

        public Customer(int id, string name)
        {
            Id = id;
            Name = name;
            Accounts = new List<Account>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
