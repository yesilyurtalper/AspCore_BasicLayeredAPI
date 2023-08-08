using BasicLayeredAPI.API.Constants;
using BasicLayeredAPI.API.Contracts;
using BasicLayeredAPI.API.DTOs;

namespace BasicLayeredAPI.API.Services;

internal class OrderService : IOrderService
{

    private readonly IHttpClientFactory _httpFactory;

    public OrderService(IHttpClientFactory httpFactory)
    {

        _httpFactory = httpFactory;

    }

    public async Task<ResponseDto<string>> MakeOrderAsync()
    {
        var httpClient = _httpFactory.CreateClient(APIConstants.OrderAPIClient);
        var httpResponse = await httpClient.SendAsync(new HttpRequestMessage());
        throw new NotImplementedException();
    }
}
