using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Interfaces
{
    public interface IPermissionManager
    {
        Task GrantAttendancePermissionAsync(Permission permission, string userName, string attendanceId);

        Task GrantDataCollectionPermissionAsync(Permission permission, string userName, string dataCollectionId);

        Task<bool> HasPermission(Permission permission, ProjectType projectType, string projectId, string userName);
    }

    public enum Permission
    {
        Administrative,
        Collaborative
    }

    public enum ProjectType
    {
        Attendance,
        DataCollection
    }
}
