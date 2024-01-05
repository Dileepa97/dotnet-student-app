using student_mgt_app.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace student_mgt_app.Data.DbHelpers
{
    public interface IAllocatedClassRoomDbHelper
    {
        Task<string> CreateAsync(AllocatedClassRoom allocated);

        Task<IEnumerable<AllocatedClassRoom>> GetByTeacherIdAsync(Guid id);

        Task<string> DeleteAsync(Guid id);
    }
}
