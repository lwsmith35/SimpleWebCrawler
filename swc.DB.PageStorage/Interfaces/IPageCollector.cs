using System;
using System.Threading.Tasks;

namespace swc.DB.PageStorage.Interfaces
{
    public interface IPageCollector
    {
        Task<(bool IsSuccess, Guid? Id, string ErrorMessage)> SavePageAsync(Model.NewPage page);
    }
}
