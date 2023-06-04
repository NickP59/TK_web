using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Domain.ViewModels.Position;

namespace tk_web.Service.Interfaces
{
    public interface IPositionService
    {
        IBaseResponse<List<Position_>> GetPositions();

        Task<IBaseResponse<PositionViewModel>> GetPosition(int id);

        Task<IBaseResponse<Position_>> CreatePosition(PositionViewModel carViewModel);

        Task<IBaseResponse<bool>> DeletePosition(int id);

        Task<IBaseResponse<Position_>> EditPosition(PositionViewModel model);

        Task<BaseResponse<Dictionary<int, string>>> GetPosition(string term);
    }
}
