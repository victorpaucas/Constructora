using Constructora.Presentacion.Web.Models;
using System.Collections.Generic;

namespace Constructora.Presentacion.Web.Repository
{
    public interface IUserTypeRepository
    {
        UserType GetUserType(int Id);
        IEnumerable<UserType> GetAllUserType();
        UserType Add(UserType userType);
        UserType Update(UserType userType);
        UserType Delete(int Id);
    }
}
