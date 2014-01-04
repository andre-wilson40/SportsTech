using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services.Core
{
    public class TeamService : ServiceBase<Data.Model.Team>, ITeamService
    {
        public TeamService(Data.IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
