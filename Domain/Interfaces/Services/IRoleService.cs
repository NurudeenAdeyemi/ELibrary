using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IRoleService
    {
        public Task<RoleResponseModel> GetRole(int id);

        public Task<RolesResponseModel> GetRoles();

        public Task<BaseResponse> UpdateRole(int id, UpdateRoleRequestModel model);

        public Task<BaseResponse> AddRole(CreateRoleRequestModel model);

        public Task<BaseResponse> DeleteRole(int id);
    }
}
