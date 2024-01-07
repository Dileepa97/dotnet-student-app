using student_mgt_app.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace student_mgt_app.Data.DbHelpers
{
    public interface IAllocatedClassRoomDbHelper
    {
        Task<string> CreateAsync(AllocatedClassRoom allocated);

        Task<IEnumerable<ClassRoom>> GetByTeacherIdAsync(Guid id);

        Task<IEnumerable<ClassRoom>> GetNotAllocatedClassRoomsByTeacherIdAsync(Guid id);

        Task<string> DeleteByTeacherIdAndClassRoomIdAsync(Guid teacherId, Guid classRoomId);
    }
}
