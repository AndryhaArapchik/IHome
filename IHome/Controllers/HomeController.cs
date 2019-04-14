using Services;
using Services.Entities;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RGB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogService logService;
        private readonly ILedControlService ledControlService;
        private readonly IAudioService audioService;
        public HomeController(ILogService logService, ILedControlService ledControlService, IAudioService audioService)
        {
            this.logService = logService;
            this.ledControlService = ledControlService;
            this.audioService = audioService;
        }
        public ActionResult Index()
        {
            string color = string.Empty;
            if (!ledControlService.IsOpen)
            {
                try
                {
                    color = ledControlService.GetColor();
                }
                catch (Exception e)
                {
                    ViewBag.CurrentColor = "rgb(0,0,0)";
                    ViewBag.Exception = e.Message;
                    logService.Write(new LogMessage(e));
                }
            }
            ViewBag.CurrentVolume = audioService.GetValueVolume();
            ViewBag.CurrentColor = color;
            return View();
        }

        [HttpPost]
        public JsonResult SetVolume(int volume)
        {
            try
            {
                audioService.ChangeVolume(volume);
                return Json("Ok");
            }
            catch (Exception)
            {
                return Json("Error");
            }
            
            
        }
        [HttpPost]
        public JsonResult SetColor(string color)
        {
            
            if (!ledControlService.IsOpen)
            {
                try
                {
                    ledControlService.SetColor(color);
                }
                catch (UnauthorizedAccessException e)
                {
                    logService.Write(new LogMessage(e));
                    return Json("Not access");
                    
                }
            }
            else
            {
                return Json("Error");
            }

            return Json("Ok");
        }
    }
}