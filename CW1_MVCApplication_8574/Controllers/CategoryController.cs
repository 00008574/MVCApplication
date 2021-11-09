using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CW1_MVCApplication_8574.Data;
using CW1_MVCApplication_8574.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace CW1_MVCApplication_8574.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CW1_MVCApplication_8574Context _context;
        private string Baseurl = "https://localhost:44397/";
        public CategoryController(CW1_MVCApplication_8574Context context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            //Hosted web API REST Service base url
            string Baseurl = "https://localhost:44397/";
            List<Category> CatInfo = new List<Category>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new
               MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Category");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Product list
                    CatInfo = JsonConvert.DeserializeObject<List<Category>>(Response);
                }
                //returning the Product list to view
                return View(CatInfo);
            }
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            string Baseurl = "https://localhost:44397/";
            Category category = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync("api/Category/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var CatResponse = Res.Content.ReadAsStringAsync().Result;

                    category = JsonConvert.DeserializeObject<Category>(CatResponse);
                }
                else
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }

            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update logic here
                using (var client = new HttpClient())
                {
                    var randomNumber = new Random();
                    category.Id = randomNumber.Next(200);
                    client.BaseAddress = new Uri(Baseurl);
                    var postTask = await client.PostAsJsonAsync<Category>("api/Category", category);
                    if (postTask.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(category);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            string Baseurl = "https://localhost:44397/";
            Category category = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync("api/Category/" + id);
                if (Res.IsSuccessStatusCode)
                {
                    var Response = Res.Content.ReadAsStringAsync().Result;
                    category = JsonConvert.DeserializeObject<Category>(Response);
                }
                else
                    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");
            }
            return View(category);

        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Category category)
        {
            if(id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // TODO: Add update logic here
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);
                        HttpResponseMessage Res = await client.GetAsync("api/Category/" + id);
                        Product products = null;
                        //Checking the response is successful or not which is sent using HttpClient
                        if (Res.IsSuccessStatusCode)
                        {
                            //Storing the response details recieved from web api
                            var Response = Res.Content.ReadAsStringAsync().Result;
                            //Deserializing the response recieved from web api and storing into the Product list
                            products = JsonConvert.DeserializeObject<Product>(Response);
                        }
                        //HTTP POST
                        var postTask = client.PutAsJsonAsync<Category>("api/Category/" + category.Id, category);
                        postTask.Wait();
                        var result = postTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                    }

                    return RedirectToAction("Index");

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Category category = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.GetAsync("api/Category/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    var ProdResponse = Res.Content.ReadAsStringAsync().Result;

                    category = JsonConvert.DeserializeObject<Category>(ProdResponse);
                }
            }
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string Baseurl = "https://localhost:44397/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Baseurl);
                HttpResponseMessage Res = await client.DeleteAsync("api/Category/" + id);

                if (Res.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
