using Microsoft.EntityFrameworkCore;
using tk_web.DAL.Interfaces;
using tk_web.Domain.Models;

namespace tk_web.DAL.Repositories
{
    public class BookingRepository : IBaseRepository<Booking>
    {

        private readonly TkEquipmentBdContext database;

        public BookingRepository(TkEquipmentBdContext database)
        {
            this.database = database;
        }

        public async Task Create(Booking obj)
        {
            await database.Bookings.AddAsync(obj);
            await database.SaveChangesAsync();
        }

        public IQueryable<Booking> GetAll()
        {
            return database.Bookings.Include(booking => booking.Equipment)
            .Include(booking => booking.Event)
            .Include(booking => booking.Participant);
        }

        public async Task Delete(Booking obj)
        {
            database.Bookings.Remove(obj);
            await database.SaveChangesAsync();
        }

        public async Task<Booking> Update(Booking obj)
        {
            database.Bookings.Update(obj);
            await database.SaveChangesAsync();

            return obj;
        }
    }
}

