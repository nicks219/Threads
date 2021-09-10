using System;
using System.Collections.Generic;
using System.Threading;

namespace Threads
{
    /// <summary>
    /// бесполезная самописная блокировка (работает)
    /// </summary>
    public class Mutex
    {
        private readonly HashSet<Account> hashset = new HashSet<Account>();
        private readonly object locked = new Object();

        private bool SetMutex(Account a1, Account a2, int id)
        {
            bool result;
            lock (locked)
            {
                //если транзакция не удалась, надо делать откат транзакции
                result = hashset.Add(a1);
                if (result == true)
                {
                    result = result && hashset.Add(a2);
                    if (result == false)
                    {
                        hashset.Remove(a1);
                    }
                }

            }
            return result;
        }

        public object IsLocked()
        {
            return locked;
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
        public void BlockingWait(Account a1, Account a2, int id)
        {
            while (!this.SetMutex(a1, a2, id)) ;
            return;
        }
    }
}