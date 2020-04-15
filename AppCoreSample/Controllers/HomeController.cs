using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AppCoreSample.Models;
using AppCoreSample.IServices;
using Consul;
using AppCoreSample.Util;
using AppCoreSample.Consul;

namespace AppCoreSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserInfoServices _userInfoServices;

        public HomeController(ILogger<HomeController> logger, IUserInfoServices userInfoServices)
        {
            _logger = logger;
            _userInfoServices = userInfoServices;
        }

        public IActionResult Index()
        {
            //ViewBag.userInfo= _userInfoServices.GetUserInfoById(1);

            string uri = "http://AppCoreSample/api/user/get?id=1";

            //string content = HttpClientHelper.GetByHttpClient(url);

            var url = ConsulHelper.GetConsulService(new Uri(uri));

            string content = HttpClientHelper.GetByHttpClient(url);

            //#region consul服务发现
            //ConsulClient consulClient = new ConsulClient(x => { 
            //    x.Address
            //});
            //#endregion

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
