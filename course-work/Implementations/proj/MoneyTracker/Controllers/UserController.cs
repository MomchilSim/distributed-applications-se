using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using MoneyTracker.Auth;
using System.Text;
using System.Text.Json.Serialization;
using X.PagedList;

namespace MoneyTracker.Controllers
{
    public class UserController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5161");
        private readonly HttpClient _client;
        

        public UserController()
        {
            _client = new HttpClient();
           // _client.DefaultRequestHeaders.Add(AuthConstants.Key, AuthConstants.Val);
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index(int? page, string? username)
        {
            List<UserModel> userList = new List<UserModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "users").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                userList = JsonConvert.DeserializeObject<List<UserModel>>(data);
            }
            if (username != null)
            {
                userList = userList.Where(u => u.UserName.ToLower() == username.ToLower()).ToList();
            }
            int pageNum = page??1;
            int pageSize = 4;
            var pagedUsers = userList.ToPagedList(pageNum, pageSize);

            return View(pagedUsers);

        }
        
        [HttpGet, ActionName("Details")]
        public IActionResult GetId(int id)
        {
            UserModel user = new UserModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"users/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<UserModel>(data); 
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        public IActionResult Create(UserModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage resp = _client.PostAsync(_client.BaseAddress + "users", content).Result;
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            UserModel user = new UserModel();
            HttpResponseMessage resp = _client.GetAsync(_client.BaseAddress + $"users/{id}").Result;
            if (resp.IsSuccessStatusCode)
            {
                string data = resp.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<UserModel>(data); 
            }
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage resp = _client.DeleteAsync(_client.BaseAddress + $"users/{id}").Result;
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            UserModel user = new UserModel();
            HttpResponseMessage resp = _client.GetAsync(_client.BaseAddress + $"users/{id}").Result;
            if (resp.IsSuccessStatusCode)
            {
                string data = resp.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<UserModel>(data);
            }
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(UserModel user)
        {
            string data = JsonConvert.SerializeObject(user);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage resp = _client.PutAsync(_client.BaseAddress + $"users/{user.UserId}", content).Result;
            if(resp.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
