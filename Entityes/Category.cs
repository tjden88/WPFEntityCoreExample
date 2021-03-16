﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WPFEntityCoreExample.Entityes
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Element> Elements { get; set; } // навигационное свойство
    }
}
