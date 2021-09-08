using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    public class Account
    {
        public string ID { get; private set; }
        decimal _money;
        int id;
        public decimal Money
        {
            get
            {
                return _money;
            }
            set
            {
                //переключаю поток - "тест Албахари"
                //Thread.Yield();
                {
                    _money = value;
                }
            }
        }
        public Account()
        {
            id = new Object().GetHashCode();
            Console.WriteLine(id);
            Money = 10000M;
            Random rnd = new Random();
            ID = rnd.Next(1, 10000).ToString();
        }
    }
}