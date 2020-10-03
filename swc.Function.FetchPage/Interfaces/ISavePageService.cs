using swc.Function.FetchPage.Model;
using System.Threading.Tasks;

namespace swc.Function.FetchPage.Interfaces
{
    public interface ISavePageService
    {
        Task<(bool IsSuccess, CreatedPage page, string ErrorMessage)> SavePage(string resourceUrl, string pageContent);
    }
}
