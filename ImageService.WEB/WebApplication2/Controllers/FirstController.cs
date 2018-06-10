using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Communication.Client;
using ImageService.WEB.Models;
using System.IO;

namespace WebApplication2.Controllers
{
    public class FirstController : Controller
    {
        #region Models and client
        Client LocalClient = Client.GetInstance;
        LogsTabViewModel LogWindowModel = new LogsTabViewModel();
        ConfigTabViewModel ConfigWindowModel = new ConfigTabViewModel();
        PhotoTabModel PhotoWindowModel = new PhotoTabModel();
        #endregion

        #region MenuLinks
        public ActionResult MainPage()
        {
            LocalClient.ClientStart();
            return View();
        }
        public ActionResult Config()
        {
            return View(ConfigWindowModel.HandelerList);
        }
        public ActionResult Photos()
        {
            ViewBag.photos = PhotoWindowModel.PhotoAddresses(Server.MapPath("~/App_Data"));
            ViewBag.Deleted = RouteData.Values["id"];
            return View();
        }
        public ActionResult Logs()
        {
            return View(LogWindowModel.LogList);
        }
        #endregion

        #region Config
        [HttpGet]
        public JObject GetOutputDirectory()
        {
            JObject data = new JObject();
            data["text"] = ConfigWindowModel.OutputDirectory;
            return data;
        }
        [HttpGet]
        public JObject GetSourceName()
        {
            JObject data = new JObject();
            data["text"] = ConfigWindowModel.SourceName;
            return data;
        }
        [HttpGet]
        public JObject GetLogName()
        {
            JObject data = new JObject();
            data["text"] = ConfigWindowModel.LogName;
            return data;
        }
        [HttpGet]
        public JObject GetThumbnailSize()
        {
            JObject data = new JObject();
            data["text"] = ConfigWindowModel.ThumbnailSize;
            return data;
        }
        [HttpGet]
        [ActionName("DeleteImage")]
        public ActionResult DeleteImage(String year, String month, String file)
        {
            PhotoWindowModel.DeleteImage(Server.MapPath("~/App_Data"), year, month, file);
            return RedirectToAction("Photos/true");
        }
        [HttpGet]
        public ActionResult HandelerCloseConfirm(String handeler)
        {
            ViewBag.handler = RouteData.Values["id"];
            return View();
        }
        [HttpGet]
        [ActionName("ViewPage")]
        public ActionResult ViewPage(String year, String month, String file)
        {
            ViewBag.Year = year;
            ViewBag.Month = month;
            ViewBag.Name = file;
            return View();
        }
        [HttpGet]
        [ActionName("ImageDeleteConfirm")]
        public ActionResult ImageDeleteConfirm(String year, String month, String file)
        {
            ViewBag.Year = year;
            ViewBag.Month = month;
            ViewBag.Name = file;
            return View();
        }
        #endregion


    }
}
