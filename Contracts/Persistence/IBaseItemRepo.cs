
using BasicLayeredService.API.Domain;

namespace BasicLayeredService.API.Contracts.Persistence;

public interface IBaseItemRepo<TModel> where TModel : BaseItem
{
    Task<List<TModel>> GetLatestAsync(int count);
    Task<TModel> GetByIdAsync(int id);
    Task<TModel> GetByIdAsNoTrackingAsync(int id);
    Task<List<TModel>> GetByAuthorAsync(string author);
    Task CreateAsync(TModel model);
    Task UpdateAsync(TModel model);
    Task DeleteAsync(TModel model);
}
