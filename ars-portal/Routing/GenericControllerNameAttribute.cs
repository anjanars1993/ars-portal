using ars_portal.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;

namespace ars_portal.Routing
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class GenericControllerNameAttribute : Attribute, IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.GetGenericTypeDefinition() == typeof(GenericController<>))
            {
                var entityType = controller.ControllerType.GenericTypeArguments[0];
                controller.ControllerName = entityType.Name;
                controller.RouteValues["Controller"] = entityType.Name;
            }
        }
    }
}
