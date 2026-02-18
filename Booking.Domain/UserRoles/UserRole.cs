using Booking.Domain.Roles;
using Booking.Domain.Users;
using System.ComponentModel.DataAnnotations;

namespace Booking.Domain.UserRoles;

public class UserRole
{
    [Key]
    public Guid UserId { get; private set; }

    [Key] 
    public Guid RoleId { get; private set; }

    public DateTime AssignedAt { get; private set; }

    public User User { get; private set; }

    public Role Role { get; private set; }

    public UserRole() { }

    public UserRole(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
        AssignedAt = DateTime.UtcNow;
    }
}