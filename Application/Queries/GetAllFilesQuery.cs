using MediatR;
using System.Collections.Generic;
using Demos.API.Application.Models.ResponseModels;

namespace Demos.API.Application.Queries
{
    public class GetAllFilesQuery : IRequest<List<GetFileResponse>>
    {
    }
}
