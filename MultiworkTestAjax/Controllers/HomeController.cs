using System.Diagnostics;
using System.Threading;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using MultiworkTestAjax.Models;

namespace MultiworkTestAjax.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Validate(string firstName, string lastName, string phoneNumber)
        {
            Thread.Sleep(10000);

            string errorMessage = "<font color=\"red\">Ошибки содержат следующие поля:";
            bool isAnyInvalid = false;
            if (firstName == null)
            {
                errorMessage += " Имя";
                isAnyInvalid = true;
            }
            if (lastName == null)
            {
                if (isAnyInvalid)
                    errorMessage += ",";
                errorMessage += " Фамилия";
                isAnyInvalid = true;
            }
            if (phoneNumber == null || !IsPhoneNumberValid(phoneNumber))
            {
                if (isAnyInvalid)
                    errorMessage += ",";
                errorMessage += " Номер телефона";
                isAnyInvalid = true;
            }
            errorMessage += "</font>";

            return Json( new {
                isValid = !isAnyInvalid,
                message = isAnyInvalid
                    ? errorMessage
                    : "<font color=\"green\">Принято</font>"
            });
        }

        private bool IsPhoneNumberValid(string phoneNumber)
        {
            Regex regex = new Regex(@"^[+0]\d+");
            return regex.IsMatch(phoneNumber);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
