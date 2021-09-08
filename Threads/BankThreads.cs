using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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
            Thread tr5;

            Task t5;

            BankThread bt1 = new BankThread(mutex, delayTime);
            BankThread bt2 = new BankThread(mutex, delayTime);
            BankThread bt3 = new BankThread(mutex, delayTime);
            BankThread bt4 = new BankThread(mutex, delayTime);
            BankThread bt5 = new BankThread(mutex, delayTime);

            //0>3 все ломал
            t5 = Task.Run(() => bt1.MoneyTransfer(accounts[0], accounts[3], ref transactionCount));
            tr1 = new Thread(() => bt1.MoneyTransfers(accounts[0], accounts[1], ref transactionCount, 1));
            tr2 = new Thread(() => bt2.MoneyTransfers(accounts[2], accounts[3], ref transactionCount, 2));
            tr3 = new Thread(() => bt3.MoneyTransfers(accounts[1], accounts[0], ref transactionCount, 3));
            tr4 = new Thread(() => bt4.MoneyTransfers(accounts[3], accounts[2], ref transactionCount, 4));
            tr5 = new Thread(() => bt4.MoneyTransfers(accounts[0], accounts[3], ref transactionCount, 0));

            //tr5.Start();

            tr1.Start();
            tr3.Start();
            tr2.Start();
            tr4.Start();
            

            Console.WriteLine("Start {0} transactions.. ", transactionCount);
            tr1.Join();
            tr2.Join();
            tr3.Join();
            tr4.Join();
            //tr5.Join();
            Console.WriteLine("Ends at {0} transactions.. ", transactionCount);
        }

        public int PrintInfo()
        {
            Console.WriteLine("Transaction: {0}", transactionCount);
            return transactionCount;
        }
    }
}