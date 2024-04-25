using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.Dto;

namespace Proyect2TWM.Api.Services.Interfaces;

public interface IPerfomanceReviewsService
{
    Task<bool> PerfomanceReviewsExist(int id);

    Task<List<PerfomanceReviewsDto>> GetAllAsyncPR();


    Task<PerfomanceReviewsDto> SaveAsycnPR(PerfomanceReviewsDto perfomanceReviews);

    Task<PerfomanceReviewsDto> GetByIdPR(int id);

    Task<PerfomanceReviewsDto> UpdateAsyncPR(PerfomanceReviewsDto perfomanceReviews);

    Task<bool> DeleteAsyncPR(int id);
}