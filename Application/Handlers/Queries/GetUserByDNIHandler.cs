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
    public class GetUserByDNIHandler : IRequestHandler<GetUserByDNIQuery, GetUserResponse>
    {
        private readonly IMongoCollection<User> _users;
        public GetUserByDNIHandler(IDatabaseSettings settings)
        {
            var client = new MongoClient(/*settings?.ConnectionString ?? */"mongodb://cosmosdbcmejia:Nkg8mnCI8IZKMR4JFS1hS5bI9v2fjqADsy9NO1ZizFcz4OTVS2IvArYmp5AdSVVV3i9Z3UI0m3fqUY6LhUAS8w==@cosmosdbcmejia.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@cosmosdbcmejia@");
            var database = client.GetDatabase(/*settings?.DatabaseName ?? */"TestDB");
            _users = database.GetCollection<User>(/*settings?.UsersCollectionName ??*/ "Users");
        }
        public async Task<GetUserResponse> Handle(GetUserByDNIQuery request, CancellationToken cancellationToken)
        {
            GetUserResponse result = null;
            var user = await _users.Find<User>(c => c.DNI == request.DNI).FirstOrDefaultAsync();
            if (user != null)
            {
                result = new GetUserResponse()
                {
                    Id = user.Id,
                    Age = user.Age,
                    DNI = user.DNI,
                    DNIType = user.DNIType,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
            }
            return result;
        }
    }
}
