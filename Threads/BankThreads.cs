using System;
using System.Collections.Generic;
using System.Threading;

namespace Threads
{
    public class BankThreads
    {
        private readonly Mutex mutex;
        private readonly int delayTime;
        private int transactionCount;
        public int Transactions { get => transactionCount; }

        /// <summary>
        /// создаю 4 потока для случайного перевода денег
        /// </summary>
        /// <param name="accounts">участники транзакций</param>
        /// <param name="_transactionCount">количество транзакций</param>
        /// <param name="_delayTime">максимальное время задержки между транзакциями</param>
        public BankThreads(List<Account> accounts, int _transactionCount, int _delayTime)
        {
            transactionCount = _transactionCount;
            delayTime = _delayTime;

            mutex = new Mutex();
            Thread tr1;
            Thread tr2;
            Thread tr3;
            Thread tr4;
            BankThread bt1 = new BankThread(mutex, delayTime);
            BankThread bt2 = new BankThread(mutex, delayTime);
            BankThread bt3 = new BankThread(mutex, delayTime);
            BankThread bt4 = new BankThread(mutex, delayTime);

            tr1 = new Thread(() => bt1.MoneyTransfer(accounts[0], accounts[1], ref transactionCount));
            tr2 = new Thread(() => bt2.MoneyTransfer(accounts[2], accounts[3], ref transactionCount));
            tr3 = new Thread(() => bt3.MoneyTransfer(accounts[1], accounts[0], ref transactionCount));
            tr4 = new Thread(() => bt4.MoneyTransfer(accounts[3], accounts[2], ref transactionCount));
            tr1.Start();
            tr3.Start();
            tr2.Start();
            tr4.Start();

            Console.WriteLine("Start {0} transactions.. ", transactionCount);
            tr1.Join();
            tr2.Join();
            tr3.Join();
            tr4.Join();
            Console.WriteLine("Ends at {0} transactions.. ", transactionCount);
        }

        public int PrintInfo()
        {
            Console.WriteLine("Transaction: {0}", transactionCount);
            return transactionCount;
        }
    }
}