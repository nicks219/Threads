using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Threads
{
    public class BankTaskScheduller
    {
        public static BankTaskScheduller CreateInstance(List<Account> accounts, int transactionCount, int delayTime)
        {
            return new BankTaskScheduller(accounts, transactionCount, delayTime);
        }

        private readonly int _delayTime;
        private int _transactionCount;
        private readonly Random _rnd;
        public int Transactions => _transactionCount;

        /// <summary>
        /// создаю несколько тасков для случайного перевода денег
        /// </summary>
        /// <param name="accounts">участники транзакций</param>
        /// <param name="transactionCount">количество транзакций</param>
        /// <param name="delayTime">максимальное время задержки между транзакциями</param>
        private BankTaskScheduller(List<Account> accounts, int transactionCount, int delayTime)
        {
            _rnd = new Random();
            this._transactionCount = transactionCount;
            this._delayTime = delayTime;

            Console.WriteLine("Start {0} transactions.. ", this._transactionCount);

            MoneyTransferer bt1 = new MoneyTransferer();
            MoneyTransferer bt2 = new MoneyTransferer();
            MoneyTransferer bt3 = new MoneyTransferer();
            MoneyTransferer bt4 = new MoneyTransferer();
            MoneyTransferer bt5 = new MoneyTransferer();

            Task t1 = new Task(() => new object());
            Task t2 = new Task(() => new object());
            Task t3 = new Task(() => new object());
            Task t4 = new Task(() => new object());
            Task t5 = new Task(() => new object());

            while (this._transactionCount > 0)
            {
                if (transactionCount > 0) Task.Delay(Randomize()).GetAwaiter().OnCompleted(() => t1 = Task.Run(() => bt1.DoTransfer(accounts[0], accounts[1], ref this._transactionCount)));
                if (transactionCount > 0) Task.Delay(Randomize()).GetAwaiter().OnCompleted(() => t2 = Task.Run(() => bt2.DoTransfer(accounts[2], accounts[3], ref this._transactionCount)));
                if (transactionCount > 0) Task.Delay(Randomize()).GetAwaiter().OnCompleted(() => t3 = Task.Run(() => bt3.DoTransfer(accounts[1], accounts[0], ref this._transactionCount)));
                if (transactionCount > 0) Task.Delay(Randomize()).GetAwaiter().OnCompleted(() => t4 = Task.Run(() => bt4.DoTransfer(accounts[3], accounts[2], ref this._transactionCount)));
                if (transactionCount > 0) Task.Delay(Randomize()).GetAwaiter().OnCompleted(() => t5 = Task.Run(() => bt5.DoTransfer(accounts[0], accounts[3], ref this._transactionCount)));
            }

            Console.WriteLine("Ends at {0} transactions.. ", this._transactionCount);
        }

        public int Randomize()
        {
            return _rnd.Next(_delayTime) + _delayTime;
        }

        public int PrintInfo()
        {
            Console.WriteLine("Transaction: {0} !", _transactionCount);
            return _transactionCount;
        }
    }
}