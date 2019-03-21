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
    public class DetailsModel : PageModel
    {
        private readonly YLinks.Data.ApplicationDbContext _context;

        public DetailsModel(YLinks.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Link Link { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Link = await _context.Links.FirstOrDefaultAsync(m => m.Id == id);

            if (Link == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
