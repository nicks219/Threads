using System;
using System.Collections.Generic;

namespace Threads
{
    public class Accounts
    {
        public List<Account> UsersList;

        public Accounts(int accountCount)
        {
            UsersList = new List<Account>();
            while (accountCount > 0)
            {
                UsersList.Add(new Account());
                accountCount--;
            }
        }

        public decimal Sum()
        {
            decimal sum = 0;
            lock (UsersList)
            {
                foreach (var r in UsersList)
                {
                    sum += r.Money;
                }
            }
            return sum;
        }

        public decimal PrintInfo()
        {
            decimal sum = 0;
            lock (UsersList)
            {
                foreach (var r in UsersList)
                {
                    decimal money = r.Money;
                    Console.WriteLine("Account: {0} Money: {1}", r.Id, money);
                    sum += r.Money;
                }
                Console.WriteLine("Sum: {0}", sum);
            }
            return sum;
        }
    }
}
