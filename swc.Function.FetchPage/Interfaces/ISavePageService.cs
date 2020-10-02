using System.Threading.Tasks;

namespace swc.Function.FetchPage.Interfaces
{
    public interface ISavePageService
    {
        Task<(bool IsSuccess, string PageId, string ErrorMessage)> SavePage(string resourceUrl, string pageContent);
    }
}
