using Microsoft.EntityFrameworkCore;
using tk_web.DAL.Interfaces;
using tk_web.Domain.Models;

namespace tk_web.DAL.Repositories
{
    public class ParticipantRepository : IBaseRepository<Participant>
    {
        private readonly TkEquipmentBdContext database;

        public ParticipantRepository(TkEquipmentBdContext database)
        {
            this.database = database;
        }

        public async Task Create(Participant obj)
        {
            await database.Participants.AddAsync(obj);
            await database.SaveChangesAsync();
        }

        public IQueryable<Participant> GetAll()
        {
            return database.Participants.Include(participant => participant.Group);
        }

        public async Task Delete(Participant obj)
        {
            database.Participants.Remove(obj);
            await database.SaveChangesAsync();
        }

        public async Task<Participant> Update(Participant obj)
        {
            database.Participants.Update(obj);
            await database.SaveChangesAsync();

            return obj;
        }
    }
}
