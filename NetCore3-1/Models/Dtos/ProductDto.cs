using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;

namespace NetCore3_1.Models.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }

        public string Summary { get; set; }

        public decimal Price { get; set; }

        public string Thumbnail { get; set; }

    }
}
