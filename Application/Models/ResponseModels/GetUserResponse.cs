namespace Demos.API.Application.Models.ResponseModels
{
    public class GetUserResponse
    {
        public string Id { get; set; }
        public string DNIType { get; set; }
        public string DNI { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
