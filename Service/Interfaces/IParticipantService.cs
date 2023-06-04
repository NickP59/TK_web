using tk_web.Domain.Models;
using tk_web.Domain.Response;
using tk_web.Domain.ViewModels.Participant;

namespace tk_web.Service.Interfaces
{
    public interface IParticipantService
    {
        IBaseResponse<List<Participant>> GetParticipants();

        Task<IBaseResponse<ParticipantViewModel>> GetParticipant(int id);

        Task<IBaseResponse<Participant>> CreateParticipant(ParticipantViewModel carViewModel);

        Task<IBaseResponse<bool>> DeleteParticipant(int id);

        Task<IBaseResponse<Participant>> EditParticipant(ParticipantViewModel model);

        //BaseResponse<Dictionary<int, string>> GetParticipantsNames();

        Task<BaseResponse<Dictionary<int, string>>> GetParticipant(string term);
    }
}
