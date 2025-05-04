using MasterDegree.Dto;

namespace MasterDegree.Utils
{
    public class ExecuteUtil<T>
    {
        public async static Task<Response<T>> Execute(Func<Task<T?>> func)
        {
            try
            {
                return new Response<T>()
                {
                    Success = true,
                    Data = await func(),
                };
            }
            catch (Exception ex)
            {
                return new Response<T>()
                {
                    Success = false,
                    Data = default,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
