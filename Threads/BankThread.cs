using System;
using System.Threading;

namespace Threads
{
    internal class BankThread
    {
        Mutex mutex;
        int delayTime;
        public BankThread(Mutex mutex, int delayTime)
        {
            this.mutex = mutex;
            this.delayTime = delayTime;
        }
        public void MoneyTransfer(Account first, Account second, ref int transactionCount)
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
                            firstMoney -= valueMoney;
                            secondMoney += valueMoney;
                            first.Money = firstMoney;
                            second.Money = secondMoney;
                        }
                        transactionCount--;
                    }
                    if (!mutex.ResetMutex(first, second))
                    {
                        throw new Exception();
                    }
                    //Thread.Sleep(new Random().Next(delayTime));
                }
            }
            Console.WriteLine("Thread stopped: " + id);
        }
    }
}
