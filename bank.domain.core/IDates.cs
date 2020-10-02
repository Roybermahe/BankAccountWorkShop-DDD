namespace bank.domain.core
{
    public interface IDates
    {
        string getMonth();
        string getDay();
        string getYear();
        int getSemestre();
        int getTrimestre();
    }
}