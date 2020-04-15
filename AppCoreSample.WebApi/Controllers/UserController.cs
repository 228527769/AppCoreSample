using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCoreSample.IServices;
using AppCoreSample.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace AppCoreSample.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInfoServices _userInfoServices;
        private readonly IConfiguration _configuration;

        public UserController(IUserInfoServices userInfoServices, IConfiguration configuration)
        {
            _userInfoServices = userInfoServices;
            _configuration = configuration;
        }

        [HttpGet]
        [Route("Get")]
        public UserInfo Get(int id)
        {
            Console.WriteLine($"此服务的ip:{_configuration["ip"]},port:{_configuration["port"]}");

            return this._userInfoServices.GetUserInfoById(id);
        }

        [HttpGet]
        [Route("GetAll")]
        public List<UserInfo> GetAll()
        {
            return this._userInfoServices.GetAll();
        }
    }
}