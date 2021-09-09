using System;

namespace Threads
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int accountsCount = 4;
            Accounts accounts = new Accounts(accountsCount);
            _ = new BankTaskScheduller(accounts.accounts, 300, 2);
            accounts.PrintInfo();
            Console.ReadLine();
        }
    }
}