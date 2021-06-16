using System.Net;

namespace CQRSMediatRDemo.DTOs
{
    public record CQRSResponse
    {
        public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;
        public string ErrorMessage { get; init; }
    }
}
