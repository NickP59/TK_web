using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Domain.ViewModels.Group;

namespace tk_web.Service.Interfaces
{
    public interface IGroupService
    {
        IBaseResponse<List<Group>> GetGroups();

        Task<IBaseResponse<GroupViewModel>> GetGroup(int id);

        Task<IBaseResponse<Group>> CreateGroup(GroupViewModel carViewModel);

        Task<IBaseResponse<bool>> DeleteGroup(int id);

        Task<IBaseResponse<Group>> EditGroup(GroupViewModel model);

        Task<BaseResponse<Dictionary<int, string>>> GetGroup(string term);
    }
}
