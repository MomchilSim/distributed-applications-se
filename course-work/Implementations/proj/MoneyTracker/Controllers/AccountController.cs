using Microsoft.AspNetCore.Mvc;
using MoneyTracker.Models;
using Newtonsoft.Json;
using System.Text;
using X.PagedList;

namespace MoneyTracker.Controllers
{
    public class AccountController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5161");
        private readonly HttpClient _client;
        public AccountController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index(int? page, string? accname)
        {
            List<AccountDisplayModel> accountList = new List<AccountDisplayModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "accounts").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                try
                {
                    accountList = JsonConvert.DeserializeObject<List<AccountDisplayModel>>(data);
                }
                catch (JsonSerializationException ex)
                {
                    // Log or handle the exception
                    Console.WriteLine($"Serialization error: {ex.Message}");
                }
            }
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                accountList = JsonConvert.DeserializeObject<List<AccountDisplayModel>>(data);
            }
            if (accname != null)
            {
                accountList = accountList.Where(u => u.AccountName.ToLower() == accname.ToLower()).ToList();
            }
            int pageNum = page ?? 1;
            int pageSize = 4;
            var pagedAccounts = accountList.ToPagedList(pageNum, pageSize);
            return View(pagedAccounts);
        }
        [HttpGet, ActionName("Details")]
        public IActionResult GetId(int id)
        {
            AccountModel account = new AccountModel();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"accounts/{id}").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                account = JsonConvert.DeserializeObject<AccountModel>(data);
            }
            return View(account);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost, ActionName("Create")]
        public IActionResult Create(AccountModel model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage resp = _client.PostAsync(_client.BaseAddress + "accounts", content).Result;
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            AccountModel user = new AccountModel();
            HttpResponseMessage resp = _client.GetAsync(_client.BaseAddress + $"accounts/{id}").Result;
            if (resp.IsSuccessStatusCode)
            {
                string data = resp.Content.ReadAsStringAsync().Result;
                user = JsonConvert.DeserializeObject<AccountModel>(data);
            }
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            HttpResponseMessage resp = _client.DeleteAsync(_client.BaseAddress + $"accounts/{id}").Result;
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            AccountModel model = new AccountModel();
            HttpResponseMessage resp = _client.GetAsync(_client.BaseAddress + $"accounts/{id}").Result;
            if (resp.IsSuccessStatusCode)
            {
                string data = resp.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<AccountModel>(data);
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(AccountModel account)
        {
            string data = JsonConvert.SerializeObject(account);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage resp = _client.PutAsync(_client.BaseAddress + $"accounts/{account.AccountId}", content).Result;
            if (resp.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
