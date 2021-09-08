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

        /// <summary>
        /// перевод денежных средств
        /// </summary>
        /// <param name="sender">отправитель</param>
        /// <param name="recipient">получатель</param>
        /// <param name="transactionCount">ссылка на счетчик транзакций</param>
        public void MoneyTransfer(Account sender, Account recipient, ref int transactionCount)
        {
            int id = Thread.CurrentThread.ManagedThreadId; ;
            Random rnd = new Random();
            while (transactionCount > 0)
            {
                mutex.BlockingWait(sender, recipient);
                {
                    lock (sender)
                    {
                        //ПОПЫТКА СЛОМАТЬ
                        Thread.Sleep(new Random().Next(delayTime));
                        lock (recipient)
                        {
                            decimal senderMoney = sender.Money;
                            decimal recipientMoney = recipient.Money;
                            decimal theFunds = rnd.Next(1000) + 0.1M;
                            senderMoney -= theFunds;
                            recipientMoney += theFunds;
                            sender.Money = senderMoney;
                            recipient.Money = recipientMoney;
                        }
                        transactionCount--;
                    }
                    if (!mutex.ResetMutex(sender, recipient))
                    {
                        throw new ThreadStateException();
                    }
                    //Thread.Sleep(new Random().Next(delayTime));
                }
            }
            Console.WriteLine("Thread stopped: " + id);
        }
    }
}
