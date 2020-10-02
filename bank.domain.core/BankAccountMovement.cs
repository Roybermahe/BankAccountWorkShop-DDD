using System;

namespace bank.domain.core
{
    public class BankAccountMovement
    {
        public static string CONSIGN = "CONSIGN";
        public static string TAKE = "TAKE";
        public static string PAYMENT = "PAYMENT";
        public static string ADVANCE = "ADVANCE";
        public decimal BalanceOld { get; private set; }
        public decimal ValueCredit { get; private set; }
        public decimal ValueDebit { get; private set; }
        public string type { get; private set; }
        public IDates dates { get; private set; }

        public BankAccountMovement(decimal balanceOld, decimal valueCredit, decimal valueDebit, string type, IDates date)
        {
            BalanceOld = balanceOld;
            ValueCredit = valueCredit;
            ValueDebit = valueDebit;
            this.dates = date;
        }
    }
}