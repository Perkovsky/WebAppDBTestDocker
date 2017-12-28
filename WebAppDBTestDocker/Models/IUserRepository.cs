using System.Linq;

namespace WebAppDBTestDocker.Models {

    public interface IUserRepository
    {

        IQueryable<User> Users { get; }

        void SaveUser(User user);
        User DeleteUser(int id);
    }
}
