using Proyect2TWM.Api.Repositories.Interfecies;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.Dto;
using Proyect2TWM.Api.Services.Interfaces;

namespace Proyect2TWM.Api.Services;

public class PerfomanceReviewsService: IPerfomanceReviewsService
{
    private readonly IPerfomanceReviewsRepository _perfomanceReviews;

    public PerfomanceReviewsService(IPerfomanceReviewsRepository perfomanceReviews)
    {
        _perfomanceReviews = perfomanceReviews;
    }

    public async Task<bool> PerfomanceReviewsExist(int id)
    {
        var perfomanceReviews = await _perfomanceReviews.GetByIdPerfomance(id);
        return (perfomanceReviews != null);
    }

    public async Task<PerfomanceReviewsDto> SaveAsycnPR(PerfomanceReviewsDto perfomanceReviewsDto)
    {
        var perfomanceReviews = new PerfomanceReview
        {
            ID_Employee = perfomanceReviewsDto.ID_Employee,
            EvaluationDate = perfomanceReviewsDto.EvaluationDate,
            Clasification = perfomanceReviewsDto.Clasification,
            Comments = perfomanceReviewsDto.Comments,
            Goals = perfomanceReviewsDto.Goals,
            CreatedBy = "Gaby-Garcia",
            CreatedDate = DateTime.Now,
            UpdatedBy = "Gaby-Garcia",
            UpdatedDate = DateTime.Now
        };
        perfomanceReviews = await _perfomanceReviews.SaveAsycnPerfomance(perfomanceReviews);
        perfomanceReviewsDto.id = perfomanceReviews.id;

        return perfomanceReviewsDto;
    }

    public async Task<PerfomanceReviewsDto> UpdateAsyncPR(PerfomanceReviewsDto perfomanceReviewsDto)
    {
        var perfomanceReview = await _perfomanceReviews.GetByIdPerfomance(perfomanceReviewsDto.id);
        if (perfomanceReview == null)
            throw new Exception("Perfomance Review Not Found");
        
        perfomanceReview.ID_Employee = perfomanceReviewsDto.ID_Employee;
        perfomanceReview.EvaluationDate = perfomanceReviewsDto.EvaluationDate;
        perfomanceReview.Clasification = perfomanceReviewsDto.Clasification;
        perfomanceReview.Comments = perfomanceReviewsDto.Comments;
        perfomanceReview.Goals = perfomanceReviewsDto.Goals;
        perfomanceReview.UpdatedBy = "Gaby-Garcia";
        perfomanceReview.UpdatedDate = DateTime.Now;
        
        await _perfomanceReviews.UpdateAsyncPerfomance(perfomanceReview);
        return perfomanceReviewsDto;
    }

    public async Task<List<PerfomanceReviewsDto>> GetAllAsyncPR()
    {
        var perfomanceReview = await _perfomanceReviews.GetAllAsyncPerfomance();
        var perfomanceReviewDto = perfomanceReview.Select(c => new PerfomanceReviewsDto(c)).ToList();
        return perfomanceReviewDto;
    }

    public async Task<bool> DeleteAsyncPR(int id)
    {
        return await _perfomanceReviews.DeleteAsyncPerfomance(id);
    }

    public async Task<PerfomanceReviewsDto> GetByIdPR(int id)
    {
        var perfomanceReview = await _perfomanceReviews.GetByIdPerfomance(id);
        if (perfomanceReview == null)
            throw new Exception("Perfomance Review not found");

        var perfomanceReviewDto = new PerfomanceReviewsDto(perfomanceReview);
        return perfomanceReviewDto;
    }
}