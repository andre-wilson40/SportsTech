using SportsTech.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SportsTech.Web.Services.Implementation
{
    public class ClubSeasonFacadeService : IClubSeasonFacadeService
    {
        private readonly IClubService _clubService;
        private readonly ICompetitionService _competitionService;

        public ClubSeasonFacadeService(
            IClubService clubService,
            ICompetitionService competitionService)
        {
            _clubService = clubService;
            _competitionService = competitionService;
        }

        public async Task<Models.ClubCompetition> GetClubCompetitionAsync(int competitionId)
        {
            var competition = await _competitionService.GetByIdAsync(competitionId);
            
            return await Task.Run(() => new Models.ClubCompetition(competition));
        }

    }
}