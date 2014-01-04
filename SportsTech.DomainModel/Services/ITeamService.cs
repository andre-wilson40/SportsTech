using SportsTech.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services
{
    public interface ITeamService : IService, IService<Team>, IWritableService<Team>
    {
    }
}
