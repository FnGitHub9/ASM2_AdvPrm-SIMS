﻿using System.ComponentModel.DataAnnotations;

namespace ASM2_AdvPrm_SIMS.Models
{
    public class Admin
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string email { get; set; }
    }
}