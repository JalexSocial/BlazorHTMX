using Htmx;

namespace BlazorHtmx.Services
{
    public class HtmxService
    {
        private readonly IHttpContextAccessor? _httpContextAccessor = null;
        private readonly HttpContext? _httpContext = null;

        public HttpContext HttpContext => _httpContext ?? _httpContextAccessor?.HttpContext!;

        public HtmxService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

    }
}
