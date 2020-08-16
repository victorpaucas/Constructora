using Constructora.Presentacion.Web.Data;
using Constructora.Presentacion.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Constructora.Presentacion.Web.Repository
{
    public class UserRepository : IUserRepository
    {
        protected MainContext MainContext { get; set; }

        public UserRepository(MainContext mainContext)
        {
            MainContext = mainContext;
        }

        public User Add(User user)
        {
            var User = MainContext.User.Add(user).Entity;
            MainContext.SaveChanges();
            return User;
        }

        public User Delete(int Id)
        {
            var User = MainContext.User.Find(Id);
            User.Remove = true;
            MainContext.User.Update(User);
            MainContext.SaveChanges();
            return User;
        }

        public IEnumerable<User> GetAllUser()
        {
            return MainContext.User.Where(s => s.Remove == false).Include(i => i.UserType).ToList();
        }

        public User GetUser(int Id)
        {
            return MainContext.User.Find(Id);
        }

        public User Update(User user)
        {
            var User = MainContext.User.Update(user).Entity;
            MainContext.SaveChanges();
            return User;
        }

        public async Task<User> Validate(User user)
        {
            return await MainContext.User.Where(s => s.Name.ToLower().Equals(user.Name) && s.Password.ToLower().Equals(user.Password) && s.Remove == false).FirstOrDefaultAsync();
        }
    }
}
