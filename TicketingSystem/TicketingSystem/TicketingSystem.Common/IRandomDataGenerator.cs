namespace TicketingSystem.Common
{
    public interface IRandomDataGenerator
    {
        string GetRandomString(int min, int max);

        int GetRandomNumber(int min, int max);
    }
}
