using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SigmaBackend.Models;

public class Grade
{
    public Guid GradeId { get; set; }

    public Guid TeacherId { get; set; }

    public Guid StudentId { get; set; }

    public Guid SubjectId { get; set; }

    public decimal Grade1 { get; set; }

    public int GradeType { get; set; }
    [JsonIgnore]
    public virtual Student Student { get; set; } = null!;
    [JsonIgnore]
    public virtual Teacher Teacher { get; set; } = null!;
    [JsonIgnore]
    public virtual Subject Subject { get; set; } = null!;
}
