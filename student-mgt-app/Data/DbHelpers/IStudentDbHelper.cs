using student_mgt_app.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace student_mgt_app.Data.DbHelpers
{
    public interface IStudentDbHelper
    {
        Task<Student> GetByIdAsync(Guid id);

        Task<IEnumerable<Student>> GetAllAsync();

        Task<string> CreateAsync(Student student);

        Task<string> UpdateAsync(Student student);

        Task<string> DeleteAsync(Guid id);

        Task<IEnumerable<Object>> GetStudentReportDataAsync(Guid id);
    }
}
