using System.Threading.Tasks;

namespace swc.Function.FetchPage.Interfaces
{
    public interface IProcessUrlService
    {
        Task<(bool isSuccess, string corelationId)> ProcessUrl(string Url);
    }
}
