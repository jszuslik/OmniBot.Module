using Microsoft.Practices.Unity;
using OmniBot.APIModels;
using OmniBot.DataClasses;
using OmniBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace OmniBot.API.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class PersonController : ApiController
    {
        [Dependency]
        public IPeopleService PeopleService { private get; set; }

        [HttpPost]
        [Route("api/createPerson")]
        public Person CreateNewPerson([FromBody] PersonRequest pr)
        {
            System.Diagnostics.Debug.WriteLine(pr.FirstName + " " + pr.LastName);
            
            var person = new Person()
            {
                Id = pr.Id,
                FirstName = pr.FirstName,
                LastName = pr.LastName,
                PhoneNumber = pr.PhoneNumber,
                Email = pr.Email,
                IsActive = true
            };
            
            person = PeopleService.CreateNewPerson(person);

            return person;
           
        }

        [HttpGet]
        [Route("api/persons")]
        public List<Person> GetAllPersons()
        {
            return PeopleService.GetAllPersons();
        }

        [HttpGet]
        [Route("api/persons/{id}")]
        public Person GetPerson([FromUri] int id)
        {
            return PeopleService.GetPersonById(id);
        }

        [HttpPost]
        [Route("api/updatePerson")]
        public Person UpdatePerson(Person person)
        {
            return PeopleService.UpdatePerson(person);
        }

        [HttpPost]
        [Route("api/deletePerson")]
        public Boolean DeletePerson(Person person)
        {
            Person per = person;
            // PeopleService.DeletePerson(person);


            return PeopleService.DeletePerson(person);
        }
    }
}