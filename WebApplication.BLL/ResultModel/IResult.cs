using System;
using System.Collections.Generic;
using System.Text;

namespace WebApplication.BLL.ResultModel
{
    public interface IResult
    {
        IReadOnlyCollection<string> Messages { get; }

        bool Success { get; }

        Exception? Exception { get; }
    }
}
