using System.Net.Http;
using System.Threading.Tasks;

namespace Demos.API.Application.Contracts
{
    public interface INasa
    {
        public Task<string> getDONKIInfo();
        public Task<string> getInSightInfo();
        public Task<string> getInSightInfo(IHttpClientFactory factory);
    }
}
