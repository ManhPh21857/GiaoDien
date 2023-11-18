using Blazored.LocalStorage;
using Presentation.Core.Service;
using Presentation.Product.Domain.Materials;

namespace Presentation.Product.Service.Materials
{
    public class MaterialService: ApiClient, IMaterialService

    {
        public MaterialService(HttpClient httpClient, ILocalStorageService localStorageService) : base(httpClient, localStorageService)
        {
        }

        public async Task<HttpResponseMessage> GetMaterial()
        {
            var result = await this.GetAsync(Endpoint.GET_MATERIAL);
            return result;
        }

        public async Task<HttpResponseMessage> UpdateMaterial(IEnumerable<MaterialModel> material)
        {
            var request = material.Where(x => x.IsDeleted != 1);
            var result = await this.PostAsync(Endpoint.UPDATE_MATERIAL, new { Materials = request });
            return result;
        }

        public async Task<HttpResponseMessage> DeleteMaterial(MaterialModel material)
        {
            var response = await this.PutAsync(Endpoint.DELETE_MATERIAL, material);
            return response;
        }

        public async Task<HttpResponseMessage> ReactiveMaterial(MaterialModel material)
        {
            var response = await this.PutAsync(Endpoint.REACTIVE_MATERIAL, material);

            return response;
        }
    }
}
