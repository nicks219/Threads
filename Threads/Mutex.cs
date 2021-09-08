using System;
using System.Collections.Generic;

namespace Threads
{
    /// <summary>
    /// самодельный синхронный мьютекс
    /// </summary>
    public class Mutex
    {
        private HashSet<Account> hashset = new HashSet<Account>();
        private Object locked = new Object();

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

        /// <summary>
        /// постановка двух аккаунтов в список транзакций
        /// </summary>
        /// <param name="a1">первый аккаунт</param>
        /// <param name="a2">второй аккаунт</param>
        public void BlockingWait(Account a1, Account a2)
        {
            while (!this.SetMutex(a1, a2)) ;
            return;
        }
    }
}