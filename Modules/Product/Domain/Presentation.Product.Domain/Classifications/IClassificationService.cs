namespace Presentation.Product.Domain.Classifications
{
    public interface IClassificationService
    {
        Task<HttpResponseMessage> GetClassfication();
        Task<HttpResponseMessage> UpdateClassfication(IEnumerable<ClassificationModel> param);
        Task<HttpResponseMessage> DeleteClassfication(ClassificationModel param);
        Task<HttpResponseMessage> ReactiveClassfication(ClassificationModel param);
    }
}
