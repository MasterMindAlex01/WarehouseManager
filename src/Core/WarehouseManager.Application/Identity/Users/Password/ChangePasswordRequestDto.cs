namespace WarehouseManager.Application.Identity.Users.Password;

public class ChangePasswordRequestDto
{
    public string Password { get; set; } = default!;
    public string NewPassword { get; set; } = default!;
    public string ConfirmNewPassword { get; set; } = default!;
}