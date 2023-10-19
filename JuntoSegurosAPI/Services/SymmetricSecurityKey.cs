namespace JuntoSegurosAPI.Services
{
    internal class SymmetricSecurityKey
    {
        public byte[] bytes;

        public SymmetricSecurityKey(byte[] bytes)
        {
            this.bytes = bytes;
        }
    }
}