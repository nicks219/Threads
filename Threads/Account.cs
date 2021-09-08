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

        public decimal Money
        {
            get
            {
                return _money;
            }
            set
            {
                {
                    _money = value;
                }
            }
        }
        public Account()
        {
            ID = new Object().GetHashCode().ToString();
            Money = 10000M;
            Random rnd = new Random();
            ID = rnd.Next(1, 10000).ToString();
        }
    }
}