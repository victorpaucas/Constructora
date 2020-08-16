using Constructora.Presentacion.Web.Data;
using Constructora.Presentacion.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructora.Presentacion.Web.Repository
{
    public class FileRepository : IFileRepository
    {
        protected MainContext MainContext { get; set; }

        public FileRepository(MainContext mainContext)
        {
            MainContext = mainContext;
        }

        public async Task<bool> Add(File file)
        {
            MainContext.File.Add(file);
            await MainContext.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> Delete(int Id)
        {
            MainContext.File.Attach(await MainContext.File.FindAsync(Id)).Property(f => f.Remove).IsModified = true;
            await MainContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<File>> GetAllFile()
        {
            return await MainContext.File.Where(s => s.Remove == false).ToListAsync();
        }

        public async Task<File> GetFile(int Id)
        {
            return await MainContext.File.FindAsync(Id);
        }

        public async Task<bool> Update(File file)
        {
            MainContext.File.Attach(file).Property(f => f.Name).IsModified = true;
            await MainContext.SaveChangesAsync();
            
            return true;
        }
    }
}
