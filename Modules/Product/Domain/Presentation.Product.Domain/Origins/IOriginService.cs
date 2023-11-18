namespace Presentation.Product.Domain.Origins
{
    public interface IOriginService
    {
        Task<HttpResponseMessage> GetOrigin();
        Task<HttpResponseMessage> UpdateOrigin(IEnumerable<OriginModel> origin);
        Task<HttpResponseMessage> DeleteOrigin(OriginModel origin);
        Task<HttpResponseMessage> ReactiveOrigin(OriginModel origin);



    }
}
