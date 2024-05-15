namespace OrleansDemo.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int PhoneNumber { get; set; }
    public string Country { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
}