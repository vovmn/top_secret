using Microsoft.AspNetCore.Mvc;

namespace COM.API.Controllers
{
    public class BaseController : ControllerBase
    {
        protected Guid GetUserIdFromClaims()
        {
            var userIdStr = User.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userIdStr) || !Guid.TryParse(userIdStr, out var userId))
            {
                throw new InvalidOperationException("Невозможно определить идентификатор пользователя из токена.");
            }
            return userId;
        }

        protected string GetUserRoleFromClaims()
        {
            return User.FindFirst("role")?.Value ?? "anonymous";
        }
    }
}
