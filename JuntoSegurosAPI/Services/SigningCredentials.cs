namespace JuntoSegurosAPI.Services
{
    internal class SigningCredentials
    {
        public SymmetricSecurityKey key;
        public object hmacSha256;

        public SigningCredentials(SymmetricSecurityKey key, object hmacSha256)
        {
            this.key = key;
            this.hmacSha256 = hmacSha256;
        }
    }
}