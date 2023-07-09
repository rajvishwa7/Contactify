using Contactify.Data;
using Contactify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contactify.Controllers
{
    [ApiController]     // telling that this is an API Controller & not an MVC Controller
    [Route("api/[controller]")] // can give like [Route("api/contacts")] both acts same only
    public class ContactsController : Controller
    {
        private readonly ContactifyDbContext dbContext;

        public ContactsController(ContactifyDbContext dbContext)    // dbContext => ctrl + dot to create above private property & use the same name
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());     // instead of giving into IEnumerable we can wrap it in Ok() response
        }

        [HttpGet]
        [Route("{id:guid}")]    // need the keyword id to Get the contact
        public async Task<IActionResult> GetContact([FromRoute] Guid id)   // to get single contact (so GetContact without char S)
        {
            var contact = await dbContext.Contacts.FindAsync(id);   // to find the id from Database

            if (contact == null)
            {
                return NotFound("No Records");
            }
            return Ok(contact);
        }

        [HttpPost]  // when we Add or Insert a new contact or resource we use HttpPost verb
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)   // Task => as we use async method
        {
            var contact = new Contact()     // we are doing mapping between AddContactRequest & Contact
            {
                Id = Guid.NewGuid(),
                Address = addContactRequest.Address,
                Email = addContactRequest.Email,
                FullName = addContactRequest.FullName,
                Phone = addContactRequest.Phone,
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);     // means a Success response
        }

        [HttpPut]   // when we Update or Edit a contact or resource we use HttpPut verb
        [Route("{id:guid}")]    // need the keyword id to Update the contact
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = await dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {
                contact.FullName = updateContactRequest.FullName;
                contact.Address = updateContactRequest.Address;
                contact.Phone = updateContactRequest.Phone;
                contact.Email = updateContactRequest.Email;

                await dbContext.SaveChangesAsync();

                return Ok(contact);

            }
            return NotFound();
        }

        [HttpDelete]    // when we Delete a contact or resource we use HttpDelete verb
        [Route("{id:guid}")]    // need the keyword id to Delete the contact
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = await dbContext.Contacts.FindAsync(id); // we will talk to our contact table & try to find a resource with primary key or id

            if (contact != null)
            {
                dbContext.Remove(contact);
                await dbContext.SaveChangesAsync();
                return Ok(contact); // you can pass any msg in Ok("Contact Deleted")
            }
            return NotFound();
        }

    }
}
