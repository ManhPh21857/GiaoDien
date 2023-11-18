namespace Presentation.Product.Domain.Materials
{
    public interface IMaterialService
    {
        Task<HttpResponseMessage> GetMaterial();
        Task<HttpResponseMessage> UpdateMaterial(IEnumerable<MaterialModel> material);
        Task<HttpResponseMessage> DeleteMaterial(MaterialModel material);
        Task<HttpResponseMessage> ReactiveMaterial(MaterialModel material);
    }
}
