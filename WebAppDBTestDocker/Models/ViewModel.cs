using System.Linq;

namespace WebAppDBTestDocker.Models
{
    public class ViewModel
    {
        public IQueryable<User> Users { get; set; }
        public IQueryable<MyGuid> Guids { get; set; }

        public ViewModel(IQueryable<User> users, IQueryable<MyGuid> guids)
        {
            Users = users;
            Guids = guids;
        }
    }
}
