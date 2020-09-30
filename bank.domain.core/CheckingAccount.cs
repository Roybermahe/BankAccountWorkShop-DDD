using System;

namespace bank.domain.core
{
    public class CheckingAccount : BankAccount
    {
        private decimal OverdraftQuota { get => 0.2m; }
        public decimal OverdraftBalance { get; private set; }
        public CheckingAccount(string name, string number, string city) : base(name, number, city)
        {
        }

        public override string Takes(decimal takeQuantity, string month, string year)
        {
            var result = "Se desconto dinero de su saldo actual";
            var balanceOld = Balance;
            Balance -= takeQuantity + ((Balance * 4) / 1000);
            if (Balance >= OverdraftBalance)
            {
                saveMovement(new BankAccountMovement(balanceOld, 0, takeQuantity, BankAccountMovement.TAKE, month, year));
                return result;   
            }
            Balance = balanceOld;
            result = "No puede retirar esta cantidad de dinero";
            return result;
        }

        public override string Consign(decimal consignQuantity, string month, string year)
        {
            var result = "No se aceptan menos de 100 mil para cuenta corriente";
            if (consignQuantity < 100000 && NoTieneConsignacion()) return result;
            var balanceOld = Balance;
            OverdraftBalance += consignQuantity * OverdraftQuota;
            Balance += consignQuantity;
            saveMovement(new BankAccountMovement(balanceOld, consignQuantity, 0, BankAccountMovement.CONSIGN, month, year));
            result = $"Se consignarón $ {consignQuantity:n2}, su cuenta tiene {Balance:n2}";
            return result;
        }
    }
}