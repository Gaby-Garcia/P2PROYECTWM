using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Http;

namespace Proyect3TWN.WebSite.Services.Interfaces;

public interface IPerfomanceReviewService
{
    Task<Response<List<PerfomanceReviewsDto>>> GetAllAsync();

    Task<Response<PerfomanceReviewsDto>> GetById(int id);

    Task<Response<PerfomanceReviewsDto>> SaveAsync(PerfomanceReviewsDto perfomanceReviewsDto);

    Task<Response<PerfomanceReviewsDto>> UpdateAsync(PerfomanceReviewsDto perfomanceReviewsDto);

    Task<Response<bool>> DeleteAsync(int id);
}