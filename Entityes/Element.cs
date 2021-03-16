using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WPFEntityCoreExample.Entityes
{
    public class Element
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public double Price { get; set; } = new Random().Next();

        public double Scale { get; set; } = 2; // Значение по умолчанию

        [NotMapped]
        public double TotalPrice { get => Price * Scale; } // Не добавлять в БД

        public int CategoryId { get; set; } // внешний ключ


        public Category Category { get; set; }  // навигационное свойство

        public string CategoryName { get => Category?.Name; }


        // Скомпилированный запрос
        internal static Func<Context, string, IEnumerable<Element>> ElementsByCategories =
            EF.CompileQuery((Context db, string name) =>
            db.Elements.Include(c => c.Category)
            .Where(u => EF.Functions.Like(u.Category.Name, name)));
    }
}
