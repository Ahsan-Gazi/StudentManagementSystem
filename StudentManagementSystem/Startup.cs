using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentManagementSystem.InterfaceAndRepository;
using StudentManagementSystem.Models;

namespace StudentManagementSystem
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<StudentSemTeacherDepContext>(options => options.UseSqlServer(_configuration.GetConnectionString("StTeacherConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options=> {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 3;
            }).AddEntityFrameworkStores<StudentSemTeacherDepContext>();
            

            services.AddMvc(options=> {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlSerializerFormatters();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ISemesterRepository, SemesterRepository>();
            services.AddScoped<ITeacherRepository, TeacherRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        }
        
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
            
        }
    }
}
