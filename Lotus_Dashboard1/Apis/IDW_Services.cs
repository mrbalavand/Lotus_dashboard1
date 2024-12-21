namespace Lotus_Dashboard1.Apis
{
    public interface IDW_Services
    {
        Task<string> get_token();

        Task<string> get_data(string nationalcode, string dscode, string token1);
    }
}
