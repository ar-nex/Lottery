using System;

namespace Lottery_v2.Model
{
    public struct CustDueDetails
    {
        public string CustId;
        public decimal CurrDue;
        public DateTime UpdateTime;

        public CustDueDetails(string cid, decimal due, DateTime dt)
        {
            CustId = cid;
            CurrDue = due;
            UpdateTime = dt;
        }
    }
}