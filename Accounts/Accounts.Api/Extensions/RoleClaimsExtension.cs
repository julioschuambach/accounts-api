using Accounts.Domain.Entities;
using System.Security.Claims;

namespace Accounts.Api.Extensions;

public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this Account account)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, account.Username)
        };

        claims.AddRange(account.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        return claims;
    }
}
