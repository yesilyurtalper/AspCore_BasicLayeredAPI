
using BasicLayeredAPI.API.Domain;

namespace BasicLayeredAPI.API.Contracts.Persistence;

public interface IPostRepo
{
    Task<List<Post>> GetAllAsync();
    Task<Post> GetByIdAsync(int id);
    Task<List<Post>> GetByAuthorAsync(string author);
    Task CreateAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(Post post);
}
