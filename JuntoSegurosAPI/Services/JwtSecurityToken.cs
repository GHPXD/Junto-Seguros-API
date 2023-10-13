using System.Security.Claims;

namespace JuntoSegurosAPI.Services
{
    internal class JwtSecurityToken
    {
        private string? v1;
        private string? v2;
        private List<Claim> claims;
        private DateTime expires;
        private SigningCredentials signingCredentials;

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