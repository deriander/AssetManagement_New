using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AssetManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class ReturnController : Controller
    {
        private HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44304/api/")
        };

        // VIEW USER
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "User" || role == "App1" || role == "App2")
            {
                return View();
            }
            return RedirectToAction("AccessDenied", "User");
            
        }

        public JsonResult LoadReturnUser()
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var id = HttpContext.Session.GetString("Id"); // input from session
            List<ReturnVM> data = new List<ReturnVM>();
            var responseTask = client.GetAsync("Return/GetReturnUser/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<List<ReturnVM>>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, please try again");
            }

            return Json(data);

        }

        public JsonResult ReturnItem(Return model)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var myContent = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Return/ReturnDone/" + model.Item_Id, byteContent).Result;
            return Json(result);
        }


        // VIEW ADMIN
        public IActionResult Admin()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "Admin")
            {
                return View();
            }
            return RedirectToAction("AccessDenied", "User");
            
        }

        public JsonResult LoadReturnAdmin()
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            List<ReturnVM> data = new List<ReturnVM>();
            var responseTask = client.GetAsync("Return/GetReturnAdmin");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<List<ReturnVM>>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, please try again");
            }

            return Json(data);

        }
    }
}