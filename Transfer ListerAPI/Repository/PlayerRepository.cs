using Transfer_ListerAPI.Data;
using Transfer_ListerAPI.Models;
using Transfer_ListerAPI.Repository.IRepository;

namespace Transfer_ListerAPI.Repository
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(ApplicationDbContext db) : base(db)
        {
        }
    }
}
