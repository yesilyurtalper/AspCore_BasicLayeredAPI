
using BasicLayeredService.API.Domain;

namespace BasicLayeredService.API.Contracts.Persistence;

public interface IPostRepo
{
    Task<List<Post>> GetLatestAsync(int count);
    Task<Post> GetByIdAsync(int id);
    Task<List<Post>> GetByAuthorAsync(string author);
    Task CreateAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(Post post);
}
