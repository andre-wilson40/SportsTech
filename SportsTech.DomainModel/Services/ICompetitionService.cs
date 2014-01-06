using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services
{
    public interface ICompetitionService: IService, IService<Data.Model.Competition>, IWritableService<Data.Model.Competition>
    {
        Task<Data.Model.Competition> GetByIdAsync(int id);
    }
}
