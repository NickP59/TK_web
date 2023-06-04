using Microsoft.EntityFrameworkCore;
using tk_web.DAL.Interfaces;
using tk_web.DAL.Repositories;
using tk_web.Domain.Enum;
using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Domain.ViewModels.Equipment;
using tk_web.Domain.ViewModels.Group;
using tk_web.Service.Interfaces;

namespace tk_web.Service.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly IBaseRepository<Group> _groupRepository;

        public GroupService(IBaseRepository<Group> groupRepository)
        {
            _groupRepository = groupRepository;
        }

        public IBaseResponse<List<Group>> GetGroups()
        {

            try
            {
                var groups = _groupRepository.GetAll().ToList();
                if (!groups.Any())
                {
                    return new BaseResponse<List<Group>>()
                    {
                        Description = "найдена 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Group>>()
                {
                    Data = groups,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Group>>()
                {
                    Description = $"[GetGroups] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<GroupViewModel>> GetGroup(int id)
        {
            try
            {
                var group = await _groupRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (group == null)
                {
                    return new BaseResponse<GroupViewModel>()
                    {
                        Description = "Группа не найдена",
                        StatusCode = StatusCode.GroupNotFound
                    };
                }

                var data = new GroupViewModel()
                {
                    Name = group.Name
                };

                return new BaseResponse<GroupViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<GroupViewModel>()
                {
                    Description = $"[GetGroup] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Group>> CreateGroup(GroupViewModel groupViewModel)
        {
            try
            {
                var group = new Group()
                {
                    Name = groupViewModel.Name,
                };

                await _groupRepository.Create(group);

                return new BaseResponse<Group>()
                {
                    Data = group,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Group>()
                {
                    Description = $"[CreateGroup] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteGroup(int id)
        {
            try
            {
                var group = await _groupRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (group == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Группа не найдена",
                        StatusCode = StatusCode.GroupNotFound,
                        Data = false
                    };
                }

                await _groupRepository.Delete(group);

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
                    Description = $"[DeleteGroup] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }



        public async Task<IBaseResponse<Group>> EditGroup(GroupViewModel model)
        {
            try
            {
                var group = await _groupRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                if (group == null)
                {
                    return new BaseResponse<Group>()
                    {
                        Description = "Группа не найдена",
                        StatusCode = StatusCode.GroupNotFound
                    };
                }

                group.Name = model.Name;

                await _groupRepository.Update(group);

                return new BaseResponse<Group>()
                {
                    Data = group,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Group>()
                {
                    Description = $"[EditGroup] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Dictionary<int, string>>> GetGroup(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<int, string>>();
            try
            {
                var group = await _groupRepository.GetAll()
                    .Select(x => new GroupViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name
                    })
                    .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Name);

                baseResponse.Data = group;
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
