using SportsTech.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services
{
    public interface IEventService : IService, IService<Event>, IWritableService<Event>
    {
        Task<List<Event>> GetBySeasonAsync(string season);
        Task<Event> GetByIdAsync(int id);
    }
}
