using Base.Api.Enums;

namespace Base.Api.Model
{
    public interface IBaseApiRequest
    {
        ApiReturnError Validate();
    }
}