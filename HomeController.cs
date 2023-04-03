using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace WebApplication
{
    public class HomeController : Controller
    {
        private EntityModelContext _context=null;
        public HomeController(EntityModelContext context)
        {
            _context = context;
        }

        [Route("home/index")]
        public IActionResult Index()
        {
            List<Contact> data = null;
            data = _context.Contacts.ToList();
            return View(data);
        }
        
        [Route("home/edit/{id?}")]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Contact data = new Contact();
            if(id.HasValue)
            {
                data = _context.Contacts.Where(p=>p.Id==id.Value).FirstOrDefault();
                if(data==null)
                {
                    data = new Contact();
                }
                return View("Edit",data);
            }
            return View("Edit",data);
        }

        [Route("home/post")]
        [HttpPost]
        public IActionResult Post(Contact contact)
        {
            if(contact != null)
            {
                bool isNew = false;
                var data = _context.Contacts.Where(p=>p.Id== contact.Id).FirstOrDefault();
                if(data == null)
                {
                    data = new Contact();
                    isNew = true;
                }
                data.FirstName = contact.FirstName;
                data.LastName = contact.LastName;
                data.Email = contact.Email;
                data.PhoneNumber = contact.PhoneNumber;
                data.Address = contact.Address;
                data.City = contact.City;
                data.State = contact.State;
                data.Country = contact.Country;
                data.PostalCode = contact.PostalCode;
                if (isNew)
                {
                    _context.Add(data);
                }
                _context.SaveChanges();
            }
           return RedirectToAction("Index");
        }
        [Route("home/delete/{id}")]
        [HttpGet]  
        public IActionResult Delete(int id)  
        {
            Contact data = _context.Set<Contact>().FirstOrDefault(c => c.Id == id);  
            _context.Entry(data).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;  
            _context.SaveChanges();  
            return RedirectToAction("Index");  
        }  

    }
}