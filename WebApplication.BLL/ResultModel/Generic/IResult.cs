namespace WebApplication.BLL.ResultModel.Generic
{
    public interface IResult<out TData> : IResult
    {
        TData Data { get; }
    }
}
