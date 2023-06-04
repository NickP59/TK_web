using Microsoft.EntityFrameworkCore;
using tk_web.DAL.Interfaces;
using tk_web.Domain.Models;

namespace tk_web.DAL.Repositories
{
    public class EventRepository : IBaseRepository<Event>
    {
        private readonly TkEquipmentBdContext database;

        public EventRepository(TkEquipmentBdContext database)
        {
            this.database = database;
        }

        public async Task Create(Event obj)
        {
            await database.Events.AddAsync(obj);
            await database.SaveChangesAsync();
        }

        public IQueryable<Event> GetAll()
        {
            return database.Events;
        }

        public async Task Delete(Event obj)
        {
            database.Events.Remove(obj);
            await database.SaveChangesAsync();
        }

        public async Task<Event> Update(Event obj)
        {
            database.Events.Update(obj);
            await database.SaveChangesAsync();

            return obj;
        }
    }
}
