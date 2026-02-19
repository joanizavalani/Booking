using Booking.Domain.UserRoles;
using System.ComponentModel.DataAnnotations;

namespace Booking.Domain.Roles;

public class Role 
{
    [Key]
    public Guid Id { get; private set; }

    public string Name { get; private set; }
        
    public string Description { get; private set; }

    public bool IsDefault { get; private set; }

    public List<UserRole> UserRoles { get; private set; }

    public Role() { }

    public Role(
        Guid id,
        string name,
        string description,
        bool isDefault)
    {
        Id = id;
        Name = name;
        Description = description;
        IsDefault = isDefault;

        UserRoles = new List<UserRole>();
    }
}