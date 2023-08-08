﻿using System.ComponentModel.DataAnnotations;

namespace BasicLayeredAPI.API.Domain;

public class Post
{
    [Required]
    public int Id { get; set; }
    public string? Title { get; set; }

    [Required]
    [MaxLength(10000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Author { get; set; } = string.Empty;

    [Required]
    public DateTime DateCreated { get; set; }

    [Required]
    public DateTime DateModified { get; set; }
}
