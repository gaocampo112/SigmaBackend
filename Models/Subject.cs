using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SigmaBackend.Models;

public partial class Subject
{
    public Guid SubjectId { get; set; }

    public string SubjectName { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();
}
