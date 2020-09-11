using Demos.API.Application.Commands;
using Demos.API.Application.Models;
using Demos.API.Application.Models.EntityModels;
using Demos.API.Application.Models.ResponseModels;
using MediatR;
using MongoDB.Driver;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Demos.API.Application.Handlers.Commnads
{
    public class CreateExpenseEventHandler : IRequestHandler<CreateExpenseEventCommand, CreateExpenseEventResponse>
    {
        private readonly IMongoCollection<ExpenseEvent> _expenseEvents;
        public CreateExpenseEventHandler(IDatabaseSettings settings)
        {
            var client = new MongoClient(/*settings?.ConnectionString ?? */"mongodb://cosmosdbcmejia:Nkg8mnCI8IZKMR4JFS1hS5bI9v2fjqADsy9NO1ZizFcz4OTVS2IvArYmp5AdSVVV3i9Z3UI0m3fqUY6LhUAS8w==@cosmosdbcmejia.mongo.cosmos.azure.com:10255/?ssl=true&replicaSet=globaldb&retrywrites=false&maxIdleTimeMS=120000&appName=@cosmosdbcmejia@");
            var database = client.GetDatabase(/*settings?.DatabaseName ?? */"Expenses");
            _expenseEvents = database.GetCollection<ExpenseEvent>(/*settings?.FilesCollectionName ??*/ "ExpensesEvents");
        }
        public async Task<CreateExpenseEventResponse> Handle(CreateExpenseEventCommand request, CancellationToken cancellationToken)
        {
            CreateExpenseEventResponse result = null;
            if (request != null)
            {
                ExpenseEvent expenseEventToInsert = new ExpenseEvent()
                {
                    Author = request.User,
                    TimeStamp = DateTime.Now,
                    Data = JsonSerializer.Serialize(request),
                    Type = "Created",
                    ExpenseId = request.ExpenseId.ToString(),
                };
                await _expenseEvents.InsertOneAsync(expenseEventToInsert);
                result = new CreateExpenseEventResponse()
                {
                    User = expenseEventToInsert.Author,
                    TimeStamp = expenseEventToInsert.TimeStamp
                };
            }
            return result;
        }
    }
}