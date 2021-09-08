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
        int _money;
        //object _lock;
        public int Money
        {
            get
            {
                return _money;
            }
            set
            {
                //переключаю поток - "тест Албахари"
                Thread.Yield();
                //lock (_lock)
                {
                    _money = value;
                }
            }
        }
        public Account()
        {
            Money = 10000;
            Random rnd = new Random();
            ID = rnd.Next(1, 10000).ToString();
        }
    }
}
