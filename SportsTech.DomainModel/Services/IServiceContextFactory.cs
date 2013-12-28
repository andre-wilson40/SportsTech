using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services
{
    public interface IServiceContextFactory
    {
        TService Resolve<TService>() where TService : IService;
    }
}
