using Microsoft.Extensions.Caching.Memory;
using System;

namespace InMemoryCache.Demo
{
    public class UserCache : IUserCache
    {
        private const string KEY = "user-cache";
        private readonly IMemoryCache memoryCache;

        public UserCache(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public void AddToCache(User[] users)
        {
            var option = new MemoryCacheEntryOptions()
            {
                SlidingExpiration = TimeSpan.FromSeconds(30),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(90)
            };
            //Get Data from the DB and cache it
            memoryCache.Set<User[]>(KEY, users, option);

        }

        public User[] GetAllUsers()
        {
            User[] users;

            if (!memoryCache.TryGetValue(KEY, out users))
            {
                users = new User[]
                {
                    new User(){UserName = "User1", Age = 20},
                    new User(){UserName = "User2", Age = 21},
                    new User(){UserName = "User3", Age = 23},
                };

                AddToCache(users);
            }

            return users;
        }

    }
}
