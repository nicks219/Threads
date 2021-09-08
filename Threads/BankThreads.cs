using System;
using System.Collections.Generic;
using System.Threading;

namespace Threads
{
    public class BankThreads
    {
        public int _transactionCount { get; private set; }
        int _delayTime;

        /// <summary>
        /// создаю 4 потока для случайного перевода денег
        /// </summary>
        /// <param name="accounts">участники транзакций</param>
        /// <param name="transactionCount">количество транзакций</param>
        /// <param name="delayTime">время задержки</param>
        public BankThreads(List<Account> accounts, int transactionCount, int delayTime)
        {
            Thread tr;
            tr = new Thread(() => Convert(accounts[0], accounts[1], 0));
            tr.Start();
            tr = new Thread(() => Convert(accounts[2], accounts[3], 0));
            tr.Start();
            tr = new Thread(() => Convert(accounts[0], accounts[1], 1));
            tr.Start();
            tr = new Thread(() => Convert(accounts[2], accounts[3], 1));
            tr.Start();
            _transactionCount = transactionCount;
            _delayTime = delayTime;
        }

        public void Convert(Account f, Account s, int direction)
        {
            var rnd = new Random();
            while (_transactionCount > 0)
            {
                lock (f)
                {
                    lock (s)
                    {
                        int firstMoney = f.Money;
                        int secondMoney = s.Money;
                        var valueMoney = rnd.Next(1000);
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
                        f.Money = firstMoney;
                        s.Money = secondMoney;
                    }
                }
                Thread.Sleep(new Random().Next(_delayTime));
                _transactionCount--;
            }
        }

        public int PrintInfo()
        {
            Console.WriteLine("Transaction: {0}", _transactionCount);
            return _transactionCount;
        }
    }
}
