using Microsoft.EntityFrameworkCore;
using tk_web.DAL.Interfaces;
using tk_web.Domain.Models;

namespace tk_web.DAL.Repositories
{
    public class EquipmentPlaceRepository : IBaseRepository<EquipmentPlace>
    {
        private readonly TkEquipmentBdContext database;

        public EquipmentPlaceRepository(TkEquipmentBdContext database)
        {
            this.database = database;
        }

        public async Task Create(EquipmentPlace obj)
        {
            await database.EquipmentPlaces.AddAsync(obj);
            await database.SaveChangesAsync();
        }

        public IQueryable<EquipmentPlace> GetAll()
        {
            return database.EquipmentPlaces;
        }

        public async Task Delete(EquipmentPlace obj)
        {
            database.EquipmentPlaces.Remove(obj);
            await database.SaveChangesAsync();
        }

        public async Task<EquipmentPlace> Update(EquipmentPlace obj)
        {
            database.EquipmentPlaces.Update(obj);
            await database.SaveChangesAsync();

            return obj;
        }
    }
}
