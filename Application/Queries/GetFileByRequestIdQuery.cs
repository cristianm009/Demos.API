using MediatR;
using Demos.API.Application.Models.ResponseModels;

namespace Demos.API.Application.Queries
{
    public class GetFileByRequestIdQuery : IRequest<GetFileResponse>
    {
        public string RequestId { get; set; }
        public GetFileByRequestIdQuery(string requestId)
        {
            RequestId = requestId;
        }
    }
}
