using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BasicLayeredService.API.DTOs;

public class BaseQueryDto
{
    public Guid? Id { get; set; } 
    public string? Title { get; set; }

    public string? Body { get; set; } 

    public string? Author { get; set; }
    public DateTime? DateCreatedStart { get; set; }

    public DateTime? DateCreatedEnd { get; set; }
    public DateTime? DateModifiedStart { get; set; }

    public DateTime? DateModifiedEnd { get; set; }

    public int PageSize { get; set; } = 5;
    public int PageNumber { get; set; } = 1;

    public int SortByTitle { get; set; }

    public int SortByAuthor { get; set; }

    public int SortByBody { get; set; }

    public int SortByDateCreated { get; set; }

    public int SortByDateModified { get; set; }
}