//using ars_portal.Models;
//using ars_portal.Models.Models;
//using Microsoft.AspNet.OData;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Linq;

//namespace ars_portal.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EmployeesPrimaryDataController : ODataController
//    {
//        private readonly DbContextEmployees context;
//        public EmployeesPrimaryDataController(DbContextEmployees _context)
//        {
//            this.context = _context;
//        }
//        [EnableQuery()]
//        [HttpGet]
//        public IEnumerable<EmployeesPrimaryData> Get()
//        {
//            return context.EmployeesPrimaryData;
//        }
//        [EnableQuery()]
//        [HttpGet("{id}")]
//        public EmployeesPrimaryData Get(int id)
//        {
//            return context.EmployeesPrimaryData.FirstOrDefault(s => s.Id == id);
//        }
//        [EnableQuery()]
//        [HttpPost]
//        public void Post([FromBody] EmployeesPrimaryData value)
//        {
//            context.EmployeesPrimaryData.Add(value);
//            context.SaveChanges();
//        }
//        [EnableQuery()]
//        [HttpPut("{id}")]
//        public void Put(int id, [FromBody] EmployeesPrimaryData value)
//        {
//            var employee = context.EmployeesPrimaryData.FirstOrDefault(s => s.Id == id);
//            if (employee != null)
//            {
//                context.Entry<EmployeesPrimaryData>(employee).CurrentValues.SetValues(value);
//                context.SaveChanges();
//            }
//        }
//        [EnableQuery()]
//        [HttpDelete("{id}")]
//        public void Delete(int id)
//        {
//            var employee = context.EmployeesPrimaryData.FirstOrDefault(s => s.Id == id);
//            if (employee != null)
//            {
//                context.EmployeesPrimaryData.Remove(employee);
//                context.SaveChanges();
//            }
//        }
//    }
//}
