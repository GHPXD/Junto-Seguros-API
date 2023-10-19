using System.Security.Claims;

namespace JuntoSegurosAPI.Services
{
    internal class JwtSecurityToken
    {
        public string? v1;
        public string? v2;
        public List<Claim> claims;
        public DateTime expires;
        public SigningCredentials signingCredentials;

        public JwtSecurityToken(string? v1, string? v2, List<Claim> claims, DateTime expires, SigningCredentials signingCredentials)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.claims = claims;
            this.expires = expires;
            this.signingCredentials = signingCredentials;
        }
    }
}