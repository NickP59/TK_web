
using tk_web.DAL.Interfaces;
using tk_web.Domain.Models;
using tk_web.DAL.Repositories;
using tk_web.Service.Implementations;
using tk_web.Service.Interfaces;

namespace tk_web
{
    public static class Initilizer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddTransient<IBaseRepository<Booking>, BookingRepository>();
            services.AddTransient<IBaseRepository<Event>, EventRepository>();
            services.AddTransient<IBaseRepository<Equipment>, EquipmentRepository>();
            services.AddTransient<IBaseRepository<Participant>, ParticipantRepository>();
            services.AddTransient<IBaseRepository<EquipmentType>, EquipmentTypeRepository>();
            services.AddTransient<IBaseRepository<EquipmentPlace>, EquipmentPlaceRepository>();
            services.AddTransient<IBaseRepository<Position_>, PositionRepository>();
            services.AddTransient<IBaseRepository<Group>, GroupRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddTransient<IBookingService, BookingService>();
            services.AddTransient<IEquipmentService, EquipmentService>();
            services.AddTransient<IParticipantService, ParticipantService>();
            services.AddTransient<IEventService, EventService>();
            services.AddTransient<IEquipmentTypeService, EquipmentTypeService>();
            services.AddTransient<IEquipmentPlaceService, EquipmentPlaceService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<IGroupService, GroupService>();
        }
    }
}
