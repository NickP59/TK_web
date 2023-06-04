using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Domain.ViewModels.Equipment;

namespace tk_web.Service.Interfaces
{
    public interface IEquipmentService
    {
        IBaseResponse<List<Equipment>> GetEquipments();

        Task<IBaseResponse<EquipmentViewModel>> GetEquipment(int id);

        Task<IBaseResponse<Equipment>> CreateEquipment(EquipmentViewModel carViewModel);

        Task<IBaseResponse<bool>> DeleteEquipment(int id);

        Task<IBaseResponse<Equipment>> EditEquipment(EquipmentViewModel model);

        Task<BaseResponse<Dictionary<int, string>>> GetEquipment(string term);
    }
}
