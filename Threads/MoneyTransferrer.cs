using System;
using System.Threading;

namespace Threads
{
    internal class MoneyTransferrer
    {
        Mutex mutex;
        int delayTime;

        public MoneyTransferrer(Mutex mutex, int delayTime)
        {
            this.mutex = mutex;
            this.delayTime = delayTime;
        }

        /// <summary>
        /// разовый перевод денежных средств
        /// </summary>
        /// <param name="sender">отправитель</param>
        /// <param name="recipient">получатель</param>
        /// <param name="transactionCount">ссылка на счетчик транзакций</param>
        public void DoTransfer(Account sender, Account recipient, ref int transactionCount)
        {
            int id = Thread.CurrentThread.ManagedThreadId;
            Random rnd = new Random();
            //Console.WriteLine("Task started in thread: " + id);

            mutex.BlockingWait(sender, recipient, 0);
            {
                lock (sender)
                {
                    //ПОПЫТКА СЛОМАТЬ
                    //Thread.Sleep(new Random().Next(delayTime));
                    lock (recipient)
                    {
                        decimal senderMoney = sender.Money;
                        decimal recipientMoney = recipient.Money;
                        decimal theFunds = rnd.Next(1000) + 0.1M;
                        senderMoney -= theFunds;
                        recipientMoney += theFunds;
                        sender.Money = senderMoney;
                        recipient.Money = recipientMoney;

                        transactionCount--;
                        mutex.ResetMutex(sender, recipient);
                    }
                }
            }
            //Console.WriteLine("Task stopped in thread: " + id);
        }
    }
}
