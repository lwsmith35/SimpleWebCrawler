using System;
using System.Threading.Tasks;

namespace swc.DB.PageStorage.Interfaces
{
    public interface IPageCommand
    {
        Task<(bool IsSuccess, Guid? Id, bool PageExists, string ErrorMessage)> SavePageAsync(Model.NewPage page);
        Task<(bool IsSuccess,  string ErrorMessage)> DeletePageByIdAsync(Guid PageId);
    }
}
