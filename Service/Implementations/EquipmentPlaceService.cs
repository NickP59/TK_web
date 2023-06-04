using Microsoft.EntityFrameworkCore;
using tk_web.DAL.Interfaces;
using tk_web.DAL.Repositories;
using tk_web.Domain.Enum;
using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Domain.ViewModels.Equipment;
using tk_web.Domain.ViewModels.EquipmentPlace;
using tk_web.Domain.ViewModels.EquipmentType;
using tk_web.Service.Interfaces;

namespace tk_web.Service.Implementations
{
    public class EquipmentPlaceService : IEquipmentPlaceService
    {
        private readonly IBaseRepository<EquipmentPlace> _equipmentPlaceRepository;

        public EquipmentPlaceService(IBaseRepository<EquipmentPlace> equipmentPlaceRepository)
        {
            _equipmentPlaceRepository = equipmentPlaceRepository;
        }

        public IBaseResponse<List<EquipmentPlace>> GetEquipmentPlaces()
        {

            try
            {
                var equipmentPlaces = _equipmentPlaceRepository.GetAll().ToList();
                if (!equipmentPlaces.Any())
                {
                    return new BaseResponse<List<EquipmentPlace>>()
                    {
                        Description = "Найдено 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<EquipmentPlace>>()
                {
                    Data = equipmentPlaces,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<EquipmentPlace>>()
                {
                    Description = $"[GetEquipmentPlaces] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<EquipmentPlaceViewModel>> GetEquipmentPlace(int id)
        {
            try
            {
                var equipmentPlace = await _equipmentPlaceRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (equipmentPlace == null)
                {
                    return new BaseResponse<EquipmentPlaceViewModel>()
                    {
                        Description = "Склад не найден",
                        StatusCode = StatusCode.EquipmentPlaceNotFound
                    };
                }

                var data = new EquipmentPlaceViewModel()
                {
                    Name = equipmentPlace.Name
                };

                return new BaseResponse<EquipmentPlaceViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<EquipmentPlaceViewModel>()
                {
                    Description = $"[GetEquipmentPlace] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<EquipmentPlace>> CreateEquipmentPlace(EquipmentPlaceViewModel equipmentPlaceViewModel)
        {
            try
            {
                var equipmentPlace = new EquipmentPlace()
                {
                    Name = equipmentPlaceViewModel.Name
                };

                await _equipmentPlaceRepository.Create(equipmentPlace);

                return new BaseResponse<EquipmentPlace>()
                {
                    Data = equipmentPlace,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<EquipmentPlace>()
                {
                    Description = $"[CreateEquipmentPlace] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteEquipmentPlace(int id)
        {
            try
            {
                var equipmentPlace = await _equipmentPlaceRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (equipmentPlace == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Склад не найден",
                        StatusCode = StatusCode.EquipmentPlaceNotFound,
                        Data = false
                    };
                }

                await _equipmentPlaceRepository.Delete(equipmentPlace);

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
                    Description = $"[DeleteEquipmentPlace] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }



        public async Task<IBaseResponse<EquipmentPlace>> EditEquipmentPlace(EquipmentPlaceViewModel model)
        {
            try
            {
                var equipmentPlace = await _equipmentPlaceRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                if (equipmentPlace == null)
                {
                    return new BaseResponse<EquipmentPlace>()
                    {
                        Description = "Склад не найден",
                        StatusCode = StatusCode.EquipmentPlaceNotFound
                    };
                }

                equipmentPlace.Name = model.Name;

                await _equipmentPlaceRepository.Update(equipmentPlace);

                return new BaseResponse<EquipmentPlace>()
                {
                    Data = equipmentPlace,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<EquipmentPlace>()
                {
                    Description = $"[EditEquipmentPlace] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Dictionary<int, string>>> GetEquipmentPlace(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<int, string>>();
            try
            {
                var places = await _equipmentPlaceRepository.GetAll()
                    .Select(x => new EquipmentPlaceViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                    .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Name);

                baseResponse.Data = places;
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
