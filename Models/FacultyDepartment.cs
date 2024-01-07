using System;
using System.Collections.Generic;

namespace High_School_Individual_Project.Models;

public partial class FacultyDepartment
{
    public int FkFacultyId { get; set; }

    public int FkDepartmentId { get; set; }

    public virtual Department FkDepartment { get; set; } = null!;

    public virtual Faculty FkFaculty { get; set; } = null!;
}
