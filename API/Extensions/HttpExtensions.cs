using Microsoft.AspNetCore.Http;
using API.Helpers;
using System.Text.Json;

namespace API.Extensions {
    public static class HttpExtensions {
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int pageSize, int totalPages, int totalItems) {
            var paginationHeader = new PaginationHeader(currentPage, pageSize, totalPages, totalItems);

            var options = new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader, options));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}