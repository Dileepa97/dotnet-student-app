using student_mgt_app.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace student_mgt_app.Data.DbHelpers
{
    public interface ISubjectDbHelper
    {
        Task<Subject> GetByIdAsync(Guid id);

        Task<IEnumerable<Subject>> GetAllAsync();

        Task<string> CreateAsync(Subject subject);

        Task<string> UpdateAsync(Subject subject);

        Task<string> DeleteAsync(Guid id);
    }
}
