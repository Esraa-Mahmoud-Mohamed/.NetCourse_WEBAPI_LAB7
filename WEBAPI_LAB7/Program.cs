
using Day6.MppingConfigs;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WEBAPI_LAB7.Models;
using WEBAPI_LAB7.UnitOfWorks;
using static System.Net.Mime.MediaTypeNames;

namespace WEBAPI_LAB7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string txt = "";

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Employees API",
                        Version = "v1",

                        Description = "A sample CRUD API to manage Company System",
                        TermsOfService = new Uri("http://tempuri.org/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "Esraa",

                            Email = " "
                        },
                    }
                  );
                c.EnableAnnotations();

            });



            builder.Services.AddDbContext<CompanyContext>(op => op.UseSqlServer(builder.Configuration.GetConnectionString("con")));

            //builder.Services.AddScoped<EmployeeRepository>();
            //builder.Services.AddScoped<GenericRepository<Employee>>();
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(MappingConfig));

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(txt,
                builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors(txt);

            app.MapControllers();

            app.Run();
        }
    }
}
