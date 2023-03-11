using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BooksMgmt.Data;
using BooksMgmt.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BooksMgmt.API;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly BooksDbContext _data;
    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, BooksDbContext dbContext, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
        _data = dbContext;
    }

    protected async override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        User? user;

        if (!Request.Headers.ContainsKey("Authorization"))
            return AuthenticateResult.Fail("Missing Authorization Header");

        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(new[] { ':' }, 2);
            var username = credentials[0];
            var password = credentials[1];
            user = GetUser(username, password);
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }

        if (user == null)
            return AuthenticateResult.Fail("Invalid Username or Password");

        Claim[] claims = {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.Name),
                new (ClaimTypes.Role, user.Role.Name),
                new (ClaimTypes.Email, user.Email),
            };
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var id2 = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(new ClaimsIdentity[] { identity, id2 });

        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }

    private User? GetUser(string email, string password)
    {
        return _data.Users.Include(x => x.Role).FirstOrDefault(u => u.Email == email && u.Password == password);
    }
}
