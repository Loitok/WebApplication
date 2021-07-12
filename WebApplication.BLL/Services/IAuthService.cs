using System.Threading.Tasks;
using WebApplication.BLL.Authentication;
using WebApplication.BLL.ResultModel;

namespace WebApplication.BLL.Services
{
    public interface IAuthService
    {
        Task<IResult> Login(LoginModel model);
        Task<IResult> Register(RegisterModel model);
        Task<IResult> RegisterAdmin(RegisterModel model);
    }
}
