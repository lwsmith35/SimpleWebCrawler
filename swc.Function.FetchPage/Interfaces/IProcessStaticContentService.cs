using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace swc.Function.FetchPage.Interfaces
{
    public interface IProcessStaticContentService
    {
        Task<HttpResponseMessage> SendPageToStaticContentProcess(Guid PageId);
    }
}
