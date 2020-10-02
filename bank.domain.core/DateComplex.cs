using System;

namespace bank.domain.core
{
    public class DateComplex : IDates
    {
        private DateTime date { get; set; }
        public DateComplex(string date)
        {
            this.date = DateTime.ParseExact(date, "dd/MM/yyyy", null);
        }

        public string getMonth()
        {
            return this.date.ToString("MMMMM");
        }

        public string getDay()
        {
           return this.date.ToString("dd");
        }

        public string getYear()
        {
            return this.date.ToString("YYYY");
        }

        public int getSemestre()
        {
           return this.date.Month > 6 ? 2 : 1;
        }

        public int getTrimestre()
        {
            var month = this.date.Month;
            if(month <= 3) {
                return 1;
            } else if (month > 3 && month <= 6) {
                return 2;
            } else if (month > 6 && month <= 9) {
                return 3;
            } else {
                return 4;
            }
        }
    }
}