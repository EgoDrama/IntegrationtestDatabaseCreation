namespace EndlessLobster.Domain.Repository
{
    public interface IRepository<T>
    {
        T Get(int id);
    }
}