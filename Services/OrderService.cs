using BasicLayeredService.API.Constants;
using BasicLayeredService.API.Contracts;
using BasicLayeredService.API.DTOs;

namespace BasicLayeredService.API.Services;

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
