using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SigmaBackend.Models;

public partial class Teacher
{
    public Guid TeacherId { get; set; }

    public string TeacherName { get; set; } = null!;

    public string TeacherLastName { get; set; } = null!;

    public int TeacherAge { get; set; }

    public string? TeacherSubject { get; set; }
    public int UserName { get; set; }
    public int Password { get; set; }

    [JsonIgnore]
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
