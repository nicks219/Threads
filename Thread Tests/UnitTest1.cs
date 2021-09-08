using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using Threads;

namespace Thread_Tests
{
    public class Tests
    {
        static Accounts accounts;
        static BankThreads bank;

        [SetUp]
        public void Setup()
        {
            int accountsCount = 4;
            accounts = new Accounts(accountsCount);
            bank = new BankThreads(accounts.accounts, 2000, 1);
        }

        [Test]
        public void Test1()
        { 
            while (bank._transactionCount > 0)
            {
                Thread.Yield();
            }
            Assert.AreEqual(accounts.PrintInfo(), 40000);
            //bank.PrintInfo();
        }
    }
}