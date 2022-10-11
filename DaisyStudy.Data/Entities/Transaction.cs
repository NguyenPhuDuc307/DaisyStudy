using System;
using System.Transactions;

namespace DaisyStudy.Data.Entities
{
    public class Transaction
    {
        public int Transaction_ID { set; get; }
        public DateTime TransactionDate { set; get; }
        public string ExternalTransactionID { set; get; }
        public decimal Amount { set; get; }
        public decimal Fee { set; get; }
        public string Result { set; get; }
        public string Message { set; get; }
        public TransactionStatus Status { set; get; }
        public string Provider { set; get; }

    }
}

