using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Web.Services
{
    /// <summary>
    /// Facade service implementation trying to follow the example set in 
    /// http://blog.ploeh.dk/2010/02/02/RefactoringtoAggregateServices/
    /// </summary>
    public interface IClubSeasonFacadeService
    {
        Task<Models.ClubCompetition> GetClubCompetitionAsync(int competitionId);        
    }
}
