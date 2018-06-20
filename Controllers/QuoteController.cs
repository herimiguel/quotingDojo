using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using DbConnection;

namespace quotingDojo.Controllers
{
    public class QuoteController : Controller
    {
     
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(TempData["Error"] !=null ){
                ViewBag.Error = TempData["Error"];
            }
            return View();
        }


        [HttpGet]
        [Route("/quotes")]
        public IActionResult Quotes()
        {
            List<Dictionary<string, object>> AllQuotes = DbConnector.Query("SELECT * FROM users");
            ViewBag.Quotes = AllQuotes;
            return View("quotes");
        }

        [HttpPost]
        [Route("/addQuote")]
        public IActionResult addQuote(string name, string quote)
        {
             if(name ==""){
                TempData["Error"] = "Name cannot be empty";
                Console.WriteLine("name error");
            return RedirectToAction("index");
        
            }
            if(quote == ""){
                TempData["Error"] = "Quote field cannot be empty";
                Console.WriteLine("quote error");
                
            return RedirectToAction("index");
            }

            DbConnector.Execute($"INSERT INTO users (name, quote, created_at) VALUES ('{name}', '{quote}', NOW())");
            
            return RedirectToAction ("quotes");
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult delete(int id)
        {
            DbConnector.Execute($"DELETE FROM users Where id = {id}");
            return RedirectToAction ("quotes");
        }
    }
}
