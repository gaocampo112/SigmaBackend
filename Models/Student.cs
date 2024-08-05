using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SigmaBackend.Models;

public class Student
{
    public Guid StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public string StudentLastName { get; set; } = null!;

    public int StudentAge { get; set; }
    public int UserName { get; set; }
    public int Password { get; set; }

    [JsonIgnore]
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
