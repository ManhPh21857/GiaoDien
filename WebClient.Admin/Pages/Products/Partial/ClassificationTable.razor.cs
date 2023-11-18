using Microsoft.AspNetCore.Components;
using Presentation.Product.Domain.Colors;
using Presentation.Product.Domain.Products;
using Presentation.Product.Domain.Sizes;

#pragma warning disable CS8618

namespace WebClient.Admin.Pages.Products.Partial
{
    public partial class ClassificationTable : ComponentBase
    {
        [Parameter] public IEnumerable<ProductColor> ProductColors { get; set; }
        [Parameter] public IEnumerable<ProductDetail> ProductItems { get; set; }
        [Parameter] public IEnumerable<ColorModel> Colors { get; set; }
        [Parameter] public IEnumerable<SizeModel> Sizes { get; set; }
    }
}