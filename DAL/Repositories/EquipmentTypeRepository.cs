using Microsoft.EntityFrameworkCore;
using tk_web.DAL.Interfaces;
using tk_web.Domain.Models;

namespace tk_web.DAL.Repositories
{
    public class EquipmentTypeRepository : IBaseRepository<EquipmentType>
    {
        private readonly TkEquipmentBdContext database;

        public EquipmentTypeRepository(TkEquipmentBdContext database)
        {
            this.database = database;
        }

        public async Task Create(EquipmentType obj)
        {
            await database.EquipmentTypes.AddAsync(obj);
            await database.SaveChangesAsync();
        }

        public IQueryable<EquipmentType> GetAll()
        {
            return database.EquipmentTypes;
        }

        public async Task Delete(EquipmentType obj)
        {
            database.EquipmentTypes.Remove(obj);
            await database.SaveChangesAsync();
        }

        public async Task<EquipmentType> Update(EquipmentType obj)
        {
            database.EquipmentTypes.Update(obj);
            await database.SaveChangesAsync();

            return obj;
        }
    }
}
