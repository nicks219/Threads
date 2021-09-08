﻿using System;
using System.Threading;

namespace Threads
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int accountsCount = 4;
            var accounts = new Accounts(accountsCount);
            var bank = new BankThreads(accounts.accounts, 500, 10);

            while (bank.transactionCount > 0)
            {
                //переключаю поток - "тест Албахари"
                //Thread.Yield();
            }
            accounts.PrintInfo();
            bank.PrintInfo();
            Console.ReadLine();
        }
    }
}