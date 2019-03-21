using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using YLinks.Data;

namespace YLinks.Pages.Links
{
    public class IndexModel : PageModel
    {
        private readonly YLinks.Data.ApplicationDbContext _context;

        public IndexModel(YLinks.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Link> Link { get;set; }

        public async Task OnGetAsync()
        {
            Link = await _context.Links.ToListAsync();
        }

        public string GetUrl(Link link) =>
            $"http://link.yan-soft.com/{link.Key}";
    }
}
