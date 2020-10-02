namespace bank.domain.core
{
    public class CDTService : IFinanceService
    {
        public static string TRIMESTER = "TRIMESTER";
        public static string SEMESTER = "SEMESTER";
        public static string ANNUAL = "ANNUAL";

        private decimal TEA { get => 0.06m; }
        public string termin { get; private set; }
        public string Number { get; private set; }
        public string City { get; private set; }
        public decimal Balance { get; private set; }
        public CDTService(string number, string city, decimal balance, string Termin )
        {
            Number = number;
            City = city;
            Balance = balance;
            this.termin = Termin;
        }

        public string Consign(decimal value, IDates date, string city = "")
        {
            return "";
        } 

        public string Takes(decimal takeQuantity, IDates date)
        {
            return "";
        }
    }
}