using System;
using System.Collections.Generic;

namespace e_commerce_web.core.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int AccessLevel { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<UserRole> UserRoleRoles { get; set; } = new List<UserRole>();
}
