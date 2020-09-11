using Demos.API.Application.Models.ResponseModels;
using MediatR;
using System;

namespace Demos.API.Application.Commands
{
    public class CreateExpenseEventCommand : IRequest<CreateExpenseEventResponse>
    {
        public Guid ExpenseId { get; set; }
        public string Type { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
    }
}
