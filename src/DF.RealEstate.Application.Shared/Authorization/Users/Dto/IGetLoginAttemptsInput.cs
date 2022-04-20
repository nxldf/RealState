using Abp.Application.Services.Dto;

namespace DF.RealEstate.Authorization.Users.Dto
{
    public interface IGetLoginAttemptsInput: ISortedResultRequest
    {
        string Filter { get; set; }
    }
}