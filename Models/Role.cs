using System;
using System.Collections.Generic;

namespace High_School_Individual_Project.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string Role1 { get; set; } = null!;

    public virtual ICollection<Faculty> Faculties { get; set; } = new List<Faculty>();
}
