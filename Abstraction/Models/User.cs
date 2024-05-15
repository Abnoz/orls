using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis.Operations;

namespace Abstraction.Models;
[GenerateSerializer]
[Alias("UserBomBom")]
public class User
{
    [Id(0)]
    public Guid Id { get; set; }
    [Id(1)]
    public string FirstName { get; set; } = string.Empty;
    [Id(2)]
    public string LastName { get; set; } = string.Empty;
    [Id(3)]
    public string Email { get; set; } = string.Empty;
    [Id(4)]
    public string Address { get; set; } = string.Empty;
    [Id(5)]
    public int PhoneNumber { get; set; }
    [Id(6)]
    public string Country { get; set; } = string.Empty;
    [Id(7)]
    public string City { get; set; } = string.Empty;
}