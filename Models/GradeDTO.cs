namespace SigmaBackend.Models
{
    public class GradeDTO
    {
        public Guid GradeId { get; set; }
        public Guid TeacherId { get; set; }
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public decimal Grade1 { get; set; }
        public int GradeType { get; set; }

    }
}
