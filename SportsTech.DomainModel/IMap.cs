using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain
{
    public interface IMap<TTarget, TSource> 
        where TTarget: class, new()
        where TSource: class, new()
    {
        TTarget Map(TSource source);
    }
}
