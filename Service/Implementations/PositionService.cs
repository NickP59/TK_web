using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using tk_web.DAL.Interfaces;
using tk_web.DAL.Repositories;
using tk_web.Domain.Enum;
using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Domain.ViewModels.Group;
using tk_web.Domain.ViewModels.Position;
using tk_web.Service.Interfaces;

namespace tk_web.Service.Implementations
{
    public class PositionService : IPositionService
    {
        private readonly IBaseRepository<Position_> _positionRepository;

        public PositionService(IBaseRepository<Position_> positionRepository)
        {
            _positionRepository = positionRepository;
        }

        public IBaseResponse<List<Position_>> GetPositions()
        {

            try
            {
                var positions = _positionRepository.GetAll().ToList();
                if (!positions.Any())
                {
                    return new BaseResponse<List<Position_>>()
                    {
                        Description = "найдена 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Position_>>()
                {
                    Data = positions,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Position_>>()
                {
                    Description = $"[GetPositions] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<PositionViewModel>> GetPosition(int id)
        {
            try
            {
                var position = await _positionRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (position == null)
                {
                    return new BaseResponse<PositionViewModel>()
                    {
                        Description = "Должность не найдена",
                        StatusCode = StatusCode.PositionNotFound
                    };
                }

                var data = new PositionViewModel()
                {
                    Name = position.Name
                };

                return new BaseResponse<PositionViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PositionViewModel>()
                {
                    Description = $"[GetPosition] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Position_>> CreatePosition(PositionViewModel positionViewModel)
        {
            try
            {
                var position = new Position_()
                {
                    Name = positionViewModel.Name
                };

                await _positionRepository.Create(position);

                return new BaseResponse<Position_>()
                {
                    Data = position,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Position_>()
                {
                    Description = $"[CreatePosition] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeletePosition(int id)
        {
            try
            {
                var position = await _positionRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (position == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Должность не найдена",
                        StatusCode = StatusCode.PositionNotFound,
                        Data = false
                    };
                }

                await _positionRepository.Delete(position);

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
                    Description = $"[DeletePosition] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }



        public async Task<IBaseResponse<Position_>> EditPosition(PositionViewModel model)
        {
            try
            {
                var position = await _positionRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                if (position == null)
                {
                    return new BaseResponse<Position_>()
                    {
                        Description = "Должность не найдена",
                        StatusCode = StatusCode.PositionNotFound
                    };
                }

                position.Name = model.Name;

                await _positionRepository.Update(position);

                return new BaseResponse<Position_>()
                {
                    Data = position,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Position_>()
                {
                    Description = $"[EditPosition] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Dictionary<int, string>>> GetPosition(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<int, string>>();
            try
            {
                var position = await _positionRepository.GetAll()
                    .Select(x => new PositionViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name                 
                    })
                    .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Name);

                baseResponse.Data = position;
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
