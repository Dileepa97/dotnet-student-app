using student_mgt_app.Models.Domain;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace student_mgt_app.Data.DbHelpers
{
    public interface ITeacherDbHelper
    {
        Task<Teacher> GetByIdAsync(Guid id);

        Task<IEnumerable<Teacher>> GetAllAsync();

        Task<string> CreateAsync(Teacher teacher);

        Task<string> UpdateAsync(Teacher teacher);

        Task<string> DeleteAsync(Guid id);
    }
}
