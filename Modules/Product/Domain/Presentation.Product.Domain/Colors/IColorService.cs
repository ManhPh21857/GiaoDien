namespace Presentation.Product.Domain.Colors
{
    public interface IColorService
    {
        Task<HttpResponseMessage> GetColors();
        Task<HttpResponseMessage> UpdateColor(IEnumerable<ColorModel> param);
        Task<HttpResponseMessage> DeleteColor(ColorModel param);
        Task<HttpResponseMessage> ReactiveColor(ColorModel param);
    }
}
