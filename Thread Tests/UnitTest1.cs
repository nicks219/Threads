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
            bank = new BankThreads(accounts.accounts, 700, 2);
        }

        [Test]
        //сохранился ли баланс на счетах
        //до конца ли исчерпаны транзакции
        public void Test1()
        {
            while (bank.Transactions > 0) ;
            Assert.AreEqual(accounts.PrintInfo(), 40000);
            Assert.AreEqual(bank.PrintInfo(), -3);
        }
    }
}