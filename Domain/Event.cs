﻿using System.ComponentModel.DataAnnotations;

namespace BasicLayeredService.API.Domain;

public class Event : BaseItem
{
    [Required]
    [MaxLength(100)]
    public int Capacity{ get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public DateTime Date { get; set; }
}
