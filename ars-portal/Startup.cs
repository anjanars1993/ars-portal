using ars_portal.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Batch;
using Microsoft.OData.Edm;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNet.OData.Builder;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.OData.Edm.Vocabularies;
using ars_portal.Models.Models;

namespace ars_portal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ars_portal", Version = "v1" });
            });
            services.AddDbContext<DbContextEmployees>(options =>
            {
                string dbConn = "data source=.;database=ars-portal;Integrated Security=SSPI;persist security info=True;";
                options.UseSqlServer(dbConn);

            }, ServiceLifetime.Transient);
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddOData();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            //    app.UseSwagger();
            //    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ars_portal v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.EnableDependencyInjection();
                routeBuilder.Select().OrderBy().Filter().Expand();
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel(), new DefaultODataBatchHandler());
            });
            app.Run(async (context) => await Task.Run(() => context.Response.Redirect("/odata/$metadata")));
        }
        /// <summary>
        /// Gets the edm model.
        /// </summary>
        /// <returns></returns>
        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();

            var types = GetTypes();

            foreach (Type item in types)
            {
                EntityTypeConfiguration entityType = builder.AddEntityType(item);
                builder.AddEntitySet(item.Name, entityType);
            }

            EdmModel model = (EdmModel)builder.GetEdmModel();

            foreach (Type item in types)
            {
                IEdmEntityType edmEntity = (IEdmEntityType)model.FindDeclaredType(item.FullName);
                DisplayAttribute itemdisplayattribute = item.GetCustomAttribute<DisplayAttribute>();
                AddAnnotations(model, (EdmElement)edmEntity, itemdisplayattribute);
                foreach (PropertyInfo propertyInfo in item.GetProperties())
                {
                    EdmElement property = (EdmElement)edmEntity.FindProperty(propertyInfo.Name);
                    DisplayAttribute displayattribute = propertyInfo.GetCustomAttribute<DisplayAttribute>();
                    if (property != null)
                        AddAnnotations(model, property, displayattribute);
                }
            }
            return model;
        }
        /// <summary>
        /// Adds the annotations.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="edmElement">The edm element.</param>
        /// <param name="displayattribute">The displayattribute.</param>
        private static void AddAnnotations(EdmModel model, EdmElement edmElement, DisplayAttribute displayattribute)
        {
            if (displayattribute != null)
            {
                var stringType = EdmCoreModel.Instance.GetString(true);
                var value = new EdmStringConstant(stringType, displayattribute.Name);
                model.SetAnnotationValue(edmElement, "", "label", value);
                stringType = EdmCoreModel.Instance.GetString(true);
                value = new EdmStringConstant(stringType, displayattribute.Description);
                model.SetAnnotationValue(edmElement, "", "quickinfo", value);
            }
        }
        /// <summary>
        /// Gets the types.
        /// </summary>
        /// <returns></returns>
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
