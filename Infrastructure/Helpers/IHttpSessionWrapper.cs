using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Helpers
{
    public interface IHttpSessionWrapper
    {
        void SetString(string key, string value);
        string GetString(string key);
    }
}
