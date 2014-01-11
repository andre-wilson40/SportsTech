using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services
{
    public interface ISeasonService : IService, IService<Data.Model.Season>, IWritableService<Data.Model.Season>
    {
        Task<Data.Model.Season> GetByIdAsync(int id);
    }

    public interface ISeasonServiceFactory
    {
        Task<ISeasonService> CreateAsync(int competitionId);
        ISeasonService Create();
    }
}
