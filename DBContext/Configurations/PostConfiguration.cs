﻿
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BasicLayeredAPI.API.Domain;

namespace BasicLayeredAPI.API.DBContext.Configurations;

internal class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasIndex(u => u.Author);

        Post postA = new Post
        {
            Id = 1,
            Author = "alpery",
            Description = "Some user post by alpery",
            Title = "postA",
            DateCreated = DateTime.Now,
            DateModified = DateTime.Now,
        };

        Post postB = new Post
        {
            Id = 1,
            Author = "alpery",
            Description = "Some user post by alpery",
            Title = "postB",
            DateCreated = DateTime.Now,
            DateModified = DateTime.Now,
        };

        Post postC = new Post
        {
            Id = 1,
            Author = "alpery",
            Description = "Some user post by alpery",
            Title = "postC",
            DateCreated = DateTime.Now,
            DateModified = DateTime.Now,
        };

        builder.HasData(postA);
        builder.HasData(postB);
        builder.HasData(postC);
    }
}
