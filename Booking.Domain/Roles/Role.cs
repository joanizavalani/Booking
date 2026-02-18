using Booking.Domain.UserRoles;
using System.ComponentModel.DataAnnotations;

namespace Booking.Domain.Roles;

    public class Role 
    {
        [Key]
        public Guid Id { get; private set; }

        public string Name { get; private set; }
        
        public string Description { get; private set; }

        public List<UserRole> UserRoles { get; private set; }

        public Role() { }

        public Role(string name, string description)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            UserRoles = new List<UserRole>();
        }
    }