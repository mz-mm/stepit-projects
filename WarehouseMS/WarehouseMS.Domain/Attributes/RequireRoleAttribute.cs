using PostSharp.Aspects;
using PostSharp.Serialization;
using WarehouseMS.Domain.Enums;
using WarehouseMS.Domain.Interfaces;
using WarehouseMS.Domain.Services;

namespace WarehouseMS.Domain.Attributes;

[PSerializable]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class RequireRoleAttribute : OnMethodBoundaryAspect
{
    private UserRole _requiredRole;

    public RequireRoleAttribute(UserRole requiredRole)
    {
        _requiredRole = requiredRole;
    }

    public bool IsRoleValidForUser(UserRole userRole)
    {
        return userRole == _requiredRole;
    }

    public override void OnEntry(MethodExecutionArgs args)
    {
        var authService = ServiceLocator.GetService<IAuthService>();

        if (!authService.IsUserRole(_requiredRole))
        {
            throw new UnauthorizedAccessException("User does not have the required role.");
        }
    }
}

