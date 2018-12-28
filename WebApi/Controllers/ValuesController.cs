﻿using Swagger.Net.Examples;
using Swagger.Net.Annotations;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using Newtonsoft.Json.Converters;
using WebApi.Models;
using WebApi.Models.Examples;

namespace WebApi.Controllers
{
    [Authorize]
    [SwaggerResponse(HttpStatusCode.NotFound, "Could not find the person", typeof(ErrorResponse))]
    [SwaggerResponseExample(HttpStatusCode.NotFound, typeof(NotFoundResponseExample))]
    public class ValuesController : ApiController
    {
        /// <summary>
        /// Gets a person
        /// </summary>
        /// <param name="personRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/values/person")]

        [SwaggerResponse(HttpStatusCode.OK, "Successfully found the person", typeof(PersonResponse))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(PersonResponseExample), jsonConverter: typeof(StringEnumConverter))]
        // [SwaggerResponseExample(HttpStatusCode.OK, typeof(PersonResponseExample), typeof(DefaultContractResolver))]

        //[SwaggerResponse(HttpStatusCode.NotFound, "Could not find the person", typeof(ErrorResponse))]
        //[SwaggerResponseExample(HttpStatusCode.NotFound, typeof(NotFoundResponseExample))]

        [SwaggerResponse(HttpStatusCode.InternalServerError, "There was an unexpected error")]
        [SwaggerResponseExample(HttpStatusCode.InternalServerError, typeof(InternalServerResponseExample))]

        [SwaggerRequestExample(typeof(PersonRequest), typeof(PersonRequestExample), jsonConverter: typeof(StringEnumConverter))]

        [SwaggerResponseHeader(HttpStatusCode.OK, "Location", "string", "Location of the newly created resource")]
        [SwaggerResponseHeader(HttpStatusCode.OK, "ETag", "string", "An ETag of the resource")]
        [Authorize(Roles = "Administrator", Users = "Matt")]
        public IHttpActionResult GetPerson(PersonRequest personRequest)
        {
            var personResponse = new PersonResponse { Id = 1, Title = Title.Mr, FirstName = "Dave", Age = 32 };
            return Ok(personResponse);
        }

        [HttpPost]
        [Route("api/values/genericperson")]
        [SwaggerResponse(HttpStatusCode.OK, "Successfully found the person", typeof(ResponseWrapper<PersonResponse>))]
        [SwaggerResponseExample(HttpStatusCode.OK, typeof(WrappedPersonResponseExample), jsonConverter: typeof(StringEnumConverter))]
        [SwaggerRequestExample(typeof(RequestWrapper<PersonRequest>), typeof(WrappedPersonRequestExample), jsonConverter: typeof(StringEnumConverter))]
        [AllowAnonymous]
        public IHttpActionResult GetGenericPerson(RequestWrapper<PersonRequest> personRequest)
        {
            var personResponse = new ResponseWrapper<PersonResponse>
            {
                StatusCode = HttpStatusCode.OK,
                Body = new PersonResponse { Id = 1, FirstName = "Dave" }
            };

            return Ok(personResponse);
        }

        [HttpPost]
        [Route("api/values/listperson")]
        [SwaggerResponse(HttpStatusCode.OK, "Successfully found the people", typeof(List<PersonResponse>))]
        [SwaggerRequestExample(typeof(PeopleRequest), typeof(ListPeopleRequestExample), jsonConverter: typeof(StringEnumConverter))]
        public IHttpActionResult GetPersonList(List<PeopleRequest> peopleRequest)
        {
            var people = new[] { new PersonResponse { Id = 1, FirstName = "Sally" } };
            return Ok(people);
        }
    }
}
