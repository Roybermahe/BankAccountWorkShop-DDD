namespace bank.domain.core
{
    public interface IFinanceService
    {
        string Number { get; }
        string City { get; }
        decimal Balance { get; }
        string Consign(decimal value, string month, string year);
        string Takes(decimal takeQuantity, string month, string year);
    }
}