﻿using System.ComponentModel.DataAnnotations;

namespace SharedKernel
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}