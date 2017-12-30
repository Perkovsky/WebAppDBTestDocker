using System.Linq;

namespace WebAppDBTestDocker.Models
{
    public class EFGuidRepository : IGuidRepository
    {
        private ApplicationDbContext context;

        public IQueryable<MyGuid> Guids => context.Guids;

        public EFGuidRepository(ApplicationDbContext ctx) => context = ctx;

        public void SaveGuid(MyGuid myGuid)
        {
            if (myGuid.Id == 0)
            {
                context.Guids.Add(myGuid);
            }
            else
            {
                MyGuid dbEntry = context.Guids.FirstOrDefault(g => g.Id == myGuid.Id);
                if (dbEntry != null)
                {
                    dbEntry.Guid = myGuid.Guid;
                }
            }
            context.SaveChanges();
        }

        public MyGuid DeleteGuid(int id)
        {
            MyGuid dbEntry = context.Guids.FirstOrDefault(g => g.Id == id);
            if (dbEntry != null)
            {
                context.Guids.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
