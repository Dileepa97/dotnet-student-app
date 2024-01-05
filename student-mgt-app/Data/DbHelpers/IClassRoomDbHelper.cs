using student_mgt_app.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace student_mgt_app.Data.DbHelpers
{
    public interface IClassRoomDbHelper
    {
        Task<ClassRoom> GetByIdAsync(Guid id);

        Task<IEnumerable<ClassRoom>> GetAllAsync();

        Task<string> CreateAsync(ClassRoom classRoom);

        Task<string> UpdateAsync(ClassRoom classRoom);

        Task<string> DeleteAsync(Guid id);
    }
}
