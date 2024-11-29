using ProductUserApp.Models;
using ProductUserApp.Services;
using ProductUserApp.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace ProductUserApp.ViewModels.Products
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private readonly IProductService _productService;

        public ObservableCollection<Product> Products { get; set; } = new();
        public event PropertyChangedEventHandler PropertyChanged;

        public ProductViewModel(IProductService productService)
        {
            _productService = productService;
        }

        public async Task LoadProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            Products = new ObservableCollection<Product>(products);
            OnPropertyChanged(nameof(Products));
        }

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
