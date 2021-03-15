using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFEntityCoreExample.Entityes
{
    public class Element
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public double Price { get; set; }

        public double Scale { get; set; } = 2; // Значение по умолчанию

        [NotMapped]
        public double TotalPrice { get => Price * Scale; } // Не добавлять в БД

        public int CategoryId { get; set; } // внешний ключ
        public Category Category { get; set; }  // навигационное свойство

        //public string CategoryName { get => Category?.Name; }
    }
}
