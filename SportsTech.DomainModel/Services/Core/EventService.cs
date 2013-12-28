using SportsTech.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services.Core
{
    public class EventService : ServiceBase<Data.Model.Event>
    {
        public EventService(IDataContext dataContext) : base(dataContext.Events)
        {
                        
        }

        public async Task<List<Data.Model.Event>> GetBySeasonAsync(string season)
        {
            return null;
        }

        public async Task<Data.Model.Event> SingleAsync(int eventId)
        {
            return await SingleAsync(p => p.Id == eventId);
        }
    }
}
