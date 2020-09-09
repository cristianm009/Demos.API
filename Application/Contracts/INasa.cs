using System.Threading.Tasks;

namespace Demos.API.Application.Contracts
{
    public interface INasa
    {
        public Task<string> getDONKIAsync();
    }
}
