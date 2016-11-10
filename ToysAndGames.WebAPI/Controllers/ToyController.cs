using DataRepository;
using DataRepository.Entities;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ToysAndGames.WebAPI.Controllers
{
    public class ToyController : ApiController
    {
        private IRepository<Toy> toyRepository;

        public ToyController(IRepository<Toy> repo) // I would like to inject it with Ninject, however the lack of time doesn't allow it at this time :(
        {
            toyRepository =  repo;
        }
        public ToyController()
        {
            toyRepository = new MyRepository();
        }
        // GET: api/Toy
        public IEnumerable<Toy> Get()
        {
            return toyRepository.List();
        }

        //// GET: api/Toy/5
       public Toy Get(int id)
        {
            return toyRepository.FindById(id);
        }

       
        // POST: api/Toy
        public HttpResponseMessage Post(Toy toyToAdd)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            toyRepository.Add(toyToAdd);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT: api/Toy/5
        public HttpResponseMessage Put(Toy toyToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            toyRepository.Update(toyToUpdate);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // DELETE: api/Toy/5
        public HttpResponseMessage Delete(int id)
        {
            var objectToRemove=toyRepository.FindById(id);
            if (objectToRemove == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            toyRepository.Delete(objectToRemove);
            return Request.CreateResponse(HttpStatusCode.OK,objectToRemove);
        }
    }
}
