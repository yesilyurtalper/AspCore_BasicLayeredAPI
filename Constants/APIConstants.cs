namespace BasicLayeredService.API.Constants;

public class APIConstants
{
    public static Dictionary<int, string> StatusDescriptions = new () {
        { 401,"Kullanıcı doğrulanamadı"},
        { 403,"Yetkiniz yetersiz" },
        { 404,"Kaynak bulunamadı" },
        { 405,"Method bulunamadı" }
    };
    public static string OrderAPIClient = "OrderAPIClient";
    public static string OrderAPIBaseUrl = "OrderAPIClient";
    public static string BasicLayeredServiceClient = "BasicLayeredServiceClient";
    public static string BasicLayeredServiceAdmin = "BasicLayeredServiceAdmin";
}
