
using BasicLayeredService.API.Domain;
using BasicLayeredService.API.DTOs;

namespace BasicLayeredService.API.Contracts.Persistence;

public interface IBaseItemRepo<TModel,TQuery> where TModel : BaseItem where TQuery : BaseQueryDto
{
    Task<QueryResult<TModel>> QueryAsync(TQuery dto);
    Task<TModel> GetByIdAsync(Guid id);
    Task<TModel> GetByIdAsNoTrackingAsync(Guid id);
    Task CreateAsync(TModel model);
    Task UpdateAsync(TModel model);
    Task DeleteAsync(TModel model);
}
