using Constructora.Presentacion.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Constructora.Presentacion.Web.Repository
{
    public interface IFileRepository
    {
        Task<File> GetFile(int Id);
        Task<IEnumerable<File>> GetAllFile();
        Task<bool> Add(File file);
        Task<bool> Update(File file);
        Task<bool> Delete(int Id);
    }
}
