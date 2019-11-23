using System;
using System.Threading.Tasks;

namespace ZzAppDev
{
    public interface LocationDelegate
    {
        Task GetGPSLocationAsync();
    }
}
