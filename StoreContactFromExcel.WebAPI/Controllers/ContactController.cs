using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoreContactFromExcel.Service.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StoreContactFromExcel.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("store")]
        public int AddContactFromExcel()
        {
            string filePath = Directory.GetCurrentDirectory() + "\\Resources\\Contact.xlsx";
            return _contactService.AddContactFromExcel(filePath);
        }    
    }
}
