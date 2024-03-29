using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using student_mgt_app.Data;
using student_mgt_app.Data.DbHelpers;
using student_mgt_app.Middleware;
using student_mgt_app.Utility;
using System.IO;

namespace student_mgt_app
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
            services.AddControllers();

            // Swagger config
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Student App API", Version = "v1" });
            });

            // Configuration settings
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            //Migration runner
            MigrationRunner migrationRunner = new MigrationRunner(configuration);
            migrationRunner.RunMigrations();

            // Cors
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            // Automapper
            services.AddAutoMapper(typeof(AutoMapperProfiles));

            // services
            services.AddScoped<ITeacherDbHelper, TeacherDbHelper>();
            services.AddScoped<IClassRoomDbHelper, ClassRoomDbHelper>();
            services.AddScoped<ISubjectDbHelper, SubjectDbHelper>();
            services.AddScoped<IStudentDbHelper, StudentDbHelper>();
            services.AddScoped<IAllocatedSubjectDbHelper, AllocatedSubjectDbHelper>();
            services.AddScoped<IAllocatedClassRoomDbHelper, AllocatedClassRoomDbHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student App API V1");
                });
            }

            app.UseCors("AllowAll");

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
