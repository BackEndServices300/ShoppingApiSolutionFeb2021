using System;

namespace ShoppingApi.Services
{
    public interface ISystemTime
    {
        DateTime GetCurrent();
    }
}
