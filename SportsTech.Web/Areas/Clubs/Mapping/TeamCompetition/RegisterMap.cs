using SportsTech.Domain.Services;
using SportsTech.Web.Areas.Clubs.ViewModels.TeamCompetition;
using SportsTech.Web.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SportsTech.Web.Areas.Clubs.Mapping.TeamCompetition
{
    public class RegisterMap : Domain.IMap<RegisterViewModel, Data.Model.CompetitionRegistration>
    {
        private readonly ITeamCompetitionService _competitionService;

        public RegisterMap(ITeamCompetitionService competitionService)
        {
            _competitionService = competitionService;
        }

        public RegisterViewModel Map(Data.Model.CompetitionRegistration registration)
        {
            var viewModel = new RegisterViewModel();

            var availableCompetitions = AsyncHelpers.RunSync(() => GetAvailableCompetitions(registration));
            viewModel.TeamId = registration.TeamId;
            viewModel.Seasons = availableCompetitions.ToList();
            
            return viewModel;
        }

        private async Task<IEnumerable<System.Web.Mvc.SelectListItem>> GetAvailableCompetitions(Data.Model.CompetitionRegistration registration)
        {
            var competitions = await _competitionService.GetUnRegisteredCompetitionsAsync(registration.TeamId,registration.Team.ClubId);

            return competitions.Select(p => new System.Web.Mvc.SelectListItem
                {
                    Selected = false,
                    Text = string.Format("{0} - {1}", p.Competition.Name, p.Name),
                    Value = p.Id.ToString()
                });
        } 
    }
}