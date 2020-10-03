using swc.Function.FindLinks.Model;
using System.Threading.Tasks;

namespace swc.Function.FindLinks.Interfaces
{
    public interface IFindLinksService
    {
        Task<(bool IsSuccessful, int LinksFollowedCount, string ErrorMessage)> ParseLinksFromPageAsync(PageRequestId requestPage);
    }
}
