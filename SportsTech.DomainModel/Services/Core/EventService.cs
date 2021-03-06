﻿using SportsTech.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services.Core
{
    public class EventService : ServiceBase<Data.Model.Event>, IEventService
    {
        public EventService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
                        
        }

        public async Task<List<Data.Model.Event>> GetBySeasonAsync(string season)
        {            
            return null;
        }

        public async Task<Data.Model.Event> GetByIdAsync(int id)
        {
            return await SingleAsync(p => p.Id == id);
        }
    }
}
