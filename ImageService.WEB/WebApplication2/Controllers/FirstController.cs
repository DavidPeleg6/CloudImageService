using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Communication.Client;
using ImageService.WEB.Models;

namespace WebApplication2.Controllers
{
    public class FirstController : Controller
    {
        #region Models and client
        Client LocalClient = Client.GetInstance;
        LogsTabViewModel LogWindowModel = new LogsTabViewModel();
        ConfigTabViewModel ConfigWindowModel = new ConfigTabViewModel();
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
        public ActionResult HandelerCloseConfirm(String handeler)
        {
            ViewBag.handler = RouteData.Values["id"];
            return View();
        }
        #endregion


    }
}
