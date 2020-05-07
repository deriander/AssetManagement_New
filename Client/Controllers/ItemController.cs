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
    public class ItemController : Controller
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

        public JsonResult LoadItemUser()
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            List<Item> data = new List<Item>();
            var responseTask = client.GetAsync("Item/GetItemUser/");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<List<Item>>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, please try again");
            }

            return Json(data);

        }

        public JsonResult BorrowItem(BorrowVM model, int id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var user_id = HttpContext.Session.GetString("Id");
            model.User_Id = Convert.ToInt32(user_id); // insert user_id from session
            model.Item_Id = id; // insert item_id from item
            var myContent = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            var result = client.PostAsync("Borrow/PostBorrow", byteContent).Result;
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

        public JsonResult LoadItemAdmin()
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            List<Item> data = new List<Item>();
            var responseTask = client.GetAsync("Item/GetItemAdmin");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<List<Item>>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, please try again");
            }

            return Json(data);

        }

        public JsonResult InsertOrUpdate(Item item)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var myContent = JsonConvert.SerializeObject(item);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            if (item.Id == 0)
            {
                var result = client.PostAsync("Item/PostItem", byteContent).Result;
                return Json(result);
            }
            else
            {
                var result = client.PutAsync("Item/PutItem/" + item.Id, byteContent).Result;
                return Json(result);
            }
        }

        public JsonResult GetById(int id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            Item data = null;
            var responseTask = client.GetAsync("Item/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<Item>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, please try again");
            }

            return Json(data);

        }

        public JsonResult Delete(Item item, int Id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var myContent = JsonConvert.SerializeObject(item);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = client.PutAsync("Item/DeleteItem/" + Id, byteContent).Result;
            return Json(result);

        }
    }
}