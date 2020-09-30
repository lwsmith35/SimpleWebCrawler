using swc.Function.FetchPage.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace swc.Function.FetchPage.Interfaces
{
    public interface IFetchPageService
    {
        Task<(bool isSuccess, PageHeaders, string ErrorMsg)> FetchTargetHeadersAsync(string url);
        Task<(bool isSuccess, PageContent, string ErrorMsg)> FetchTargetContentAsync(string url);

    }
}
