using System;
using System.Collections.Generic;

namespace High_School_Individual_Project.Models;

public partial class Faculty
{
    public int FacultyId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public DateTime DateOfEmployment { get; set; }

    public int? Salary { get; set; }

    public int? FkDepartmentId { get; set; }

    public int FkRoleId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Department? FkDepartment { get; set; }

    public virtual Role FkRole { get; set; } = null!;
}
