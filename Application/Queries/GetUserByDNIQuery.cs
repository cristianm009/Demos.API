using MediatR;
using Demos.API.Application.Models.ResponseModels;

namespace Demos.API.Application.Queries
{
    public class GetUserByDNIQuery : IRequest<GetUserResponse>
    {
        public string DNI { get; set; }
        public GetUserByDNIQuery(string dni)
        {
            DNI = dni;
        }
    }
}
