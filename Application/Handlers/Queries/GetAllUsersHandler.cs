using MediatR;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Demos.API.Application.Models;
using Demos.API.Application.Models.EntityModels;
using Demos.API.Application.Models.ResponseModels;
using Demos.API.Application.Queries;

namespace Demos.API.Application.Handlers.Queries
{
    public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<GetUserResponse>>
    {
        private readonly IMongoCollection<User> _users;
        public GetAllUsersHandler(IDatabaseSettings settings)
        {
            var client = new MongoClient(/*settings?.ConnectionString ?? */"mongodb://cosmosdbcmejia:Nkg8mnCI8IZKMR4JFS1hS5bI9v2fjqADsy9NO1ZizFcz4OTVS2IvArYmp5AdSVVV3i9Z3UI0m3fqUY6LhUAS8w==@cosmosdbcmejia.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@cosmosdbcmejia@");
            var database = client.GetDatabase(/*settings?.DatabaseName ?? */"TestDB");
            _users = database.GetCollection<User>(/*settings?.UsersCollectionName ??*/ "Users");
        }
        public async Task<List<GetUserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<GetUserResponse> result = new List<GetUserResponse>();
            var users = await _users.Find(s => true).ToListAsync();
            if (users.Any())
            {
                foreach (User user in users)
                {
                    result.Add(new GetUserResponse()
                    {
                        Id = user.Id,
                        Age = user.Age,
                        DNI = user.DNI,
                        DNIType = user.DNIType,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    }
                    );
                }
            }
            return result;
        }
    }
}
