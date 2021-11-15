using System;
using System.Text;
using Dot_Net_Core_API_with_JWT.Data;
using Dot_Net_Core_API_with_JWT.Services.ClientService;
using Dot_Net_Core_API_with_JWT.Services.PhoneService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace Dot_Net_Core_API_with_JWT
{
  public class Startup
  {
    //readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSwaggerGen();

        services.AddCors(options =>
        {
          options.AddDefaultPolicy(builder =>
                            {
                              builder.AllowAnyOrigin()
                                     .AllowAnyHeader()
                                     .AllowAnyMethod();
                            });
        });

      services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

      services.AddControllers();


      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { 
            
            Version = "v1",
            Title = "Dot_Net_Core_API_with_JWT",
            Description = "API with Jwt implemented and relationships",
            Contact = new OpenApiContact
            {
                Name = "Felipe G. Belarmino",
                Email = "f.gomes.belarmino@avanade.com",
                Url = new Uri("https://www.linkedin.com/in/felipe-belarmino/"),
            }
        });

          c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
          Description = "Standard Authorization header using the Bearer Scheme. Example: \"Bearer{token}\"",
          In = ParameterLocation.Header,
          Name = "Authorization",
          Type = SecuritySchemeType.ApiKey
        });
        c.OperationFilter<SecurityRequirementsOperationFilter>();
      });

      services.AddAutoMapper(typeof(Startup));
      services.AddScoped<IClientService, clientService>();
      services.AddScoped<IAuthRepository, AuthRepository>();
      services.AddScoped<IPhoneService, PhoneService>();

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII
            .GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
          };
        });
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseCors();
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNetCoreAPI"));
      }

      app.UseHttpsRedirection();

      app.UseAuthentication();

        app.UseSwagger(c =>
        {
            c.SerializeAsV2 = true;
        });

        app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotNetCoreAPI");
        c.RoutePrefix = string.Empty;
      });

      app.UseRouting();

      app.UseCors();

      app.UseAuthorization();      

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();          
      });
    }
  }
}
