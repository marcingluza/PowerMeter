﻿using PowerMeter.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PowerMeter.Controllers
{
    public class ArduinoController : Controller
    {

        // 
        // GET: /Arduino  
        public ActionResult Index()
        { 
            return View(Startup.db.record.ToList());
        }

        // 
        // POST: /Arduino/Add 
        public HttpStatusCode Add(string id, int voltage, float l1_current, float l2_current, float l3_current)
        {
            StatsViewModel SVM = new StatsViewModel(Startup.DeviceList.Devices.Find(x => x.devID == id));
            if (Startup.DeviceList.checkExist(id))
            {
                record tempRecord = new record(0, Startup.DeviceList.getId(id), DateTime.Now, voltage, (decimal)l1_current, (decimal)l2_current, (decimal)l3_current);
                Startup.db.record.Add(tempRecord);
                Startup.db.SaveChanges();
                return HttpStatusCode.OK;
            }
            else
                return HttpStatusCode.Unauthorized;
        }

    }
}