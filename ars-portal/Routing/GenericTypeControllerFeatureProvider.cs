using ars_portal.Controllers;
using ars_portal.Models.Models;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ars_portal.Routing
{
    public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (Type item in GetTypes())
            {
                var entityType = item;
                var typeName = entityType.Name + "Controller";
                if (!feature.Controllers.Any(t => t.Name == typeName))
                {
                    var controllerType = typeof(GenericController<>)
                        .MakeGenericType(entityType)
                        .GetTypeInfo();

                    feature.Controllers.Add(controllerType);
                }
            }
        }
        public static List<Type> GetTypes()
        {
            return new List<Type>()
            {
                typeof(EmployeesDetailedData),
                typeof(EmployeesPrimaryData),
                typeof(Skill)
            };
        }
    }
}
