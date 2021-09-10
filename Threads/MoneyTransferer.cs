using System;
using System.Collections.Generic;
using System.Threading;

namespace Threads
{
    internal class MoneyTransferer
    {
        /// <summary>
        /// разовый перевод денежных средств
        /// </summary>
        /// <param name="sender">отправитель</param>
        /// <param name="recipient">получатель</param>
        /// <param name="transactionCount">ссылка на счетчик транзакций</param>
        public void DoTransfer(Account sender, Account recipient, ref int transactionCount)
        {
            if (transactionCount < 0) return;
            decimal valueMoney = new Random().Next(10);

            for (;;)
            {
                lock (sender)
                {
                    if (Monitor.TryEnter(recipient))
                    {
                        try
                        {
                            transactionCount--;
                            sender.Money -= valueMoney;
                            recipient.Money += valueMoney;
                            return;
                        }
                        finally
                        {
                            Monitor.Exit(recipient);
                        }
                    }
                }

                lock (recipient)
                {
                    if (Monitor.TryEnter(sender))
                    {
                        try
                        {
                            transactionCount--;
                            sender.Money -= valueMoney;
                            recipient.Money += valueMoney;
                            return;
                        }
                        finally
                        {
                            Monitor.Exit(sender);
                        }
                    }
                }
            }
        }
    }
}