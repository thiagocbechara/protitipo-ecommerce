using Microsoft.AspNetCore.Mvc;
using PrototipoEcommerce.Domain.Carrinhos.Services;

namespace PrototipoEcommerce.Web.Controllers
{
    public class CarrinhoController : Controller
    {
        private readonly ICarrinhoService _carrinhoService;

        public CarrinhoController(ICarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        public async Task<IActionResult> Index()
        {
            var carrinho = await _carrinhoService.SelecionarAsync();
            return View(carrinho);
        }

        public async Task<IActionResult> RemoverDoCarrinho(long carrinhoId, long itemId)
        {
            await _carrinhoService.RemoverItemAsync(carrinhoId, itemId);
            return RedirectToAction(nameof(Index));
        }
    }
}
