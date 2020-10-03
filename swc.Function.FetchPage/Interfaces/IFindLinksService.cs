using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace swc.Function.FetchPage.Interfaces
{
    public interface IFindLinksService
    {
        Task<HttpResponseMessage> SendPageToLinkProcess(Guid PageId);
    }
}
