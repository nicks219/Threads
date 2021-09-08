using System;

namespace Threads
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int accountsCount = 4;
            Accounts accounts = new Accounts(accountsCount);
            _ = new BankThreads(accounts.accounts, 500, 2);
            accounts.PrintInfo();
            Console.ReadLine();
        }
    }
}