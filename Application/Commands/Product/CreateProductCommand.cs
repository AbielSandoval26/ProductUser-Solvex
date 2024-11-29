using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Product
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
        public List<ProductVariationDTO> Variaciones { get; set; }
    }
}
