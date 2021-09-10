using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    public class BankTaskScheduller
    {
        private readonly int delayTime;
        private int transactionCount;
        private readonly Random rnd;
        public int Transactions { get => transactionCount; }

        /// <summary>
        /// создаю несколько тасков для случайного перевода денег
        /// </summary>
        /// <param name="accounts">участники транзакций</param>
        /// <param name="transactionCount">количество транзакций</param>
        /// <param name="delayTime">максимальное время задержки между транзакциями</param>
        public BankTaskScheduller(List<Account> accounts, int transactionCount, int delayTime)
        {
            rnd = new Random();
            this.transactionCount = transactionCount;
            this.delayTime = delayTime;

            Console.WriteLine("Start {0} transactions.. ", this.transactionCount);

            MoneyTransferer bt1 = new MoneyTransferer();
            MoneyTransferer bt2 = new MoneyTransferer();
            MoneyTransferer bt3 = new MoneyTransferer();
            MoneyTransferer bt4 = new MoneyTransferer();
            MoneyTransferer bt5 = new MoneyTransferer();

            Task t1 = new Task(() => new Object());
            Task t2 = new Task(() => new Object());
            Task t3 = new Task(() => new Object());
            Task t4 = new Task(() => new Object());
            Task t5 = new Task(() => new Object());

            while (this.transactionCount > 0)
            {
                if (transactionCount > 0) Task.Delay(Randomize()).GetAwaiter().OnCompleted(() => t1 = Task.Run(() => bt1.DoTransfer(accounts[0], accounts[1], ref this.transactionCount)));
                if (transactionCount > 0) Task.Delay(Randomize()).GetAwaiter().OnCompleted(() => t2 = Task.Run(() => bt2.DoTransfer(accounts[2], accounts[3], ref this.transactionCount)));
                if (transactionCount > 0) Task.Delay(Randomize()).GetAwaiter().OnCompleted(() => t3 = Task.Run(() => bt3.DoTransfer(accounts[1], accounts[0], ref this.transactionCount)));
                if (transactionCount > 0) Task.Delay(Randomize()).GetAwaiter().OnCompleted(() => t4 = Task.Run(() => bt4.DoTransfer(accounts[3], accounts[2], ref this.transactionCount)));
                if (transactionCount > 0) Task.Delay(Randomize()).GetAwaiter().OnCompleted(() => t5 = Task.Run(() => bt5.DoTransfer(accounts[0], accounts[3], ref this.transactionCount)));
            }

            Console.WriteLine("Ends at {0} transactions.. ", this.transactionCount);
        }

        public int Randomize()
        {
            return rnd.Next(delayTime) + delayTime;
        }

        public int PrintInfo()
        {
            Console.WriteLine("Transaction: {0} !", transactionCount);
            return transactionCount;
        }
    }
}