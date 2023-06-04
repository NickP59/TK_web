using Microsoft.EntityFrameworkCore;
using tk_web.DAL.Interfaces;
using tk_web.Domain.Models;

namespace tk_web.DAL.Repositories
{
    public class EquipmentRepository : IBaseRepository<Equipment>
    {
        private readonly TkEquipmentBdContext database;

        public EquipmentRepository(TkEquipmentBdContext database)
        {
            this.database = database;
        }

        public async Task Create(Equipment obj)
        {
            await database.Equipment.AddAsync(obj);
            await database.SaveChangesAsync();
        }

        public IQueryable<Equipment> GetAll()
        {
            return database.Equipment.Include(equipment => equipment.Type)
            .Include(equipment => equipment.Place);
        }

        public async Task Delete(Equipment obj)
        {
            database.Equipment.Remove(obj);
            await database.SaveChangesAsync();
        }

        public async Task<Equipment> Update(Equipment obj)
        {
            database.Equipment.Update(obj);
            await database.SaveChangesAsync();

            return obj;
        } 
    }
}
