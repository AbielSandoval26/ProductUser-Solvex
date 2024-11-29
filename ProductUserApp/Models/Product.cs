namespace ProductUserApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImagenUrl { get; set; }
        public List<ProductVariant> Variaciones { get; set; }
    }
}
