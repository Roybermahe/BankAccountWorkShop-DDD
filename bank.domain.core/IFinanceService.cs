namespace bank.domain.core
{
    public interface IFinanceService
    {
        string Number { get; }
        string City { get; }
        decimal Balance { get; }
        string Consign(decimal value, IDates date);
        string Takes(decimal takeQuantity, IDates date);
    }
}