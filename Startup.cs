using System.Text;
using Dot_Net_Core_API_with_JWT.Data;
using Dot_Net_Core_API_with_JWT.Services.CharacterService;
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
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<DataContext>(options => 
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
      services.AddControllers();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Dot_Net_Core_API_with_JWT", Version = "v1" });

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
      services.AddScoped<ICharacterService, CharacterService>();
      services.AddScoped<IAuthRepository, AuthRepository>();

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
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dot_Net_Core_API_with_JWT v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
