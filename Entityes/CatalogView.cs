using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFEntityCoreExample.Entityes
{
    public class CatalogView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public double TotalPrice { get; set; }


        public static List<CatalogView> SelectAll()
        {
            using Context db = new();
            var result = db.Elements.Include(c => c.Category)
                .Select(p => new CatalogView
                {
                    Id=p.Id,
                    Name = p.Name,
                    Category = p.CategoryName,
                    TotalPrice = p.TotalPrice
                });
            return result.ToList();
        }
    }
}
