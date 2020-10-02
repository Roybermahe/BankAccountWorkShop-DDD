using System.Collections.Generic;
using System.Linq;

namespace bank.domain.core
{
    public abstract class BankAccount: IFinanceService
    {
        public string Name { get; }
        public string Number { get; }
        public string City { get; }
        public decimal Balance { get; protected set; }
        private readonly List<BankAccountMovement> _movimientos;

        public BankAccount(string name,string number, string city)
        {
            Name = name;
            Number = number;
            City = city;
            Balance = 0;
            _movimientos = new List<BankAccountMovement>();
        }
        
        protected bool NoTieneConsignacion() 
        {
            return !(_movimientos.Any(t => t.type ==  BankAccountMovement.CONSIGN));
        }
        protected int countTakeMonth(IDates date)
        {
            return _movimientos.FindAll(t =>
                    t.type == BankAccountMovement.TAKE 
                    && t.dates.getMonth() == date.getMonth()
                    && t.dates.getYear() == date.getYear())
                .Count;
        }
        protected void saveMovement(BankAccountMovement movement)
        {    
            _movimientos.Add(movement);
        }
        abstract public string Takes(decimal value, IDates date);
        abstract public string Consign(decimal takeQuantity, IDates date);
    }
}