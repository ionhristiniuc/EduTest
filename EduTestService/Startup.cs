using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using EduTestService.Repositories;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace EduTestService
{
    public class Startup
    {        
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            // other configurations

            ConfigureOAuth(app);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            IUserRepository userRepo = new UserRepository();

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
                Provider = new AuthorizationServerProvider(userRepo)
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

        public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
        {
            public IUserRepository UserRepo { get; set; }

            public AuthorizationServerProvider(IUserRepository userRepository)
            {
                UserRepo = userRepository;
            }

            public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
            {
                context.Validated();
            }

            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

                try
                {                    
                    var user = await UserRepo.GetUser(context.UserName, context.Password);

                    if (user == null)
                    {
                        context.Rejected();
                        return;
                    }

                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Email, user.Email));                    

                    foreach (var role in user.Roles)                    
                        identity.AddClaim(new Claim(ClaimTypes.Role, role));                    

                    var principal = new GenericPrincipal(identity, user.Roles);
                    Thread.CurrentPrincipal = principal;

                    context.Validated(identity);
                }
                catch (Exception ex)
                {
                    context.SetError("invalid_grant", ex.ToString());                    
                }                
            }
        }
    }
}