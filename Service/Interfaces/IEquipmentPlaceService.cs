using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Domain.ViewModels.EquipmentPlace;

namespace tk_web.Service.Interfaces
{
    public interface IEquipmentPlaceService
    {
        IBaseResponse<List<EquipmentPlace>> GetEquipmentPlaces();

        Task<IBaseResponse<EquipmentPlaceViewModel>> GetEquipmentPlace(int id);

        Task<IBaseResponse<EquipmentPlace>> CreateEquipmentPlace(EquipmentPlaceViewModel carViewModel);

        Task<IBaseResponse<bool>> DeleteEquipmentPlace(int id);

        Task<IBaseResponse<EquipmentPlace>> EditEquipmentPlace(EquipmentPlaceViewModel model);

        Task<BaseResponse<Dictionary<int, string>>> GetEquipmentPlace(string term);


    }
}
