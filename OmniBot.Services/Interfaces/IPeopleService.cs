using OmniBot.APIModels;
using OmniBot.DataClasses;
using System;
using System.Collections.Generic;

namespace OmniBot.Services.Interfaces
{
    public interface IPeopleService
    {
        Person GetPersonByPhoneNumber(string phoneNumber);
        Person GetPersonByUserRequest(UserRequest userRequest);
        Person CreateNewPerson(Person person);
        List<Person> GetAllPersons();
        Person GetPersonById(int Id);
        Person UpdatePerson(Person person);
        Boolean DeletePerson(Person person);
    }
}
