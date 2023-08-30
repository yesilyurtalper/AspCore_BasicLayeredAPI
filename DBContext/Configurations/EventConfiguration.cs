
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

        for (int i = 1; i <= 500; i++)
        {
            Event ev = new();
            ev.Id = i;
            ev.Author = $"organizer{i}";
            ev.Title = $"name{i}";
            ev.Body = $"descr{i} " +
                $"descr{i} descr{i} " +
                $"descr{i} descr{i} descr{i} " +
                $"descr{i}";
            ev.DateCreated = DateTime.Now;
            ev.DateModified = ev.DateCreated;
            ev.Date = ev.DateCreated;
            ev.Price = 12.12;
            ev.Capacity = 12345;
            
            builder.HasData(ev);
        }
    }
}
