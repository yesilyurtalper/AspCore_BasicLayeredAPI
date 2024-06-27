
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
        builder.HasIndex(u => u.DateModified);
        builder.HasIndex(u => u.DateCreated);

        for (int i = 1; i <= 500; i++)
        {
            Post post = new()
            {
                Id = Guid.NewGuid(),
                Author = $"author{i}",
                Title = $"title{i}",
                Body = $"body{i} " +
                $"body{i} body{i} " +
                $"body{i} body{i} body{i} " +
                $"body{i}",
                DateCreated = DateTime.Now.AddDays(i%2 == 0 ? i : -i),
            };

            post.DateModified = post.DateCreated;

            builder.HasData(post);
        }
    }
}
