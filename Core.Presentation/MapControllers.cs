using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Core.Presentation;

public static class MapControllers
{
    public static void MapEndpoints(this IEndpointRouteBuilder endpoint)
    {
        var group = endpoint.MapGroup("api");
    }
}