using Constructora.Presentacion.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Constructora.Presentacion.Web.Repository
{
    public interface IUserRepository
    {
        User GetUser(int Id);
        IEnumerable<User> GetAllUser();
        User Add(User user);
        User Update(User user);
        User Delete(int Id);
        Task<User> Validate(User user);
    }
}
