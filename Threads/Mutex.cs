using System;
using System.Collections.Generic;

namespace Threads
{
    /// <summary>
    /// бесполезная самописная блокировка (работает)
    /// </summary>
    public class Mutex
    {
        private readonly HashSet<Account> _hashset = new HashSet<Account>();
        private readonly object _locked = new Object();

        private bool SetMutex(Account a1, Account a2, int id)
        {
            bool result;
            lock (_locked)
            {
                //если транзакция не удалась, надо делать откат транзакции
                result = _hashset.Add(a1);
                if (result)
                {
                    result = _hashset.Add(a2);
                    if (!result)
                    {
                        _hashset.Remove(a1);
                    }
                }

            }
            return result;
        }

        public object IsLocked()
        {
            return _locked;
        }

        public bool ResetMutex(Account a1, Account a2)
        {
            bool result;
            lock (_locked)
            {
                result = _hashset.Remove(a1);
                result = result && _hashset.Remove(a2);
            }
            return result;
        }

        /// <summary>
        /// постановка двух аккаунтов в список транзакций
        /// </summary>
        /// <param name="a1">первый аккаунт</param>
        /// <param name="a2">второй аккаунт</param>
        /// <param name="id"></param>
        public void BlockingWait(Account a1, Account a2, int id)
        {
            while (!this.SetMutex(a1, a2, id)) { }
        }
    }
}