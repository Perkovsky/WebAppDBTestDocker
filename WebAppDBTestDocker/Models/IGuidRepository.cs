using System.Linq;

namespace WebAppDBTestDocker.Models {

    public interface IGuidRepository
    {

        IQueryable<MyGuid> Guids { get; }

        void SaveGuid(MyGuid myGuid);
        MyGuid DeleteGuid(int id);
    }
}
