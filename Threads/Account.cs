using System;

namespace Threads
{
    public class Account
    {
        public string Id { get; }

        public decimal Money { get; set; }

        public Account()
        {
            Money = 10000M;
            Random rnd = new Random();
            Id = rnd.Next(1, 10000).ToString();
        }
    }
}