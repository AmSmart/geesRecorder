using geesRecorderApi.Data;
using geesRecorderApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace geesRecorderApi.Services
{
    public class PermissionManager : IPermissionManager
    {
        private readonly ApplicationDbContext _dbContext;

        public PermissionManager(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task GrantAttendancePermissionAsync(Permission permission, string userName, string attendanceId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            //var attendance = await _dbContext.Attendances.FirstOrDefaultAsync(x => x.Id == attendanceId);

            //string permissionString = GeneratePermissionString(attendance.Id, permission);

            //user.ProjectPermissions.Add(permissionString);

            //user.Attendances.Add(attendance);

            await _dbContext.SaveChangesAsync();
        }

        public async Task GrantDataCollectionPermissionAsync(Permission permission, string userName, string dataCollectionId)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            var dataCollection = await _dbContext.DataCollections.FirstOrDefaultAsync(x => x.Id == dataCollectionId);

            string permissionString = GeneratePermissionString(dataCollection.Id, permission);

            user.ProjectPermissions.Add(permissionString);

            user.DataCollections.Add(dataCollection);

            await _dbContext.SaveChangesAsync();
        }

        public Task<bool> HasPermission(Permission permission, ProjectType projectType, string projectId, string userName)
        {
            throw new NotImplementedException();
        }

        string GeneratePermissionString(string projectId, Permission permission)
            => $"{projectId}:{Enum.GetName(typeof(Permission), permission)}";
    }
}
