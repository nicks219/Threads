using NUnit.Framework;
using System.Threading;
using Threads;

namespace Thread_Tests
{
    public class Tests
    {
        static Accounts _accounts;
        static BankTaskScheduller _bank;

        [SetUp]
        public void Setup()
        {
            int accountsCount = 4;
            _accounts = new Accounts(accountsCount);
            _bank = BankTaskScheduller.CreateInstance(_accounts.UsersList, 30000, 100);
        }

        [Test]
        //сохранился ли баланс на счетах
        //до конца ли исчерпаны транзакции
        public void Test1()
        {
            while (_bank.Transactions > 0) { }
            Thread.Sleep(100);
            Assert.AreEqual(_accounts.PrintInfo(), 40000);
            Assert.IsTrue(_bank.PrintInfo() == 0);
        }
    }
}