using student_mgt_app.Models.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace student_mgt_app.Data.DbHelpers
{
    public interface IAllocatedSubjectDbHelper
    {
        Task<string> CreateAsync(AllocatedSubject allocated);

        Task<IEnumerable<Subject>> GetByTeacherIdAsync(Guid id);

        Task<string> DeleteByTeacherIdAndSubjectIdAsync(Guid teacherId, Guid subjectId);

        Task<IEnumerable<Subject>> GetNotAllocatedSubjectsByTeacherIdAsync(Guid id);
    }
}
