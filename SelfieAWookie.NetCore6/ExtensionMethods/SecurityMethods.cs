using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace SelfieAWookie.NetCore6.ExtensionMethods
{
    public static class SecurityMethods
    {
        public const string DEFAULT_POLICY = "DEFAULT_POLICY";

        public const string DEFAULT_POLICY2 = "DEFAULT_POLICY2";

        public static void AddCustomSecurity(this IServiceCollection services, IConfiguration configuration)
        {
           services.AddCustomCors(configuration);
            services.AddCustomAuthentification(configuration);
        }

        public static void AddCustomAuthentification(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                string myKey = configuration ["Jwt:Key"];
                options.SaveToken = true;
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(myKey)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateActor = false,
                    ValidateLifetime = true,
                };
            });
        }

        public static void AddCustomCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(DEFAULT_POLICY, builder =>
                {
                    builder.WithOrigins(configuration["Cors:Origin"])
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });

                options.AddPolicy(DEFAULT_POLICY2, builder =>
                {
                    builder.WithOrigins(configuration["Cors:Display"])
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });

            });
        }
    }
}
