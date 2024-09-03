using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Core.Interfaces
{
    public interface ICacheService
    {
        Task<T?> GetData<T>(string key);
        Task SetData<T>(string key, T data,TimeSpan ? absoluteExpiration = null, TimeSpan? slidingExpiration = null);
        Task Remove(string key);
    }
}
