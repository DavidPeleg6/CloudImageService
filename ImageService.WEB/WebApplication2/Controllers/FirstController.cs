using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Communication.Client;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class FirstController : Controller
    {
        #region MenuLinks
        public ActionResult MainPage()
        {
            return View();
        }
        public ActionResult Config()
        {
            return View();
        }
        public ActionResult Photos()
        {
            return View();
        }
        public ActionResult Logs()
        {
            return View(logs);
        }
        #endregion

        static Client LocalClient = Client.GetInstance;

        public bool IsClientRunning()
        {
            return LocalClient.GetStatus();
        }

        //shit below is stuff from the example itself
        //TODO: delete it
        
        static List<LogData> logs = new List<LogData>()
        {
          new LogData  { Type = "qqqqqqqqq", Message="sfaaaaaaa" },
          new LogData  { Type = "fasasf", Message="sfafas" },
          new LogData  { Type = "AESaasfFIGH", Message="AAAAdvafasAAAAA" },
          new LogData  { Type = "afsasfasf", Message="ssssssssssssssssss" },
          new LogData  { Type = "asfrwr", Message="gaasgggggggggggggggggggg" }
        };
        [HttpPost]
        public JObject GetLog(string name, int salary)
        {
            foreach (var log in logs)
            {
                JObject data = new JObject();
                data["Type"] = log.Type;
                data["Message"] = log.Message;
                return data;
            }
            return null;
        }
        /*[HttpGet]
        public JObject GetEmployee()
        {
            JObject data = new JObject();
            data["FirstName"] = "Kuky";
            data["LastName"] = "Mopy";
            return data;
        }

        [HttpPost]
        public JObject GetEmployee(string name, int salary)
        {
            foreach (var empl in employees)
            {
                if (empl.Salary > salary || name.Equals(name))
                {
                    JObject data = new JObject();
                    data["FirstName"] = empl.FirstName;
                    data["LastName"] = empl.LastName;
                    data["Salary"] = empl.Salary;
                    return data;
                }
            }
            return null;
        }

        // GET: First/Details
        public ActionResult Details()
        {
            return View(employees);
        }

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
