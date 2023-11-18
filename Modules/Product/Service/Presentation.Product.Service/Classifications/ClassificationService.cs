using Blazored.LocalStorage;
using Presentation.Core.Service;
using Presentation.Product.Domain.Classifications;

namespace Presentation.Product.Service.Classifications
{
    public class ClassificationService : ApiClient, IClassificationService
    {
        public ClassificationService(HttpClient httpClient, ILocalStorageService localStorageService) : base(httpClient,
            localStorageService)
        {
        }

        public async Task<HttpResponseMessage> DeleteClassfication(ClassificationModel param)
        {
            var response = await this.PutAsync(Endpoint.DELETE_CLASSFICATION, param);
            return response;
        }

        public async Task<HttpResponseMessage> GetClassfication()
        {
            var result = await this.GetAsync(Endpoint.GET_CLASSFICATION);
            return result;
        }

        public async Task<HttpResponseMessage> ReactiveClassfication(ClassificationModel param)
        {
            var response = await this.PutAsync(Endpoint.REACTIVE_CLASSFICATION, param);

            return response;
        }

        public async Task<HttpResponseMessage> UpdateClassfication(IEnumerable<ClassificationModel> param)
        {
            var request = param.Where(x => x.IsDeleted != 1);
            var result = await this.PostAsync(Endpoint.UPDATE_CLASSFICATION,
                new { Classifications = request });
            return result;
        }
    }
}