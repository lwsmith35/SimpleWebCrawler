using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace swc.Function.FetchPage.Interfaces
{
    public interface IProcessUrlService
    {
        Task<(bool isSuccess, string corelationId)> processUrl(string Url);
    }
}
