using System.Collections.Generic;
using System.Linq;

namespace bank.domain.core
{
    public class CreditCard : IFinanceService
    {
        public string Name { get; }
        public string Number { get; }
        public string City { get; }
        public decimal Balance { get; protected set; }
        public decimal Quota { get; protected set; }

        private readonly List<BankAccountMovement> _movimientos;

        public CreditCard(string name, string number, string city, decimal quota)
        {
            Name = name;
            Number = number;
            City = city;
            Balance = quota;
            Quota = quota;
            _movimientos = new List<BankAccountMovement>();
        }

        public string Consign(decimal value, IDates dates)
        {
            var result = "Se realizo el abono a la cuenta";
            if (value <= 0 || Balance < value)
                return "El valor del abono no esta permitido";
            var balanceOld = Balance;
            Quota += (value - (Balance - Quota));
            Balance = Quota;
            this._movimientos.Add(new BankAccountMovement(balanceOld, value, 0, BankAccountMovement.PAYMENT, dates));
            return result;
        }

        public string Takes(decimal takeQuantity, IDates dates)
        {
            if( takeQuantity <= 0 || takeQuantity > Quota) 
                return "Valor del avance no esta permitido";
            Balance -= takeQuantity;
            return $"Se realizo el avance";
        }

    }
}