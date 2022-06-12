using CarPark.User1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CarPark.User1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            var cultureInfo = CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture= cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;


            var say_Hello_value = _localizer["Say_Hello"];

          

            var settings = MongoClientSettings.FromConnectionString("mongodb+srv://melihokat:melih350@cluster0.actcp.mongodb.net/Melih?retryWrites=true&w=majority");
            var client = new MongoClient(settings);
            var database = client.GetDatabase("Melih");
            var collection = database.GetCollection<Test>("Test");

            var test = new Test()
            {
                _Id = ObjectId.GenerateNewId(),
                NameSurname = "Melih Okat",
                Age = 28,
                AddressList = new List<Address>()
                {
                    new Address
                    {
                    Title="Ev Adresi",
                    Description="Ankara Dikmen Caddesi 160/5"
                    },
                    new Address
                    {
                        Title="Baba Evi",
                        Description="Etimesgut/Ankara"
                    }

                }


            };
            collection.InsertOne(test); //MongoDbDriver ile gelen özellik
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
