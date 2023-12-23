using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HospitalApp.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;

namespace HospitalApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public  readonly IStringLocalizer<HomeController> _localization;
    public HomeController(ILogger<HomeController> logger , IStringLocalizer<HomeController> localization)
    {
        _localization = localization;
        _logger = logger;
    }

   

    public IActionResult Index()
    {
        ViewData["Title"] = _localization["HospitalName"];
        var localizedTitle = _localization["Welcome"];
        var localized = _localization["HomeİstanbulTitle"];
        return View();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
