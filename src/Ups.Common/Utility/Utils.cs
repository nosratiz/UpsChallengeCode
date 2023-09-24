using System.Linq;
using System.Net.Http;

namespace UPS.Common.Utility;

public static class Utils
{
    public static string GenerateQuerySearchFromRequest<T>(T request) where T : class
    {
        var query = string.Empty;
        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            var value = property.GetValue(request);
            if (value != null)
            {
                query += $"{property.Name.ToLower()}={value}&";
            }
        }

        return query;
    }
    
    public static Paging GetPaging(HttpResponseMessage responseMessage){
        var paging = new Paging();
      
        if (responseMessage.Headers.TryGetValues("x-pagination-page", out var page))
            paging.Page = int.Parse(page.First());
        
        if (responseMessage.Headers.TryGetValues("x-pagination-pages", out var totalPage))
            paging.TotalPages = int.Parse(totalPage.First());
        
        if (responseMessage.Headers.TryGetValues("x-pagination-total", out var totalCount))
            paging.TotalCount = int.Parse(totalCount.First());
        

        return paging;
    }
}