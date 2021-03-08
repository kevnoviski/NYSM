using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NYSM.Data;
using NYSM.Models;

namespace NYSM.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly NYSMContext _context;
        
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            NYSMContext context) : base(options, logger,encoder,clock)
        {
            _context = context;
        }
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if(!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Missing Authorization.");    
            }

            var authentication= AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var bytes = Convert.FromBase64String(authentication.Parameter);
            string parm = Encoding.UTF8.GetString(bytes);
            string[] array = parm.Split(":");

            User user = _context.users.Where(x => x.Email == array[0] ).FirstOrDefault();
            
            PasswordHasher<User> passwordHasher= new PasswordHasher<User>();
            
            
            if(user != null && passwordHasher.VerifyHashedPassword(user,user.Password,array[1]).Equals(PasswordVerificationResult.Success) )
            {
                var claims = new[] { new Claim(ClaimTypes.Name,user.Name)};
                var identity = new ClaimsIdentity(claims,Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal,Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            return AuthenticateResult.Fail("Failed to Authenticate.");    
        }
    }
}