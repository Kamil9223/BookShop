namespace OrderService.Helpers
{
    public interface IHttpSessionWrapper
    {
        void SetString(string key, string value);
        string GetString(string key);
    }
}
