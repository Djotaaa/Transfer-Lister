using Transfer_ListerAPI.Data;
using Transfer_ListerAPI.Models;
using Transfer_ListerAPI.Repository.IRepository;

namespace Transfer_ListerAPI.Repository
{
    public class PositionRepository : Repository<Position>, IPositionRepository
    {
        public PositionRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
