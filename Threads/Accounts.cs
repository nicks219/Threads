﻿using System;
using System.Collections.Generic;

namespace Threads
{
    public class Accounts
    {
        public List<Account> accounts;

        public Accounts(int accountCount)
        {
            accounts = new List<Account>();
            while (accountCount > 0)
            {
                accounts.Add(new Account());
                accountCount--;
            }
        }

        public int Sum()
        {
            int sum = 0;
            foreach(var r in accounts)
            {
                sum += r.Money;
            }
            return sum;
        }

        public int PrintInfo()
        {
            int sum = 0;
            foreach (var r in accounts)
            {
                int money = r.Money;
                Console.WriteLine("Account: {0} Money: {1}", r.ID, money);
                sum += money;
            }
            Console.WriteLine("Sum: {0}", sum);
            return sum;
        }
    }
}