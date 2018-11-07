using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndreasBank.Models
{
    public class Account
    {
        public Account()
        {

        }

        public Account(int id, decimal balance, decimal credit, Customer owner)
        {
            Id = id;
            Balance = balance;
            Credit = credit;
            Owner = owner;
        }

        public int Id { get; set; }
        public decimal Balance { get; set; }
        public decimal Credit { get; set; }
        public Customer Owner { get; set; }
    }
}
