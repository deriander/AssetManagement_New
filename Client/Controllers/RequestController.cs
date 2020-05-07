using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using AssetManagement.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Client.Controllers
{
    public class RequestController : Controller
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

        public JsonResult LoadRequestById()
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var id = HttpContext.Session.GetString("Id"); // session user id
            List<Request> data = new List<Request>();
            var responseTask = client.GetAsync("Request/GetByUserId/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<List<Request>>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, please try again");
            }

            return Json(data);

        }

        public JsonResult AddRequest(Request model)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var user_id = HttpContext.Session.GetString("Id");
            model.User_Id = Convert.ToInt32(user_id); // add user_id from session
            var myContent = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("Request/PostRequest", byteContent).Result;
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

        public JsonResult LoadRequest()
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            List<RequestVM> data = new List<RequestVM>();
            var responseTask = client.GetAsync("Request/GetAdmin");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<List<RequestVM>>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, please try again");
            }

            return Json(data);

        }

        public JsonResult GetById(int id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            List<RequestVM> data = new List<RequestVM>();
            var responseTask = client.GetAsync("Request/GetById/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                //data = JsonConvert.SerializeObject(json);
                data = JsonConvert.DeserializeObject<List<RequestVM>>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, please try again");
            }

            return Json(data);

        }

        public JsonResult AddItemRequest(RequestVM model)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var myContent = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PostAsync("Request/AddRequestItem", byteContent).Result;
            if (result.IsSuccessStatusCode)
            {
                SendEmail(model);
            }
            return Json(result);

        }

        public void SendEmail(RequestVM model)
        {
            // Credentials
            var credentials = new NetworkCredential("yrsproject15@gmail.com", "yarsiproject2015");

            // Mail message
            var mail = new MailMessage()
            {
                From = new MailAddress("yrsproject15@gmail.com"), // email from
                Subject = "Asset Management MII " + DateTimeOffset.Now,
                Body = string.Format("Dear {0},<br /><br />your item request has been added.<br /><br />Thank You.", model.Fullname)
            };
            mail.IsBodyHtml = true;
            mail.To.Add(new MailAddress(model.Email));

            // Smtp client
            var client = new SmtpClient()
            {
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                Credentials = credentials
            };
            try
            {
                client.Send(mail);
            }
            catch
            {

            }
        }


        // VIEW APP1
        public IActionResult App1()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "App1")
            {
                return View();
            }
            return RedirectToAction("AccessDenied", "User");

        }

        public JsonResult LoadRequestApp1()
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            List<RequestVM> data = new List<RequestVM>();
            var responseTask = client.GetAsync("Request/GetApproval1");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();               
                data = JsonConvert.DeserializeObject<List<RequestVM>>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, please try again");
            }

            return Json(data);

        }

        public JsonResult AcceptApproval1(Request model, int id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var myContent = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Request/PutApproval1/" + id, byteContent).Result;
            return Json(result);

        }

        // VIEW APP2
        public IActionResult App2()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "App2")
            {
                return View();
            }
            return RedirectToAction("AccessDenied", "User");

        }

        public JsonResult LoadRequestApp2()
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            List<RequestVM> data = new List<RequestVM>();
            var responseTask = client.GetAsync("Request/GetApproval2");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();;
                data = JsonConvert.DeserializeObject<List<RequestVM>>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, please try again");
            }

            return Json(data);

        }

        public JsonResult AcceptApproval2(Request model, int id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var myContent = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Request/PutApproval2/" + id, byteContent).Result;
            return Json(result);

        }

        // DECLINE APPROVAL
        public JsonResult DeclineApproval(Request model, int id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var myContent = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Request/DeclineApproval/" + id, byteContent).Result;
            return Json(result);

        }
    }
}