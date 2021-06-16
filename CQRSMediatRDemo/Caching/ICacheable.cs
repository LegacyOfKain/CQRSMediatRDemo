using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSMediatRDemo.Caching
{
    public interface ICacheable
    {
        string CacheKey { get; }
    }
}
