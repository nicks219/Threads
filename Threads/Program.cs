using System;

namespace Threads
{
    public class Program
    {
        public static void Main(string[] args)
        {
            int accountsCount = 4;
            Accounts accounts = new Accounts(accountsCount);
            BankThreads bank = new BankThreads(accounts.accounts, 400, 10);

            //while (bank.Transactions > 0) ;
            accounts.PrintInfo();
            Console.ReadLine();
        }
    }
}