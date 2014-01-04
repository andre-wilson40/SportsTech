using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain
{
    public interface IErrorHandler
    {
        void AddError(string key, string errorMessage);
        bool IsValid { get; }
    }
}
