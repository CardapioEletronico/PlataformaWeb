using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CRUDRestaurante.Controllers
{
    public class RestauranteController : ApiController
    {
        public IEnumerable<Models.Restaurante> Get()
        {
            Models.CardapioDataContext dc = new Models.CardapioDataContext();
            var r1 = from r in dc.Restaurantes select r;
            return r1.ToList();
        }

        public void Delete(int idRestaurante)
        {
            Models.CardapioDataContext dc = new Models.CardapioDataContext();
            Models.Restaurante r = (from f in dc.Restaurantes where f.Id == idRestaurante select f).Single();
            dc.Restaurantes.DeleteOnSubmit(r);
            dc.SubmitChanges();
        }
        //O bom e velho update
        public void Put(int idRestaurante, [FromBody] string value)
        {
            Models.Restaurante x = JsonConvert.DeserializeObject<Models.Restaurante>(value);
            Models.CardapioDataContext dc = new Models.CardapioDataContext();
            Models.Restaurante rest = (from f in dc.Restaurantes where f.Id == idRestaurante select f).Single();
            rest.Descricao = x.Descricao;
            dc.SubmitChanges();
        }

        public void Post([FromBody] string value)
        {
            List<Models.Restaurante> r = JsonConvert.DeserializeObject<List<Models.Restaurante>>(value);
            Models.CardapioDataContext dc = new Models.CardapioDataContext();
            dc.Restaurantes.InsertAllOnSubmit(r);
            dc.SubmitChanges();
        }
    }
}
