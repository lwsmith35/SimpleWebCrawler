using swc.Function.FetchPage.Model;
using System.Threading.Tasks;

namespace swc.Function.FetchPage.Interfaces
{
    public interface IFetchPageService
    {
        Task<(bool IsSuccess, PageHeaders PageHeaders, string ErrorMsg)> FetchTargetHeadersAsync(string url);
        Task<(bool IsSuccess, PageContent PageContent, string ErrorMsg)> FetchTargetContentAsync(string url);
    }
}
