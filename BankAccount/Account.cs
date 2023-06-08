using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount
{
    class Account
    {
        public string AccountId;
        public string OwnerName { get; set; }
        public double CurrentBalance { get; set; }

        public Account(string name, double initialAmount)
        {
            Guid id = Guid.NewGuid();
            string idString = id.ToString();
            this.AccountId = idString;
            this.OwnerName = name;
            this.CurrentBalance = initialAmount;
        }

        public double Withdraw (double amount)
        {
            CurrentBalance -= amount;
            Console.WriteLine("Withdraw Successful !!!");
            return CurrentBalance;
        }

        public double Deposit (double amount)
        {
            CurrentBalance += amount;
            Console.WriteLine("Deposition Successful !!!");
            return CurrentBalance;
        }
    }
}
