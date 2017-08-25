using Microsoft.Practices.Unity;
using OmniBot.APIModels;
using OmniBot.APIModels.Enums;
using OmniBot.DataClasses;
using OmniBot.DataModel;
using OmniBot.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OmniBot.Services
{
    public class PeopleService : IPeopleService
    {
        [Dependency]
        public OmniBotContext Context { private get; set; }


        public Person GetPersonByUserRequest(UserRequest userRequest)
        {
            Person person = null;

            switch (userRequest.UserRequestType)
            {
                case UserRequestType.TEXT:
                    person = GetPersonByPhoneNumber(userRequest.PhoneNumber);
                    break;
                case UserRequestType.EMAIL:
                    throw new NotImplementedException("Email support not yet implemented");
                default:
                    System.Diagnostics.Trace.WriteLine("Hit Default for UserRequest type");
                    break;
            }

            // TODO consider updating Person information with other things in the request

            return person;
        }

        public Person GetPersonByPhoneNumber(string phoneNumber)
        {
            Person person = Context.Persons.Where(n => n.PhoneNumber == phoneNumber).SingleOrDefault<Person>();

            if (person == null)
            {
                person = new Person()
                {
                    PhoneNumber = phoneNumber
                };
                Context.Persons.Add(person);
            }

            return person;
        }

        public Person CreateNewPerson(Person person)
        {
            Context.Database.Log = Console.WriteLine;
            Context.Persons.Add(person);
            Context.SaveChanges();

            return person;
        }

        public List<Person> GetAllPersons()
        {
            return Context.Persons.ToList();
        }

        public Person GetPersonById(int Id)
        {
            Person person = Context.Persons.Where(n => n.Id == Id).SingleOrDefault<Person>();

            if (person == null)
            {
                return null;
            }

            return person;
        }

        public Person UpdatePerson(Person person)
        {
            // Context.Persons.Attach(person);
            Context.Entry(person).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();

            return person;
        }

        public Boolean DeletePerson(Person person)
        {
            Context.Entry(person).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();

            return person == null;
        }
    }
}
