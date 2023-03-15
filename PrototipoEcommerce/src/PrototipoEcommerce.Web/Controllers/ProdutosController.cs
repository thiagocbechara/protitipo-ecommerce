using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PrototipoEcommerce.Domain.Carrinhos.Services;
using PrototipoEcommerce.Domain.Produtos.Entities;
using PrototipoEcommerce.Domain.Produtos.Services;
using PrototipoEcommerce.Web.Models;

namespace PrototipoEcommerce.Web.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutosController(IProdutoService produtoService, IMapper mapper)
        {
            _produtoService = produtoService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _produtoService.ListarAsync(1, int.MaxValue);
            var viewModels = _mapper.Map<IEnumerable<ProdutoViewModel>>(produtos);
            return View(viewModels);
        }

        public async Task<IActionResult> Details(long id)
        {
            var viewModel = await SelecionarProdutoViewModelAsync(id);
            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new ProdutoViewModel
            {
                Promocoes = await ListarPromocoesOpcoesAsync()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProdutoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Promocoes = await ListarPromocoesOpcoesAsync();
                return View(viewModel);
            }

            await _produtoService.CriarAsync(viewModel.Nome, viewModel.Valor, viewModel.PromocaoId);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(long id)
        {
            var viewModel = await SelecionarProdutoViewModelAsync(id);
            viewModel.Promocoes = await ListarPromocoesOpcoesAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(long id, [FromForm] ProdutoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Promocoes = await ListarPromocoesOpcoesAsync();
                return View(viewModel);
            }

            var model = _mapper.Map<Produto>(viewModel);
            model.Id = id;
            model.Promocao = null;
            if (viewModel.PromocaoId.HasValue)
                model.Promocao = new Promocao { Id = viewModel.PromocaoId.Value };

            await _produtoService.AtualizarAsync(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(long id)
        {
            var viewModel = await SelecionarProdutoViewModelAsync(id);
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(long id, IFormCollection collection)
        {
            await _produtoService.RemoverAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AdicionarAoCarrinho(long id, [FromServices] ICarrinhoService carrinhoService)
        {
            await carrinhoService.AdicionarItemAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<ProdutoViewModel> SelecionarProdutoViewModelAsync(long id)
        {
            var produto = await _produtoService.SelecionarAsync(id);
            return _mapper.Map<ProdutoViewModel>(produto);
        }

        private async Task<IEnumerable<SelectListItem>> ListarPromocoesOpcoesAsync()
        {
            var promocoes = await _produtoService.ListarPromocaoAsync();
            var opcoes = new List<SelectListItem>
            {
                new SelectListItem { Value = string.Empty, Text = string.Empty},
            };
            opcoes.AddRange(promocoes.Select(prom => new SelectListItem { Value = prom.Id.ToString(), Text = prom.Nome }));
            return opcoes;
        }
    }
}
