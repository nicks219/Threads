using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    public class BankTaskScheduller
    {
        private readonly Mutex mutex;
        private readonly int delayTime;
        private int transactionCount;
        private readonly Random rnd;
        public int Transactions { get => transactionCount; }

        /// <summary>
        /// создаю несколько тасков для случайного перевода денег
        /// </summary>
        /// <param name="accounts">участники транзакций</param>
        /// <param name="_transactionCount">количество транзакций</param>
        /// <param name="_delayTime">максимальное время задержки между транзакциями</param>
        public BankTaskScheduller(List<Account> accounts, int _transactionCount, int _delayTime)
        {
            rnd = new Random();
            transactionCount = _transactionCount;
            delayTime = _delayTime;
            mutex = new Mutex();
            Console.WriteLine("Start {0} transactions.. ", transactionCount);

            MoneyTransferrer bt1 = new MoneyTransferrer(mutex, delayTime);
            MoneyTransferrer bt2 = new MoneyTransferrer(mutex, delayTime);
            MoneyTransferrer bt3 = new MoneyTransferrer(mutex, delayTime);
            MoneyTransferrer bt4 = new MoneyTransferrer(mutex, delayTime);
            MoneyTransferrer bt5 = new MoneyTransferrer(mutex, delayTime);

            Task t1 = new Task(() => new Object());
            Task t2 = new Task(() => new Object());
            Task t3 = new Task(() => new Object());
            Task t4 = new Task(() => new Object());
            Task t5 = new Task(() => new Object());

            while (transactionCount > 0)
            {
                Task.Delay(Randomize()).GetAwaiter().OnCompleted(() => t1 = Task.Run(() => bt1.DoTransfer(accounts[0], accounts[1], ref transactionCount)));
                Task.Delay(Randomize()).GetAwaiter().OnCompleted(() => t2 = Task.Run(() => bt2.DoTransfer(accounts[2], accounts[3], ref transactionCount)));
                Task.Delay(Randomize()).GetAwaiter().OnCompleted(() => t3 = Task.Run(() => bt3.DoTransfer(accounts[1], accounts[0], ref transactionCount)));
                Task.Delay(Randomize()).GetAwaiter().OnCompleted(() => t4 = Task.Run(() => bt4.DoTransfer(accounts[3], accounts[2], ref transactionCount)));
                Task.Delay(Randomize()).GetAwaiter().OnCompleted(() => t5 = Task.Run(() => bt5.DoTransfer(accounts[0], accounts[3], ref transactionCount)));
            }

            Task.WaitAll();

            Console.WriteLine("Ends at {0} transactions.. ", transactionCount);
        }

        public int Randomize()
        {
            return 1;
            return rnd.Next(100);
        }

        public int PrintInfo()
        {
            Console.WriteLine("Transaction: {0}", transactionCount);
            return transactionCount;
        }
    }
}