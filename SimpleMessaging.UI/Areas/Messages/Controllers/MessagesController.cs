using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleMessaging.Message.Repository.Interfaces;

namespace SimpleMessaging.UI
{
    public class MessagesController : Controller
    {
        private readonly IMesssageRepository repository;

        public MessagesController(IMesssageRepository repository)
        {
            this.repository = repository;
        }

        // GET: MessagesController
        public async Task<ActionResult> Index()
        {
            var messages = await repository.GetMessagesAsync();

            return View(messages);
        }

        // GET: MessagesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MessagesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MessagesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MessagesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MessagesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MessagesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MessagesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
