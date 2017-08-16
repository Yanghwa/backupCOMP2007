﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FirstApplication.Models
{
    public class BaseModel
    {
        [Display(Name = "Create Date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreateDate { get; set; }
        [Display(Name = "Edit Date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime EditDate { get; set; } = DateTime.UtcNow;
    }
}