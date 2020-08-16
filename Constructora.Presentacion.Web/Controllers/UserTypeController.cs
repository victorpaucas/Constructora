using Constructora.Presentacion.Web.Models;
using Constructora.Presentacion.Web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Constructora.Presentacion.Web.Controllers
{
    public class UserTypeController : Controller
    {
        private readonly IUserTypeRepository UserTypeRepository;

        public UserTypeController(IUserTypeRepository userTypeRepository)
        {
            UserTypeRepository = userTypeRepository;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetUserType(int Id)
        {
            return Json(UserTypeRepository.GetUserType(Id));
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetAllUserType()
        {
            return Json(UserTypeRepository.GetAllUserType());
        }

        [Authorize]
        [HttpPost]
        public JsonResult Save([FromBody] UserType userType)
        {
            var result = new UserType();

            if (userType.Id > 0)
            {
                result = UserTypeRepository.Update(userType);
            }
            else
            {
                result = UserTypeRepository.Add(userType);
            }

            return Json(result);
        }

        [Authorize]
        [HttpDelete]
        public JsonResult Delete(int Id)
        {
            return Json(UserTypeRepository.Delete(Id));
        }
    }
}
