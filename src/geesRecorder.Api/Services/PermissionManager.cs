using geesRecorder.Api.Data;
using geesRecorder.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorder.Api.Services
{
    public class PermissionManager : IPermissionManager
    {
        private readonly ApplicationDbContext _dbContext;

        public PermissionManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task GrantAttendancePermissionAsync(Permission permission, string userName, string attendanceId)
        {
            throw new NotImplementedException();
        }

        public Task GrantDataCollectionPermissionAsync(Permission permission, string userName, string dataCollectionId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPermission(Permission permission, ProjectType projectType, string projectId, string userName)
        {
            throw new NotImplementedException();
        }

        string GeneratePermissionString(string projectId, Permission permission)
            => $"{projectId}:{Enum.GetName(typeof(Permission), permission)}";
    }
}
