using Microsoft.EntityFrameworkCore;
using tk_web.DAL.Interfaces;
using tk_web.Domain.Models;

namespace tk_web.DAL.Repositories
{
    public class PositionRepository : IBaseRepository<Position_>
    {
        private readonly TkEquipmentBdContext database;

        public PositionRepository(TkEquipmentBdContext database)
        {
            this.database = database;
        }

        public async Task Create(Position_ obj)
        {
            await database.Positions.AddAsync(obj);
            await database.SaveChangesAsync();
        }

        public IQueryable<Position_> GetAll()
        {
            return database.Positions;
        }

        public async Task Delete(Position_ obj)
        {
            database.Positions.Remove(obj);
            await database.SaveChangesAsync();
        }

        public async Task<Position_> Update(Position_ obj)
        {
            database.Positions.Update(obj);
            await database.SaveChangesAsync();

            return obj;
        }
    }
}
