using System;
using System.Threading;

namespace Threads
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int accountsCount = 4;
            Accounts accounts = new Accounts(accountsCount);
            _ = new BankTaskScheduller(accounts.accounts, 1000000, 0);
            Thread.Sleep(1000);
            accounts.PrintInfo();
            Console.ReadLine();
        }
    }
}