namespace HFA.DB.Interfaces
{
    public interface IUnitOfWorks : IDisposable
    {
        int Complete();
    }
}