using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using Demos.API.Application.Commands;
using Demos.API.Application.Models;
using Demos.API.Application.Models.EntityModels;
using Demos.API.Application.Models.ResponseModels;

namespace Demos.API.Application.Handlers.Commnads
{

    public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResponse>
    {
        private readonly IMongoCollection<User> _users;
        public CreateUserHandler(IDatabaseSettings settings)
        {
            var client = new MongoClient(/*settings?.ConnectionString ?? */ "mongodb://cosmosdbcmejia:Nkg8mnCI8IZKMR4JFS1hS5bI9v2fjqADsy9NO1ZizFcz4OTVS2IvArYmp5AdSVVV3i9Z3UI0m3fqUY6LhUAS8w==@cosmosdbcmejia.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@cosmosdbcmejia@");
            var database = client.GetDatabase(/*settings?.DatabaseName ?? */ "TestDB");
            _users = database.GetCollection<User>(/*settings?.UsersCollectionName ?? */ "Users");
        }
        public async Task<CreateUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            CreateUserResponse result = null;
            if (request != null)
            {
                User userToInsert = new User()
                {
                    Age = request.Age,
                    DNI = request.DNI,
                    DNIType = request.DNIType,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                };
                await _users.InsertOneAsync(userToInsert);
                result = new CreateUserResponse()
                { DNI = userToInsert.DNI };
            }
            return result;
        }
    }
}
