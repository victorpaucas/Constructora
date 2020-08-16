using Constructora.Presentacion.Web.Data;
using Constructora.Presentacion.Web.Models;
using System.Collections.Generic;
using System.Linq;

namespace Constructora.Presentacion.Web.Repository
{
    public class UserTypeRepository : IUserTypeRepository
    {
        protected MainContext MainContext { get; set; }

        public UserTypeRepository(MainContext mainContext)
        {
            MainContext = mainContext;
        }

        public UserType Add(UserType userType)
        {
            var UserType = MainContext.UserType.Add(userType).Entity;
            MainContext.SaveChanges();
            return UserType;
        }

        public UserType Delete(int Id)
        {
            var UserType = MainContext.UserType.Find(Id);
            UserType.Remove = true;
            MainContext.UserType.Update(UserType);
            MainContext.SaveChanges();
            return UserType;
        }

        public IEnumerable<UserType> GetAllUserType()
        {
            return MainContext.UserType.Where(s => s.Remove == false);
        }

        public UserType GetUserType(int Id)
        {
            return MainContext.UserType.Find(Id);
        }

        public UserType Update(UserType userType)
        {
            var UserType = MainContext.UserType.Update(userType).Entity;
            MainContext.SaveChanges();
            return UserType;
        }
    }
}
