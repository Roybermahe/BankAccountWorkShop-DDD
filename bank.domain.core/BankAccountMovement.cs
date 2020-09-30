using System;

namespace bank.domain.core
{
    public class BankAccountMovement
    {
        public static string CONSIGN = "CONSIGN";
        public static string TAKE = "TAKE";
        public decimal BalanceOld { get; private set; }
        public decimal ValueCredit { get; private set; }
        public decimal ValueDebit { get; private set; }
        
        public string month { get; private set; }
        public string year { get; private set; }
        public string type { get; private set; }
        
        

        public BankAccountMovement(decimal balanceOld, decimal valueCredit, decimal valueDebit, string type, string month, string year)
        {
            BalanceOld = balanceOld;
            ValueCredit = valueCredit;
            ValueDebit = valueDebit;
            this.type = type;
            this.month = month;
            this.year = year;
        }
    }
}