using System;

namespace bank.domain.core
{
    public class SavingsAccount: BankAccount
    {

        public SavingsAccount(string name, string number, string city) : base(name, number, city)
        {
            
        }

        public override string Takes(decimal takeQuantity, string month, string year)
        {
            var result = "No tiene suficiente saldo para realizar un retiro";
            if (Balance < 20000) return result;
            var balanceOld = Balance;
            Balance -= takeQuantity;
            if (countTakeMonth(month, year) >= 3)
                Balance -= 5000;
            saveMovement(new BankAccountMovement(balanceOld, 0, takeQuantity, BankAccountMovement.TAKE, month, year));
            result = $"Usted acaba de retirar $ {takeQuantity:n2} de su cuenta de ahorros, Saldo restante {Balance}";
            return result;
        }

        public override string Consign(decimal consignQuantity, string month, string year)
        {
            if (consignQuantity <= 0) return "El valor a consignar es incorrecto";
            if (consignQuantity < 50000 && NoTieneConsignacion()) 
                return "El valor mínimo de la primera consignación debe ser de $50.000 mil pesos. Su nuevo saldo es $0 pesos";
            var balanceOld = Balance;
            Balance += consignQuantity;
            saveMovement(new BankAccountMovement(balanceOld, consignQuantity, 0, BankAccountMovement.CONSIGN, month, year));
            return $"Su Nuevo Saldo es de ${Balance:n2} pesos m/c";
        }
    }
}