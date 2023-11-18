using Presentation.Product.Domain.Colors;
using Presentation.Product.Domain.Sizes;

namespace Presentation.Product.Domain.Sizes
{
    public interface ISizeService
    {
        Task<HttpResponseMessage> GetSize();
        Task<HttpResponseMessage> UpdateSize(IEnumerable<SizeModel> param);
        Task<HttpResponseMessage> DeleteSize(SizeModel param);
        Task<HttpResponseMessage> ReactiveSize(SizeModel param);


    }
}
