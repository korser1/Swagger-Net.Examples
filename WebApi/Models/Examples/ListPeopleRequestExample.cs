﻿using Swagger.Net.Examples;

namespace WebApi.Models.Examples
{
    internal class ListPeopleRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new PeopleRequest { Title = Title.Mr, Age = 24, FirstName = "Dave in a list", Income = null };
        }
    }
}