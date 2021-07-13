using Microsoft.AspNetCore.Mvc;
using WebApplication.ActionResult;
using WebApplication.BLL.ResultModel;

namespace WebApplication.Extensions
{
    public static class ActionResultExtensions
    {
        public static IActionResult ToActionResult(this IResult result)
            => new ErrorableActionResult(result);
    }
}
