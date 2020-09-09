using MediatR;
using Demos.API.Application.Models.ResponseModels;

namespace Demos.API.Application.Commands
{
    public class CreateFileCommand : IRequest<CreateFileResponse>
    {
        public string RequestId { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        public string BlobType { get; set; }
        public string Url { get; set; }
    }
}
