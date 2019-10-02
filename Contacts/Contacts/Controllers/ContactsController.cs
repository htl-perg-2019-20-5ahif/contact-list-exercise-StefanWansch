using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contacts_Homework.Controllers
{
    [Route("api/contacts")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        public static List<Person> contacts = new List<Person>();
        // GET: api/contacts
        [HttpGet]
        public IActionResult GetContacts()
        {
            Person p = new Person();
            p.email = "stefan-wansch@gmx.at";
            p.id = 1;
            p.lastName = "Wansch";
            p.firstName = "Stefan";
            contacts.Add(p);
            return Ok(contacts);
        }


        // POST: api/contacts
        [HttpPost]
        public IActionResult NewContact([FromBody] Person p)
        {
            contacts.Add(p);
            return CreatedAtRoute(
                "GetSpecificItem", new { index = contacts.IndexOf(p) }, p);
        }


        // DELETE: api/contacts/[id]
        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            foreach (Person contact in contacts) // Loop through List with foreach
            {
                if(contact.id == id)
                {
                    contacts.Remove(contact);
                    return Ok("Success");
                }
                
                
            }

            return BadRequest("Invalid index");

        }

        [HttpGet]
        [Route("findByName")]
        public IActionResult GetContactByName([FromQuery]string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("Parameter was empty");
            }
            else
            {
                List<Person> contacts_help = new List<Person>();
                foreach(Person contact in contacts)
                {
                    if(contact.firstName.Equals(name) || contact.lastName.Equals(name))
                    {
                        contacts_help.Add(contact);
                    }
                }
                return Ok(contacts_help);
            }
        }
    }
}
