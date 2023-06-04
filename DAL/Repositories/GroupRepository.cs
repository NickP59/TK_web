using Microsoft.EntityFrameworkCore;
using tk_web.DAL.Interfaces;
using tk_web.Domain.Models;

namespace tk_web.DAL.Repositories
{
    public class GroupRepository : IBaseRepository<Group>
    {
        private readonly TkEquipmentBdContext database;

        public GroupRepository(TkEquipmentBdContext database)
        {
            this.database = database;
        }

        public async Task Create(Group obj)
        {
            await database.Groups.AddAsync(obj);
            await database.SaveChangesAsync();
        }

        public IQueryable<Group> GetAll()
        {
            return database.Groups;
        }

        public async Task Delete(Group obj)
        {
            database.Groups.Remove(obj);
            await database.SaveChangesAsync();
        }

        public async Task<Group> Update(Group obj)
        {
            database.Groups.Update(obj);
            await database.SaveChangesAsync();

            return obj;
        }
    }
}
