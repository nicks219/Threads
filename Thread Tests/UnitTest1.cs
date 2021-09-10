using NUnit.Framework;
using System.Threading;
using Threads;

namespace Thread_Tests
{
    public class Tests
    {
        static Accounts accounts;
        static BankTaskScheduller bank;

        [SetUp]
        public void Setup()
        {
            int accountsCount = 4;
            accounts = new Accounts(accountsCount);
            bank = new BankTaskScheduller(accounts.accounts, 30000, 100);
        }

        [Test]
        //���������� �� ������ �� ������
        //�� ����� �� ��������� ����������
        public void Test1()
        {
            while (bank.Transactions > 0) ;
            Thread.Sleep(200);
            Assert.AreEqual(accounts.PrintInfo(), 40000);
            Assert.IsTrue(bank.PrintInfo() < 0);
        }
    }
}