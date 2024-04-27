using System.Diagnostics;
using LinkShortener.Entities;
using Microsoft.AspNetCore.Mvc;
using LinkShortener.Models;
using LinkShortener.Services.Interfaces;

namespace LinkShortener.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ILinkService _linkService;
    public HomeController(ILogger<HomeController> logger, ILinkService linkService)
    {
        _logger = logger;
        _linkService = linkService;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var links = await _linkService.GetAllLinksAsync();
            
            return View(links);
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(string fullUrl)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View("Create");
            }
            
            await _linkService.CreateLinkAsync(fullUrl);
            
            return Redirect("/");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("Error", ex.Message);
            return View("Create");
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Update(Guid id)
    {
        try
        {
            var link = await _linkService.GetLinkByIdAsync(id);
            
            return View(link);
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Update(Link link)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return View(link);
            }
            
            await _linkService.UpdateLinkAsync(link);
            
            return Redirect("/");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("Error", ex.Message);
            return View(link);
        }
    }
    
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _linkService.DeleteLinkAsync(id);
            
            return Redirect("/");
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    [HttpGet("/{shortUrl}")]
    public async Task<IActionResult> RedirectUrl(string shortUrl)
    {
        try
        {
            var link = await _linkService.GetLinkByShortUrl(shortUrl);

            link.CountClicks += 1;
            await _linkService.UpdateLinkAsync(link);
            
            return Redirect(link.FullUrl);
        }
        catch (Exception ex)
        {
            return View("Error", new ErrorViewModel() { RequestId = ex.Message });
        }
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}