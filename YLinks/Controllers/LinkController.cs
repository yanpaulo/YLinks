using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YLinks.Data;

namespace YLinks.Controllers
{
    public class LinkController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LinkController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("/")]
        public IActionResult Index() => RedirectToPage("/Site/Index");

        [Route("/{key}")]
        public IActionResult Index(string key)
        {
            var link = _context.Links.SingleOrDefault(l => l.Key == key);
            return link != null ?
                (IActionResult)Redirect(link.Url)
                : NotFound();
        }
    }
}