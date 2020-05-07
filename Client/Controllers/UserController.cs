using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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
    public class UserController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:44304/api/")
        };

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var myContent = JsonConvert.SerializeObject(user);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("User/SignIn", byteContent).Result;

            if (result.IsSuccessStatusCode)
            {
                var data = result.Content.ReadAsStringAsync().Result; // token
                var handler = new JwtSecurityTokenHandler();
                var datajson = handler.ReadJwtToken(data);

                // get token, role, emai from jwt
                string token = "Bearer " + data;
                string id = datajson.Claims.First(claim => claim.Type == "Id").Value;
                string role = datajson.Claims.First(claim => claim.Type == "Role").Value;
                string email = datajson.Claims.First(claim => claim.Type == "Email").Value;

                // set token
                HttpContext.Session.SetString("JWTToken", token);
                HttpContext.Session.SetString("Id", id);
                HttpContext.Session.SetString("Role", role);
                HttpContext.Session.SetString("Email", email);

                if (role == "User")
                {
                    return RedirectToAction("Index", "Item");
                }
                else if (role == "Admin")
                {
                    return RedirectToAction("Admin", "Item");
                }
                else if (role == "App1")
                {
                    return RedirectToAction("App1", "Borrow");
                }
                else if (role == "App2")
                {
                    return RedirectToAction("App2", "Borrow");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("JWTToken");
            HttpContext.Session.Remove("Id");
            HttpContext.Session.Remove("Role");
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Login", "User");
        }


    }
}