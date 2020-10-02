using System.Collections.Generic;

namespace bank.domain.core
{
    public class CDTService : IFinanceService
    {
        public static int TRIMESTER = 90;
        public static int SEMESTER = 180;
        public static int ANNUAL = 360;
        private decimal TEA { get => 0.06m; }
        public int termin { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public decimal Balance { get; private set; }
        private readonly List<BankAccountMovement> _movimientos;
        public CDTService(string number, string city, int Termin)
        {
            Number = number;
            City = city;
            Balance = 0;
            this.termin = Termin;
            _movimientos = new List<BankAccountMovement>();
        }

        public string Consign(decimal value, IDates date, string city = "")
        {
            if(value < 1000000) return "Solo se acepta 1M como mínimo";
            if(haveConsign() > 0) return "Solo se puede realizar una consignacion";
            var balanceOld = Balance;
            Balance += value;
            saveMovement(new BankAccountMovement(balanceOld, value, 0, BankAccountMovement.CONSIGNCDT, date));
            return "Se realizo la consignacion";
        }
        public string Takes(decimal takeQuantity, IDates date)
        {
            return "";
        }
        protected void saveMovement(BankAccountMovement movement)
        {    
            _movimientos.Add(movement);
        }

        protected int haveConsign()
        {    
            return _movimientos.FindAll(t => t.type == BankAccountMovement.CONSIGNCDT).Count;
        }
        
    }
}