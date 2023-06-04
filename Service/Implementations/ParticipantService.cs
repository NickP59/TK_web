using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using tk_web.DAL.Interfaces;
using tk_web.Domain.Enum;
using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Domain.ViewModels.Participant;
using tk_web.Service.Interfaces;

namespace tk_web.Service.Implementations
{
    public class ParticipantService : IParticipantService
    {
        private readonly IBaseRepository<Participant> _participantRepository;

        public ParticipantService(IBaseRepository<Participant> participantRepository)
        {
            _participantRepository = participantRepository;
        }

        public IBaseResponse<List<Participant>> GetParticipants()
        {

            try
            {
                var participants = _participantRepository.GetAll().ToList();
                if (!participants.Any())
                {
                    return new BaseResponse<List<Participant>>()
                    {
                        Description = "найден 0 элементов",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Participant>>()
                {
                    Data = participants,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Participant>>()
                {
                    Description = $"[GetParticipants] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<ParticipantViewModel>> GetParticipant(int id)
        {
            try
            {
                var participant = await _participantRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (participant == null)
                {
                    return new BaseResponse<ParticipantViewModel>()
                    {
                        Description = "Участник не найден",
                        StatusCode = StatusCode.ParticipantNotFound
                    };
                }

                var data = new ParticipantViewModel()
                {
                    FullName = participant.FullName,
                    GroupId = participant.GroupId,
                    PositionId = participant.PositionId,
                    SocialNetworkLink = participant.SocialNetworkLink,
                    PhoneNumber = participant.PhoneNumber
                };

                return new BaseResponse<ParticipantViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ParticipantViewModel>()
                {
                    Description = $"[GetParticipant] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Participant>> CreateParticipant(ParticipantViewModel ParticipantViewModel)
        {
            try
            {
                var participant = new Participant()
                {
                    FullName = ParticipantViewModel.FullName,
                    GroupId = ParticipantViewModel.GroupId,
                    PositionId = ParticipantViewModel.PositionId,
                    SocialNetworkLink = ParticipantViewModel.SocialNetworkLink,
                    PhoneNumber = ParticipantViewModel.PhoneNumber   
                };

                await _participantRepository.Create(participant);

                return new BaseResponse<Participant>()
                {
                    Data = participant,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Participant>()
                {
                    Description = $"[CreateParticipant] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteParticipant(int id)
        {
            try
            {
                var participant = await _participantRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (participant == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Участник не найден",
                        StatusCode = StatusCode.ParticipantNotFound,
                        Data = false
                    };
                }

                await _participantRepository.Delete(participant);

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
                    Description = $"[DeleteParticipant] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }



        public async Task<IBaseResponse<Participant>> EditParticipant(ParticipantViewModel model)
        {
            try
            {
                var participant = await _participantRepository.GetAll().FirstOrDefaultAsync(x => x.Id == model.Id);
                if (participant == null)
                {
                    return new BaseResponse<Participant>()
                    {
                        Description = "Участник не найден",
                        StatusCode = StatusCode.ParticipantNotFound
                    };
                }

                participant.FullName = model.FullName;
                participant.GroupId = model.GroupId;
                participant.PositionId = model.PositionId;
                participant.SocialNetworkLink = model.SocialNetworkLink;
                participant.PhoneNumber = model.PhoneNumber;
                
                await _participantRepository.Update(participant);

                return new BaseResponse<Participant>()
                {
                    Data = participant,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Participant>()
                {
                    Description = $"[EditParticipant] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        //public BaseResponse<Dictionary<int, string>> GetParticipantsNames()
        //{
        //    try
        //    {
        //        var names = _participantRepository.GetAll()
        //            .ToDictionary(id => id.Id, fn => fn.FullName);

        //        return new BaseResponse<Dictionary<int, string>>()
        //        {
        //            Data = names,
        //            StatusCode = StatusCode.OK
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<Dictionary<int, string>>()
        //        {
        //            Description = ex.Message,
        //            StatusCode = StatusCode.InternalServerError
        //        };
        //    }
        //}

        public async Task<BaseResponse<Dictionary<int, string>>> GetParticipant(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<int, string>>();
            try
            {
                var participants = await _participantRepository.GetAll().Include(participants => participants.Group).Include(participants => participants.Position)
                    .Select(x => new ParticipantViewModel()
                    {
                        Id = x.Id,
                        FullName = x.FullName,
                        GroupId = x.GroupId,
                        PositionId = x.PositionId,
                        SocialNetworkLink = x.SocialNetworkLink,
                        PhoneNumber = x.PhoneNumber
                    })
                    .Where(x => EF.Functions.Like(x.FullName, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.FullName);

                baseResponse.Data = participants;
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
