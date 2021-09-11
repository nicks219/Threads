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
            var bank = BankTaskScheduller.CreateInstance(accounts.UsersList, 100000, 1);
            Thread.Sleep(100);
            accounts.PrintInfo();
            bank.PrintInfo();
            Console.ReadLine();
            //Environment.Exit(0);
        }
    }
}