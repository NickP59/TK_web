using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Domain.ViewModels.EquipmentType;

namespace tk_web.Service.Interfaces
{
    public interface IEquipmentTypeService
    {
        IBaseResponse<List<EquipmentType>> GetEquipmentTypes();

        Task<IBaseResponse<EquipmentTypeViewModel>> GetEquipmentType(int id);

        Task<IBaseResponse<EquipmentType>> CreateEquipmentType(EquipmentTypeViewModel carViewModel);

        Task<IBaseResponse<bool>> DeleteEquipmentType(int id);

        Task<IBaseResponse<EquipmentType>> EditEquipmentType(EquipmentTypeViewModel model);

        Task<BaseResponse<Dictionary<int, string>>> GetEquipmentType(string term);
    }
}
