namespace WebApplication.ActionResult
{
    using BLL.ResultModel;
    using Microsoft.AspNetCore.Mvc;

    public class ErrorableActionResult : ActionResult
    {
        public ErrorableActionResult(IResult result)
        {
            Result = result;
        }

        public IResult Result { get; }
    }
}
