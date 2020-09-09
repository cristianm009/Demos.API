using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using Demos.API.Application.Models;
using Demos.API.Application.Models.EntityModels;
using Demos.API.Application.Models.ResponseModels;
using Demos.API.Application.Queries;

namespace Demos.API.Application.Handlers.Queries
{
    public class GetFileByRequestIdHandler : IRequestHandler<GetFileByRequestIdQuery, GetFileResponse>
    {
        private readonly IMongoCollection<File> _files;
        public GetFileByRequestIdHandler(IDatabaseSettings settings)
        {
            var client = new MongoClient(/*settings?.ConnectionString ??*/ "mongodb://cosmosdbcmejia:Nkg8mnCI8IZKMR4JFS1hS5bI9v2fjqADsy9NO1ZizFcz4OTVS2IvArYmp5AdSVVV3i9Z3UI0m3fqUY6LhUAS8w==@cosmosdbcmejia.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@cosmosdbcmejia@");
            var database = client.GetDatabase(/*settings?.DatabaseName ??*/ "TestDB");
            _files = database.GetCollection<File>(/*settings?.FilesCollectionName ??*/ "Files");
        }
        public async Task<GetFileResponse> Handle(GetFileByRequestIdQuery request, CancellationToken cancellationToken)
        {
            GetFileResponse result = null;
            var file = await _files.Find<File>(c => c.RequestId == request.RequestId).FirstOrDefaultAsync();
            if (file != null)
            {
                result = new GetFileResponse()
                {
                    BlobType = file.BlobType,
                    ContentLength = file.ContentLength,
                    ContentType = file.ContentType,
                    RequestId = file.RequestId,
                    Url = file.Url
                };
            }
            return result;
        }
    }
}
