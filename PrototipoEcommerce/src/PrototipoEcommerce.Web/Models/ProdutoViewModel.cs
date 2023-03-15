using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace PrototipoEcommerce.Web.Models;

public class ProdutoViewModel
{
    public long Id { get; set; }
    
    [Required]
    public string Nome { get; set; } = default!;
    
    [Required]
    public decimal Valor { get; set; }

    [Display(Name = "Promoção")]
    public long? PromocaoId { get; set; }

    [Display(Name = "Promoção")]
    public string? PromocaoNome => Promocao?.Nome;

    public PromocaoViewModel? Promocao { get; set; }

    public IEnumerable<SelectListItem>? Promocoes { get; set; }
}
