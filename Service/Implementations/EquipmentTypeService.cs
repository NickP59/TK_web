using Microsoft.EntityFrameworkCore;
using tk_web.DAL.Interfaces;
using tk_web.DAL.Repositories;
using tk_web.Domain.Enum;
using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Domain.ViewModels.EquipmentType;
using tk_web.Domain.ViewModels.Participant;
using tk_web.Service.Interfaces;

namespace tk_web.Service.Implementations
{
    public class EquipmentTypeService : IEquipmentTypeService
    {
        private readonly IBaseRepository<EquipmentType> _equipmentTypeRepository;

        public EquipmentTypeService(IBaseRepository<EquipmentType> equipmentTypeRepository)
        {
            _equipmentTypeRepository = equipmentTypeRepository;
        }

        public IBaseResponse<List<EquipmentType>> GetEquipmentTypes()
        {
            try
            {
                var equipmentTypes = _equipmentTypeRepository.GetAll().ToList();
                if (!equipmentTypes.Any())
                {
                    return new BaseResponse<List<EquipmentType>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<EquipmentType>>()
                {
                    Data = equipmentTypes,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<EquipmentType>>()
                {
                    Description = $"[GetEquipmentTypes] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<EquipmentTypeViewModel>> GetEquipmentType(int id)
        {
            try
            {
                var equipmentType = await _equipmentTypeRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (equipmentType == null)
                {
                    return new BaseResponse<EquipmentTypeViewModel>()
                    {
                        Description = "Тип не найден",
                        StatusCode = StatusCode.EquipmentTypeNotFound
                    };
                }

                var data = new EquipmentTypeViewModel()
                {
                    Type = equipmentType.Type
                };

                return new BaseResponse<EquipmentTypeViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<EquipmentTypeViewModel>()
                {
                    Description = $"[GetEquipmentType] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<EquipmentType>> CreateEquipmentType(EquipmentTypeViewModel equipmentTypeViewModel)
        {
            try
            {
                var equipmentType = new EquipmentType()
                {
                    Type = equipmentTypeViewModel.Type
                };

                await _equipmentTypeRepository.Create(equipmentType);

                return new BaseResponse<EquipmentType>()
                {
                    Data = equipmentType,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<EquipmentType>()
                {
                    Description = $"[CreateEquipmentType] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteEquipmentType(int id)
        {
            try
            {
                var equipmentType = await _equipmentTypeRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (equipmentType == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Тип не найден",
                        StatusCode = StatusCode.EquipmentTypeNotFound,
                        Data = false
                    };
                }

                await _equipmentTypeRepository.Delete(equipmentType);

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
                    Description = $"[DeleteEquipmentType] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }



        public async Task<IBaseResponse<EquipmentType>> EditEquipmentType(EquipmentTypeViewModel model)
        {
            try
            {
                var equipmentType = await _equipmentTypeRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                if (equipmentType == null)
                {
                    return new BaseResponse<EquipmentType>()
                    {
                        Description = "Тип не найден",
                        StatusCode = StatusCode.EquipmentTypeNotFound
                    };
                }

                equipmentType.Type = model.Type;

                await _equipmentTypeRepository.Update(equipmentType);

                return new BaseResponse<EquipmentType>()
                {
                    Data = equipmentType,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<EquipmentType>()
                {
                    Description = $"[EditEquipmentType] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Dictionary<int, string>>> GetEquipmentType(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<int, string>>();
            try
            {
                var types = await _equipmentTypeRepository.GetAll()
                    .Select(x => new EquipmentTypeViewModel()
                    {
                        Id = x.Id,
                        Type = x.Type
                    })
                    .Where(x => EF.Functions.Like(x.Type, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Type);

                baseResponse.Data = types;
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
