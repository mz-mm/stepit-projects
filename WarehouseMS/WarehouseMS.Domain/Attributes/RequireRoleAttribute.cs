using PostSharp.Aspects;
using PostSharp.Serialization;
using WarehouseMS.Domain.Enums;
using WarehouseMS.Domain.Interfaces;

namespace WarehouseMS.Domain.Attributes;

[PSerializable]
[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class RequireRoleAttribute : OnMethodBoundaryAspect
{
    private UserRole _requiredRole;
    public static IAuthService AuthService;

    public RequireRoleAttribute(UserRole requiredRole)
    {
        _requiredRole = requiredRole;
    }

    public override void OnEntry(MethodExecutionArgs args)
    {
        if (!AuthService.IsUserRole(_requiredRole))
        {
            throw new UnauthorizedAccessException("User does not have the required role.");
        }
    }
}

