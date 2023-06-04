using Microsoft.EntityFrameworkCore;
using tk_web.DAL.Interfaces;
using tk_web.DAL.Repositories;
using tk_web.Domain.Enum;
using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Domain.ViewModels.Equipment;
using tk_web.Domain.ViewModels.Event;
using tk_web.Service.Interfaces;

namespace tk_web.Service.Implementations
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IBaseRepository<Equipment> _equipmentRepository;

        public EquipmentService(IBaseRepository<Equipment> equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public IBaseResponse<List<Equipment>> GetEquipments()
        {
            try
            {
                var equipments = _equipmentRepository.GetAll().ToList();
                if (!equipments.Any())
                {
                    return new BaseResponse<List<Equipment>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Equipment>>()
                {
                    Data = equipments,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Equipment>>()
                {
                    Description = $"[GetEquipments] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<EquipmentViewModel>> GetEquipment(int id)
        {
            try
            {
                var equipment = await _equipmentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (equipment == null)
                {
                    return new BaseResponse<EquipmentViewModel>()
                    {
                        Description = "Снаряжение не найдено",
                        StatusCode = StatusCode.EquipmentNotFound
                    };
                }

                var data = new EquipmentViewModel()
                {
                    Name = equipment.Name
                };

                return new BaseResponse<EquipmentViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<EquipmentViewModel>()
                {
                    Description = $"[GetEquipment] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Equipment>> CreateEquipment(EquipmentViewModel equipmentViewModel)
        {
            try
            {
                var equipment = new Equipment()
                {
                    Name = equipmentViewModel.Name
                };

                await _equipmentRepository.Create(equipment);

                return new BaseResponse<Equipment>()
                {
                    Data = equipment,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Equipment>()
                {
                    Description = $"[CreateEquipment] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteEquipment(int id)
        {
            try
            {
                var equipment = await _equipmentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (equipment == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Снаряжение не найдено",
                        StatusCode = StatusCode.EquipmentNotFound,
                        Data = false
                    };
                }

                await _equipmentRepository.Delete(equipment);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteEquipment] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }



        public async Task<IBaseResponse<Equipment>> EditEquipment(EquipmentViewModel model)
        {
            try
            {
                var equipment = await _equipmentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                if (equipment == null)
                {
                    return new BaseResponse<Equipment>()
                    {
                        Description = "Снаряжение не найдено",
                        StatusCode = StatusCode.EquipmentNotFound
                    };
                }

                equipment.Name = model.Name;

                await _equipmentRepository.Update(equipment);

                return new BaseResponse<Equipment>()
                {
                    Data = equipment,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Equipment>()
                {
                    Description = $"[EditEquipment] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Dictionary<int, string>>> GetEquipment(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<int, string>>();
            try
            {
                var equipments = await _equipmentRepository.GetAll().Include(equipment => equipment.Type).Include(equipment => equipment.Place)
                    .Select(x => new EquipmentViewModel()
                    {
                        Id = x.Id,
                        TypeId = x.TypeId,
                        Name = x.Name,
                        Description = x.Description,
                        Notes = x.Notes,
                        PlaceId = x.PlaceId
                    })
                    .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Name);

                baseResponse.Data = equipments;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
