using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using QRCoder;
using ZXing;
using ZXing.QrCode;

namespace Client.Controllers
{
    public class BorrowController : Controller
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

        public JsonResult LoadBorrowById()
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var id = HttpContext.Session.GetString("Id"); ; // session user id
            List<BorrowVM> data = new List<BorrowVM>();
            var responseTask = client.GetAsync("Borrow/GetBorrowUser/" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<List<BorrowVM>>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, please try again");
            }

            return Json(data);

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

        public JsonResult LoadBorrowAdmin()
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            List<BorrowVM> data = new List<BorrowVM>();
            var responseTask = client.GetAsync("Borrow/GetBorrowAdmin");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<List<BorrowVM>>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, please try again");
            }

            return Json(data);

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

        public JsonResult LoadBorrowApp1()
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            List<BorrowVM> data = new List<BorrowVM>();
            var responseTask = client.GetAsync("Borrow/GetApproval1");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<List<BorrowVM>>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, please try again");
            }

            return Json(data);

        }

        public JsonResult AcceptApproval1(Borrow model, int id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var myContent = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Borrow/PutApproval1/" + id, byteContent).Result;
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

        public JsonResult LoadBorrowApp2()
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            List<BorrowVM> data = new List<BorrowVM>();
            var responseTask = client.GetAsync("Borrow/GetApproval2");
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<List<BorrowVM>>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "server error, please try again");
            }

            return Json(data);

        }

        public JsonResult AcceptApproval2(BorrowVM model)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var myContent = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Borrow/PutApproval2/" + model.Id, byteContent).Result;
            if (result.IsSuccessStatusCode)
            {
                string filename = GenerateFileName();
                GenerateQRCode(filename, model);
                SendEmail(filename, model);
            }
            return Json(result);

        }

        public string GenerateFileName()
        {
            string fileGuid = Guid.NewGuid().ToString().Substring(0, 4);
            string tempFileName = "Image/QRCode-Borrow-" + fileGuid + ".png";
            return tempFileName;
        }

        public void GenerateQRCode(string tempFileName, BorrowVM model)
        {
            string qrText = Convert.ToString(model.Item_Id);

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrText, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Image image = new Bitmap(qrCode.GetGraphic(20));
            image.Save(tempFileName);

        }

        public void SendEmail(string filename, BorrowVM model)
        {

            // Credentials
            var credentials = new NetworkCredential("yrsproject15@gmail.com", "yarsiproject2015");

            // Mail message
            var mail = new MailMessage()
            {
                From = new MailAddress("yrsproject15@gmail.com"), // email from
                Subject = "Borrow Item QR Code " + DateTimeOffset.Now,
                Body = string.Format("Hi,<br /><br />your borrow item has been approved. This is your QR Code.<br /><br />Thank You.")
            };

            mail.Attachments.Add(new Attachment(filename));

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

        // DECLINE APPROVAL
        public JsonResult DeclineApproval(Borrow model, int id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("JWTToken"));
            var myContent = JsonConvert.SerializeObject(model);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var result = client.PutAsync("Borrow/DeclineApproval/" + id, byteContent).Result;
            return Json(result);

        }

    }
}