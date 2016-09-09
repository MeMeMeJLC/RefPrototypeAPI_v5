using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Prototype_Web_App.Models;

namespace Prototype_Web_App.Controllers
{
    public class RefereeInfoController : Controller
    {
        HttpClient client;
        //the url of the WEB API Service
        string url = "http://refprototypeapiv5.azurewebsites.net/api/referees";

        //The HttpClient Class, this will be used for performing 
        //HTTP Operations, GET, POST, PUT, DELETE
        //Set the base address and the Header Formatter

            public RefereeInfoController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        }

        // GET: RefereeInfo
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage responseMessage = await client.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData = responseMessage.Content.ReadAsStringAsync().Result;

                var Referees = JsonConvert.DeserializeObject<List<RefereeInfo>>(responseData);

                return View(Referees);
            }
            return View("Error");
        }
        public ActionResult Create()
        {
            return View(new RefereeInfo());
        }
        /*public ActionResult Create()
        {
            return View(new EmployeeInfo());
        }*/
    }
}