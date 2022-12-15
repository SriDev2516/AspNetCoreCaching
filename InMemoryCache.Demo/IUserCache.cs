namespace InMemoryCache.Demo
{
    public interface IUserCache
    {
        void AddToCache(User[] users);
        User[] GetAllUsers();
    }
}