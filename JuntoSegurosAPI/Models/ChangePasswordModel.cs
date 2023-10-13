namespace JuntoSegurosAPI.Models
{
    public class ChangePasswordModel
    {
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
        public required string ConfirmNewPassword { get; set; }
    }

}
