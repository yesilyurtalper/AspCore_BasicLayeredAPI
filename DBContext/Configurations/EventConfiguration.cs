
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BasicLayeredService.API.Domain;
using Microsoft.Extensions.Hosting;

namespace BasicLayeredService.API.DBContext.Configurations;

public class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasIndex(u => u.Author);
        builder.HasIndex(u => u.Date);

        for (int i = 1; i <= 1000; i++)
        {
            Event ev = new()
            {
                Id = Guid.NewGuid(),
                Author = $"author{i}",
                Title = $"name{i}",
                Body = $@"descr{i} descr{i} descr{i} descr{i} descr{i} descr{i}
                    descr{i}",
                DateCreated = DateTime.Now.AddDays(i%2 == 0 ? i : -i) ,
                Price = i,
                Capacity = i
            };

            ev.DateModified = ev.DateCreated;
            ev.Date = ev.DateCreated;

            builder.HasData(ev);
        }
    }
}
