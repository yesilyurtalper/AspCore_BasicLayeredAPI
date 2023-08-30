
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BasicLayeredService.API.Domain;
using Microsoft.Extensions.Hosting;

namespace BasicLayeredService.API.DBContext.Configurations;

internal class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasIndex(u => u.Author);

        for(int i = 1; i <= 500; i++)
        {
            Post post = new();
            post.Id = i;
            post.Author = $"author{i}";
            post.Title = $"title{i}";
            post.Body = $"body{i} " +
                $"body{i} body{i} " +
                $"body{i} body{i} body{i} " +
                $"body{i}";
            post.DateCreated = DateTime.Now;
            post.DateModified = post.DateCreated;

            builder.HasData(post);
        }
    }
}
