﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductWithVariationsView
    {
        public int ProductoId { get; set; }
        public string Producto { get; set; }
        public string Color { get; set; }
        public decimal Precio { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
