using SigmaBackend.Models;

namespace SigmaBackend.Services
{
    public class GradeService : IGradeService
    {
        SigmaContext _sigmaContext;

        public GradeService(SigmaContext sigmaContext)
        {
            _sigmaContext = sigmaContext;
        }

        public async Task<Grade> Get(Guid id)
        {
            return await _sigmaContext.Grades.FindAsync(id);
        }

        public async Task Save(Grade grade)
        {
            _sigmaContext.Grades.Add(grade);
            await _sigmaContext.SaveChangesAsync();
        }

        public async Task<bool> Update(Guid id, Grade Grade)
        {
            var GradeToEdit = await _sigmaContext.Grades.FindAsync(id);

            if (GradeToEdit != null)
            {
                GradeToEdit.Grade1 = Grade.Grade1;
                GradeToEdit.GradeType = Grade.GradeType;
                await _sigmaContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(Guid id)
        {
            var gradeToDelete = await _sigmaContext.Grades.FindAsync(id);

            if (gradeToDelete != null)
            {
                _sigmaContext.Remove(gradeToDelete);
                await _sigmaContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

    public interface IGradeService
    {
        Task<Grade> Get(Guid id);
        Task Save(Grade grade);
        Task<bool> Update(Guid id, Grade grade);
        Task<bool> Delete(Guid id);
    }
}
