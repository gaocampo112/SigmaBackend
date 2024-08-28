using Microsoft.EntityFrameworkCore;
using SigmaBackend.Models;

namespace SigmaBackend.Services
{
    public class TeacherService : ITeacherService
    {
        SigmaContext _sigmaContext;
        public TeacherService(SigmaContext sigmaContext)
        {
            _sigmaContext = sigmaContext;
        }

        public async Task<Teacher> Get(Guid id)
        {
            return await _sigmaContext.Teachers.FindAsync(id);
        }
        public async Task<IEnumerable<Teacher>> GetAll()
        {
            return await _sigmaContext.Teachers.ToListAsync();
        }

        public async Task Save(Teacher teacher)
        {
            _sigmaContext.Teachers.Add(teacher);
            await _sigmaContext.SaveChangesAsync();
        }

        public async Task<bool> Update(Guid id, Teacher teacher)
        {
            var teacherToEdit = await _sigmaContext.Teachers.FindAsync(id);

            if (teacherToEdit != null)
            {
                teacherToEdit.TeacherName = teacher.TeacherName;
                teacherToEdit.TeacherLastName = teacher.TeacherLastName;
                teacherToEdit.TeacherAge = teacher.TeacherAge;
                teacherToEdit.TeacherSubject = teacher.TeacherSubject;
                teacherToEdit.UserName = teacher.UserName;
                teacherToEdit.Password = teacher.Password;
                await _sigmaContext.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task<bool> Delete(Guid id)
        {
            var teacherToDelete = await _sigmaContext.Teachers.FindAsync(id);

            if (teacherToDelete != null)
            {
                _sigmaContext.Remove(teacherToDelete);
                await _sigmaContext.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

    public interface ITeacherService
    {
        Task<Teacher> Get(Guid id);
        Task<IEnumerable<Teacher>> GetAll();
        Task Save(Teacher teacher);
        Task<bool> Update(Guid id, Teacher teacher);
        Task<bool> Delete(Guid id);
    }
}
