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
            configData = ConfigWindowModel.HandelerList;
            return View(configData);//TODO: maybe just put the thingy in here
        }
        public ActionResult Photos()
        {
            return View();
        }
        public ActionResult Logs()
        {
            logs = LogWindowModel.LogList;
            return View(logs);//TODO: maybe just put the thingy in here
        }
        #endregion

        #region Logs
        static List<LogData> logs = new List<LogData>();
        #endregion

        #region Config
        static List<HandelerData> configData = new List<HandelerData>();
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
        #endregion

        //shit below is stuff from the example itself
        //TODO: delete it

        /*

        // GET: First/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: First/Create
        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            try
            {
                employees.Add(emp);

                return RedirectToAction("Details");
            }
            catch
            {
                return View();
            }
        }

        // GET: First/Edit/5
        public ActionResult Edit(int id)
        {
            foreach (Employee emp in employees) {
                if (emp.ID.Equals(id)) { 
                    return View(emp);
                }
            }
            return View("Error");
        }

        // POST: First/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Employee empT)
        {
            try
            {
                foreach (Employee emp in employees)
                {
                    if (emp.ID.Equals(id))
                    {
                        emp.copy(empT);
                        return RedirectToAction("Index");
                    }
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Error");
            }
        }

        // GET: First/Delete/5
        public ActionResult Delete(int id)
        {
            int i = 0;
            foreach (Employee emp in employees)
            {
                if (emp.ID.Equals(id))
                {
                    employees.RemoveAt(i);
                    return RedirectToAction("Details");
                }
                i++;
            }
            return RedirectToAction("Error");
        }
        */
    }
}
