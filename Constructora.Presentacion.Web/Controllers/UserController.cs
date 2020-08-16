using Constructora.Presentacion.Web.Models;
using Constructora.Presentacion.Web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Constructora.Presentacion.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository UserRepository;

        public UserController(IUserRepository userRepository)
        {
            UserRepository = userRepository;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetUser( int Id)
        {
            return Json(UserRepository.GetUser(Id));
        }

        [Authorize]
        [HttpGet]
        public JsonResult GetAllUser()
        {
            return Json(UserRepository.GetAllUser());
        }

        [Authorize]
        [HttpPost]
        public JsonResult Save([FromBody] User user)
        {
            User result;
            
            if (user.Id > 0)
            {
                result = UserRepository.Update(user);
            }
            else
            {
                result = UserRepository.Add(user);
            }

            return Json(result);
        }

        [Authorize]
        [HttpDelete]
        public JsonResult Delete(int Id)
        {
            return Json(UserRepository.Delete(Id));
        }
    }
}
