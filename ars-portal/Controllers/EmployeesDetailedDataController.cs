using ars_portal.Models;
using ars_portal.Models.Models;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq;

namespace ars_portal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesDetailedDataController : ODataController
    {
        private readonly DbContextEmployees context;
        public EmployeesDetailedDataController (DbContextEmployees _context)
        {
            this.context = _context;
        }
        [EnableQuery()]
        [HttpGet]
        public IQueryable<EmployeesDetailedData> Get()
        {
            return context.EmployeesDetailedData;
        }
        [EnableQuery()]
        [Route("{id:int}")]
        [HttpGet]
        public SingleResult<EmployeesDetailedData> Get([FromODataUri] int id)
        {
            //return context.EmployeesDetailedData.FirstOrDefault(s => s.id == key);
            return SingleResult.Create(context.EmployeesDetailedData.Where(c => c.id == id));
        }
        [HttpPost]
        public void Post([FromBody] EmployeesDetailedData value)
        {
            context.EmployeesDetailedData.Add(value);
            context.SaveChanges();
        }
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] EmployeesDetailedData value)
        {
            var employee = context.EmployeesDetailedData.FirstOrDefault(s => s.id == id);
            if (employee != null)
            {
                context.Entry<EmployeesDetailedData>(employee).CurrentValues.SetValues(value);
                context.SaveChanges();
            }
        }
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            using (IDbContextTransaction transaction = context.Database.BeginTransaction())
            {
                try
                {
                    var employee = context.EmployeesDetailedData.FirstOrDefault(s => s.id == id);
                    var skill = context.Skill.Where(x => x.employeesDetailedDataId == employee.id).ToList();
                    foreach(var skillItem in skill)
                    {
                        context.Skill.Remove(skillItem);
                        context.SaveChanges();
                    }
                    if (employee != null)
                    {
                        context.EmployeesDetailedData.Remove(employee);
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            
        }
    }
}
