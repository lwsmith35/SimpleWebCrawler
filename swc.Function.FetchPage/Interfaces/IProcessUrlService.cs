using swc.Function.FetchPage.Model;
using System.Threading.Tasks;

namespace swc.Function.FetchPage.Interfaces
{
    public interface IProcessUrlService
    {
        Task<(bool IsSuccess, string CorelationId, string ErrorMessage)> ProcessUrl(ProcessUrl Url);
    }
}
