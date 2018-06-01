namespace RealEstate.WebAPI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using RealEstate.DAL.Domain;

    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly RealEstateContext _context;

        public ItemController(RealEstateContext context)
        {
            _context = context;
        }

        [HttpGet("list")]
        public async Task<List<Item>> FetchItems()
        {
            return await _context.Items.ToListAsync();
        }
    }
}