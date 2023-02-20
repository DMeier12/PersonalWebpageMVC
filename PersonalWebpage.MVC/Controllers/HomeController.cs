using Microsoft.AspNetCore.Mvc;
using PersonalWebpage.MVC.Models;
using System.Diagnostics;
using System.Net.Mail;

namespace PersonalWebpage.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public IActionResult Index()
        {
            return View(new ContactViewModel());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            //var test = Request.Form["Email"];
            //var test2 = Request.Form["Name"];
            //var test3 = Request.Form["Message"];
            //var test4 = Request.Form["Subject"];
            SendEmail(Request.Form["Email"], Request.Form["Subject"], Request.Form["Name"],Request.Form["Message"]);
            return View("ThankYou");
        }

        private void SendEmail(string Email, string Subject, string Name, String Message)
        {
            var to = System.Configuration.ConfigurationManager.AppSettings["EmailSettings"];
            var message = new MailMessage(Email, to??"Dawson.A.Meier@gmail.com", Subject, "Name:" + Name + "Message:" + Message);
            SmtpClient client = new SmtpClient("mail.dawsonmeierdev.com");
            client.UseDefaultCredentials = true;

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                    ex.ToString());
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}