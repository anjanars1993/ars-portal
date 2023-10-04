using ars_portal.Models.Models;
using ars_portal.Models.Models.Base;
using ars_portal.Repository;
using ars_portal.Routing;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ars_portal.Controllers
{
    [Produces("application/json")]
    [GenericControllerName]
    [EnableQuery(MaxNodeCount = 10000, AllowedQueryOptions = AllowedQueryOptions.All, MaxExpansionDepth = 4)]
    public class GenericController<T> : ODataController where T : BaseModel
    {
        protected IRepository<T> Repository { get; set; }
        public GenericController(IRepository<T> Repo)
        {
            Repository = Repo;
        }
        [HttpGet]
        public async Task<IQueryable<T>> Get()
        {
            var res = await Repository.GetItemsAsync(c => c.IsActive == true);
            return res;
        }


        [Route("{id:int}")]
        [HttpGet]
        public async Task<T> Get([FromODataUri] int key)
        {
            var result = await Repository.GetItemAsync(key);

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] T item, [FromODataUri] bool IsList = false)
        {
            try
            {
                var response = await Repository.CreateItemAsync(item);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //put and delete for EmployeesDetailedData with skills will not work for skills
        [HttpPut]
        public async Task<IActionResult> Put([FromODataUri] Guid key, [FromBody] T item)
        {
            try
            {
                return Ok(await Repository.UpdateItemAsync(item.Id.ToString(), item));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public virtual async Task<IActionResult> Delete([FromODataUri] int key, [FromODataUri] bool DeleteRecord = false)
        {
            try
            {
                return Ok(await Repository.DeleteItemAsync(key));
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
