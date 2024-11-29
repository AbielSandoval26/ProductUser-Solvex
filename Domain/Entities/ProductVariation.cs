using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("ProductVariations")]
    public class ProductVariation
    {
        public int Id { get; set; }
        public int ProductoId { get; set; }
        public string Color { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
