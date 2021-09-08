using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Threads
{
    public class Mutex
    {
        public static HashSet<Account> hashset = new HashSet<Account>();
        public static Object locked = new Object();
        private bool SetMutex(Account a1, Account a2)
        {
            bool result;
            lock (locked)
            {
                result = hashset.Add(a1);
                result = result && hashset.Add(a2);
            }
            return result;
        }

        public bool ResetMutex(Account a1, Account a2)
        {
            bool result;
            lock (locked)
            {
                result = hashset.Remove(a1);
                result = result && hashset.Remove(a2);
            }
            return result;
        }

        public void BlockingWait(Account a1, Account a2)
        {
            while (!this.SetMutex(a1, a2));
            return;
        }
    }
}