using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace swc.DB.PageStorage.Interfaces
{
    public interface IPageProvider
    {
        Task<(bool IsSuccess, Model.Page page, string ErrorMsg)> GetPageByIdAsync(Guid id);
        Task<(bool IsSuccess, IEnumerable<Model.Page> pages, string ErrorMsg)> GetPagesAsync(/* Introduce Filters */);
        Task<(bool IsSuccess, IEnumerable<Model.Page> pages, string ErrorMsg)> GetPagesInDomainAsync(string domain);
    }
}
