using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Poop_MVC.Models;

namespace Poop_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Reverse()
        {
            PalindromeModel model = new();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reverse(PalindromeModel palindrome)
        {
            string inputWord = palindrome.InputWord;

            string revWord = "";

            for (int i = inputWord.Length - 1; i >= 0; i--)
            {
                revWord += inputWord[i];
            }

            palindrome.RevWord = revWord;

            revWord = Regex.Replace(revWord.ToLower(),"[^a-zA-Z0-9]+","" );
            inputWord = Regex.Replace(inputWord.ToLower(),"[^a-zA-Z0-9]+","");

            if (revWord == inputWord)
            {
                palindrome.IsPalindrome = true;
                palindrome.Message = $"Success '{palindrome.InputWord}' is a Palindrome";
            }
            else
            {
                palindrome.IsPalindrome = false;
                palindrome.Message = $"Sorry, '{palindrome.InputWord}' is not a Palindrome";
            }

            return View(palindrome);
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
