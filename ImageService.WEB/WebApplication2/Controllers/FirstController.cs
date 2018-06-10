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
    /// <summary>
    /// Controller, manages the connection between the views and the viewmodels.
    /// Also, starts the client when the main page is loaded.
    /// </summary>
    public class FirstController : Controller
    {
        //The client is here because it is neccary to start it up once when the window is loaded.
        //Besides it there also the viewmodels that the views use.
        #region Models and client
        Client LocalClient = Client.GetInstance;
        LogsTabViewModel LogWindowModel = new LogsTabViewModel();
        ConfigTabViewModel ConfigWindowModel = new ConfigTabViewModel();
        MainTabViewModel MainWindowModel = new MainTabViewModel();
        #endregion

        //These are used by the links in the top of the screen.
        //These also the client startup in the mainpage link.
        //(i.e when the main page is loaded, the client starts up
        //unless it was already running)
        #region MenuLinks
        /// <summary>
        /// Links to the main page.
        /// </summary>
        /// <returns>A view of the main page.</returns>
        public ActionResult MainPage()
        {
            if (!LocalClient.GetStatus())
            {
                LocalClient.ClientStart();
            }
            return View();
        }
        /// <summary>
        /// Links to the config page.
        /// </summary>
        /// <returns>A view of the config page.</returns>
        public ActionResult Config()
        {
            return View(ConfigWindowModel.HandelerList);
        }
        /// <summary>
        /// Links to the photos page.
        /// </summary>
        /// <returns>A view of the photos page.</returns>
        public ActionResult Photos()
        {
            return View();
        }
        /// <summary>
        /// Links to the logs page.
        /// </summary>
        /// <returns>A view of the logs page.</returns>
        public ActionResult Logs()
        {
            return View(LogWindowModel.LogList);
        }
        #endregion

        //Functions used by the config window
        #region Config
        /// <summary>
        /// Returns the output directory as a JSON formatted object.
        /// </summary>
        /// <returns>The output directory as a JSON formatted object.</returns>
        [HttpGet]
        public JObject GetOutputDirectory()
        {
            JObject data = new JObject();
            data["text"] = ConfigWindowModel.OutputDirectory;
            return data;
        }
        /// <summary>
        /// Returns the source name as a JSON formatted object.
        /// </summary>
        /// <returns>The source name as a JSON formatted object.</returns>
        [HttpGet]
        public JObject GetSourceName()
        {
            JObject data = new JObject();
            data["text"] = ConfigWindowModel.SourceName;
            return data;
        }
        /// <summary>
        /// Returns the log name as a JSON formatted object.
        /// </summary>
        /// <returns>The log name as a JSON formatted object.</returns>
        [HttpGet]
        public JObject GetLogName()
        {
            JObject data = new JObject();
            data["text"] = ConfigWindowModel.LogName;
            return data;
        }
        /// <summary>
        /// Returns the thumbnail size as a JSON formatted object.
        /// </summary>
        /// <returns>The thumbnail size as a JSON formatted object.</returns>
        [HttpGet]
        public JObject GetThumbnailSize()
        {
            JObject data = new JObject();
            data["text"] = ConfigWindowModel.ThumbnailSize;
            return data;
        }
        /// <summary>
        /// Returns the handeler closing confirmation page.
        /// </summary>
        /// <param name="handeler">The handeler to be closed.</param>
        /// <returns>The handeler closing confirmation page.</returns>
        [HttpGet]
        public ActionResult HandelerCloseConfirm(String handeler)
        {
            ViewBag.handler = RouteData.Values["id"];
            return View();
        }
        /// <summary>
        /// Removes the handeler
        /// </summary>
        /// <param name="handeler">The handeler to be removed.</param>
        /// <returns>True if removed, false otherwise.</returns>
        [HttpGet]
        public JObject RemoveHandeler(String handeler)
        {
            JObject data = new JObject();
            data["text"] = ConfigWindowModel.RemoveHandeler(new HandelerData() { Text = handeler });
            return data;
        }
        #endregion

        //Functions used by the main window
        #region Main Page
        /// <summary>
        /// Returns the state of the service as a JSON formatted object.
        /// </summary>
        /// <returns>The state of the service as a JSON formatted object.</returns>
        [HttpGet]
        public JObject GetRunning()
        {
            JObject data = new JObject();
            data["text"] = MainWindowModel.Running;
            return data;
        }
        /// <summary>
        /// Returns the image count as a JSON formatted object.
        /// </summary>
        /// <returns>The image count as a JSON formatted object.</returns>
        [HttpGet]
        public JObject GetImageCount()
        {
            JObject data = new JObject();
            data["text"] = MainWindowModel.ImageCount;
            return data;
        }
        /// <summary>
        /// Returns the name of the first developer as a JSON formatted object.
        /// </summary>
        /// <returns>The name of the first developer as a JSON formatted object.</returns>
        [HttpGet]
        public JObject GetName1()
        {
            JObject data = new JObject();
            data["text"] = MainWindowModel.Name1;
            return data;
        }
        /// <summary>
        /// Returns the name of the other developer as a JSON formatted object.
        /// </summary>
        /// <returns>The name of the other developer as a JSON formatted object.</returns>
        [HttpGet]
        public JObject GetName2()
        {
            JObject data = new JObject();
            data["text"] = MainWindowModel.Name2;
            return data;
        }
        /// <summary>
        /// Returns the ID of one of the developers as a JSON formatted object.
        /// </summary>
        /// <returns>The ID of one of the developers as a JSON formatted object.</returns>
        [HttpGet]
        public JObject GetID1()
        {
            JObject data = new JObject();
            data["text"] = MainWindowModel.ID1;
            return data;
        }
        /// <summary>
        /// Returns the ID of the other developer as a JSON formatted object.
        /// </summary>
        /// <returns>The ID of the other developer as a JSON formatted object.</returns>
        [HttpGet]
        public JObject GetID2()
        {
            JObject data = new JObject();
            data["text"] = MainWindowModel.ID2;
            return data;
        }
        #endregion
    }
}
