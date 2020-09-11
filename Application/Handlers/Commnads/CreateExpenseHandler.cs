using Demos.API.Application.Commands;
using Demos.API.Application.Models;
using Demos.API.Application.Models.EntityModels;
using Demos.API.Application.Models.ResponseModels;
using MediatR;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;

namespace Demos.API.Application.Handlers.Commnads
{
    public class CreateExpenseHandler : IRequestHandler<CreateExpenseCommand, CreateExpenseResponse>
    {
        private readonly IMongoCollection<Expense> _expenses;
        public CreateExpenseHandler(IDatabaseSettings settings)
        {
            var client = new MongoClient(/*settings?.ConnectionString ?? */"mongodb://cosmosdbcmejia:Nkg8mnCI8IZKMR4JFS1hS5bI9v2fjqADsy9NO1ZizFcz4OTVS2IvArYmp5AdSVVV3i9Z3UI0m3fqUY6LhUAS8w==@cosmosdbcmejia.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@cosmosdbcmejia@");
            var database = client.GetDatabase(/*settings?.DatabaseName ?? */"Expenses");
            _expenses = database.GetCollection<Expense>(/*settings?.FilesCollectionName ??*/ "Expenses");
        }
        public async Task<CreateExpenseResponse> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
        {
            CreateExpenseResponse result = null;
            if (request != null)
            {
                Expense expenseToInsert = new Expense()
                {
                    CreationDate = request.CreationDate,
                    Description = request.Description,
                    ExpenseId = request.ExpenseId,
                    Name = request.Name,
                    Type = request.Type,
                    User = request.User
                };
                await _expenses.InsertOneAsync(expenseToInsert);
                result = new CreateExpenseResponse()
                {
                    ExpenseId = expenseToInsert.ExpenseId.ToString()
                };
            }
            return result;
        }
    }
}