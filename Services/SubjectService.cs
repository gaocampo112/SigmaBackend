using Microsoft.EntityFrameworkCore;
using SigmaBackend.Models;


namespace SigmaBackend.Services
{
    public class SubjectService : ISubjectService
    {
        SigmaContext _sigmaContext;
        public SubjectService(SigmaContext sigmaContext)
        {
            _sigmaContext = sigmaContext;
        }

        public async Task<Subject> Get(Guid id)
        {
            return await _sigmaContext.Subjects.FindAsync(id);
        }

        public async Task<IEnumerable<Subject>> GetAll()
        {
            return await _sigmaContext.Subjects.ToListAsync();
        }

        public async Task Save(Subject subject)
        {
            _sigmaContext.Subjects.Add(subject);
            await _sigmaContext.SaveChangesAsync();
        }

        public async Task<bool> Update(Guid id, Subject subject)
        {
            var subjectToEdit = await _sigmaContext.Subjects.FindAsync(id);

            if (subjectToEdit != null)
            {
                subjectToEdit.SubjectName = subject.SubjectName;
                await _sigmaContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> Delete(Guid id)
        {
            var subjectToDelete = await _sigmaContext.Subjects.FindAsync(id);

            if (subjectToDelete != null)
            {
                _sigmaContext.Remove(subjectToDelete);
                await _sigmaContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

    public interface ISubjectService
    {
        Task<Subject> Get(Guid id);
        Task<IEnumerable<Subject>> GetAll();
        Task Save(Subject subject);
        Task<bool> Update(Guid id, Subject subject);
        Task<bool> Delete(Guid id);
    }
}
