using Newtonsoft.Json;
using Presentation.Core.Domain;

namespace Presentation.Core.Service;

public static class ResponseExtension {
    public static ResponseBaseModel<T> ConvertResponse<T>(this HttpResponseMessage httpResponse) => JsonConvert.DeserializeObject<ResponseBaseModel<T>>(httpResponse.Content.ReadAsStringAsync().Result)!;
}