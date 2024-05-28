using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MoneyTracker.Models;
using Newtonsoft.Json;
using System.Text;
using X.PagedList;

namespace MoneyTracker.Controllers
{
    public class TransactionController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:5161");
        private readonly HttpClient _client;
        public TransactionController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index(int? page, string? trnname)
        {
            List<TransactionDisplayModel> transactionList = new List<TransactionDisplayModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "transactions").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                try
                {
                    transactionList = JsonConvert.DeserializeObject<List<TransactionDisplayModel>>(data);
                }
                catch (JsonSerializationException ex)
                {
                    // Log or handle the exception
                    Console.WriteLine($"Serialization error: {ex.Message}");
                }
            }
            if (trnname != null)
            {
                transactionList = transactionList.Where(u => u.Category.ToLower() == trnname.ToLower()).ToList();
            }
            int pageNum = page ?? 1;
            int pageSize = 4;
            var pagedTransactions = transactionList.ToPagedList(pageNum, pageSize);
            return View(pagedTransactions);
        }
        [HttpGet, ActionName("Details")]
public IActionResult GetId(int id)
{
    TransactionModel transaction = new TransactionModel();
    HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + $"transactions/{id}").Result;
    if (response.IsSuccessStatusCode)
    {
        string data = response.Content.ReadAsStringAsync().Result;
        transaction = JsonConvert.DeserializeObject<TransactionModel>(data);
    }
    return View(transaction);
}
public IActionResult Create()
{
    return View();
}
[HttpPost, ActionName("Create")]
public IActionResult Create(TransactionModel model)
{
    string data = JsonConvert.SerializeObject(model);
    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
    HttpResponseMessage resp = _client.PostAsync(_client.BaseAddress + "transactions", content).Result;
    if (resp.IsSuccessStatusCode)
    {
        return RedirectToAction("Index");
    }
    return View();
}
[HttpGet]
public IActionResult Delete(int id)
{
            TransactionModel user = new TransactionModel();
    HttpResponseMessage resp = _client.GetAsync(_client.BaseAddress + $"transactions/{id}").Result;
    if (resp.IsSuccessStatusCode)
    {
        string data = resp.Content.ReadAsStringAsync().Result;
        user = JsonConvert.DeserializeObject<TransactionModel>(data);
    }
    return View(user);
}
[HttpPost, ActionName("Delete")]
public IActionResult DeleteConfirmed(int id)
{
    HttpResponseMessage resp = _client.DeleteAsync(_client.BaseAddress + $"transactions/{id}").Result;
    if (resp.IsSuccessStatusCode)
    {
        return RedirectToAction("Index");
    }
    return View();
}
[HttpGet]
public IActionResult Edit(int id)
{
    TransactionModel model = new TransactionModel();
    HttpResponseMessage resp = _client.GetAsync(_client.BaseAddress + $"transactions/{id}").Result;
    if (resp.IsSuccessStatusCode)
    {
        string data = resp.Content.ReadAsStringAsync().Result;
        model = JsonConvert.DeserializeObject<TransactionModel>(data);
    }
    return View(model);
}
[HttpPost]
public IActionResult Edit(TransactionModel transaction)
{
    string data = JsonConvert.SerializeObject(transaction);
    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
    HttpResponseMessage resp = _client.PutAsync(_client.BaseAddress + $"transactions/{transaction.TransactionID}", content).Result;
    if (resp.IsSuccessStatusCode)
    {
        return RedirectToAction("Index");
    }
    return View();
}
    }
}
