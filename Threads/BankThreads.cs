using System;
using System.Collections.Generic;
using System.Threading;

namespace Threads
{
    public class BankThreads
    {
        public Mutex mutex;
        public int transactionCount { get; private set; }
        int delayTime;

        /// <summary>
        /// создаю 4 потока для случайного перевода денег
        /// </summary>
        /// <param name="accounts">участники транзакций</param>
        /// <param name="transactionCount">количество транзакций</param>
        /// <param name="delayTime">время задержки</param>
        public BankThreads(List<Account> accounts, int transactionCount, int delayTime)
        {
            mutex = new Mutex();
            Thread tr1;
            Thread tr2;
            Thread tr3;
            Thread tr4;
            tr1 = new Thread(() => MoneyTransfer(accounts[0], accounts[1], 0));
            tr2 = new Thread(() => MoneyTransfer(accounts[2], accounts[3], 0));
            tr3 = new Thread(() => MoneyTransfer(accounts[1], accounts[0], 0));
            tr4 = new Thread(() => MoneyTransfer(accounts[2], accounts[3], 1));
            tr1.Start();
            tr3.Start();
            tr2.Start();
            tr4.Start();
            this.transactionCount = transactionCount;
            this.delayTime = delayTime;
        }

        public void MoneyTransfer(Account first, Account second, int direction)
        {
            int id = Thread.CurrentThread.ManagedThreadId; ;
            var rnd = new Random();
            while (transactionCount > 0)
            {
                mutex.BlockingWait(first, second);
                {
                    lock (first)
                    {
                        //ПОПЫТКА СЛОМАТЬ
                        Thread.Sleep(new Random().Next(delayTime));
                        lock (second)
                        {
                            decimal firstMoney = first.Money;
                            decimal secondMoney = second.Money;
                            var valueMoney = rnd.Next(1000) + 0.1M;
                            if (direction == 0)
                            {
                                firstMoney -= valueMoney;
                                secondMoney += valueMoney;
                            }
                            else
                            {
                                firstMoney += valueMoney;
                                secondMoney -= valueMoney;
                            }
                            first.Money = firstMoney;
                            second.Money = secondMoney;
                        }
                    }
                    if (!mutex.ResetMutex(first, second)) 
                    { 
                        throw new Exception(); 
                    }
                    //Thread.Sleep(new Random().Next(delayTime));
                    transactionCount--;
                }
            }
        }

        public int PrintInfo()
        {
            Console.WriteLine("Transaction: {0}", transactionCount);
            return transactionCount;
        }
    }
}