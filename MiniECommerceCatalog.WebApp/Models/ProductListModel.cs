using Microsoft.AspNetCore.Mvc.RazorPages;
using MiniECommerceCatalog.Data.Models;
using MiniECommerceCatalog.Data.Repositories;

namespace MiniECommerceCatalog.WebApp.Models
{
    public class ProductListModel(IProductRepository repository) : PageModel
    {
        private readonly IProductRepository _repository = repository;

        public List<Product> Products { get; private set; }

        public async Task OnGetAsync()
        {
            Products = (await _repository.GetAllAsync()).ToList();
        }
    }
}
