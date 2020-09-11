using System;

namespace Demos.API.Application.Models.ResponseModels
{
    public class CreateExpenseEventResponse
    {
        public string User { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
