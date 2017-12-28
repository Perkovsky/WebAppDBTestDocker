using System.Linq;

namespace WebAppDBTestDocker.Models
{
    public class EFUserRepository : IUserRepository
    {
        private ApplicationDbContext context;

        public IQueryable<User> Users => context.Users;

        public EFUserRepository(ApplicationDbContext ctx) => context = ctx;

        public void SaveUser(User user)
        {
            if (user.Id == 0)
            {
                context.Users.Add(user);
            }
            else
            {
                User dbEntry = context.Users.FirstOrDefault(u => u.Id == user.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = user.Name;
                    dbEntry.Age = user.Age;
                    dbEntry.Phone = user.Phone;
                }
            }
            context.SaveChanges();
        }

        public User DeleteUser(int id)
        {
            User dbEntry = context.Users.FirstOrDefault(u => u.Id == id);
            if (dbEntry != null)
            {
                context.Users.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
