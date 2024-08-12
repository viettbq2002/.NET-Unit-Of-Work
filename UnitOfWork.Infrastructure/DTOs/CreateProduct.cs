using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Infrastructure.DTOs
{
    public class CreateProduct
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int Price { get; set; }

        public int Stock { get; set; }
    }
}
