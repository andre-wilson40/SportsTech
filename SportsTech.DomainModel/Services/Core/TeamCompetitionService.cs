using LinqKit;
using SportsTech.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services.Core
{
    public class TeamCompetitionService : ITeamCompetitionService
    {
        private readonly ServiceBaseImpl _baseService;
        private readonly ITeamService _teamService;
        private readonly ICompetitionService _competitionService;

        public TeamCompetitionService(
            ITeamService teamService,   
            ICompetitionService competitionService,
            IUnitOfWork unitOfWork)
        {
            _teamService = teamService;
            _competitionService = competitionService;
            _baseService = new ServiceBaseImpl(unitOfWork);
        }

        public Task<Data.Model.Team> GetTeam(int teamId)
        {
            return _teamService.GetByIdAsync(teamId);
        }

        public Task<List<Data.Model.CompetitionRegistration>> GetCompetitionsAsync(int teamId)
        {
            return _baseService.GetAllAsync(p => p.TeamId == teamId);
        }

        public async Task<List<Data.Model.CompetitionRegistration>> GetCompetitionsAsync(int teamId, System.Linq.Expressions.Expression<Func<Data.Model.CompetitionRegistration, bool>> expression)
        {
            System.Linq.Expressions.Expression<Func<Data.Model.CompetitionRegistration, bool>> teamExpr = registration => registration.TeamId == teamId;
            
            // LinqKit and PredicateBuilder doesn't seem to support expressions when using the async set of extension methods.  The recommended
            // solution is to do dbset.AsExpandable() rather than doing AsQueryable().  However this fails.
            // As of today the source code could be altered as suggested by:
            // http://www.dhuvelle.com/2013_11_01_archive.html
            // However I'm hoping the original code will be updated instead to support this

            //var builder = PredicateBuilder.True<Data.Model.CompetitionRegistration>();
            //var filter = builder.And(teamExpr).And(expression);
            //var filter = builder.And(expression);
            
            // For now we don't have many registrations per team anyway so happy to do the second expr on an IEnumerable
            var registrations = await _baseService.GetAllAsync(teamExpr);
            return registrations.Where(expression.Compile()).ToList();
        }

        public async Task<List<Data.Model.Season>> GetUnRegisteredCompetitionsAsync(int teamId, int clubId)
        {
            // return all competitions and seasons this team isn't involved with
            var team = GetTeam(teamId);
            var competitions = _competitionService.GetAllAsync();

            await Task.WhenAll(team, competitions);

            var seasons = competitions.Result.SelectMany(p => p.Seasons);
            var currentSeasons = team.Result.CompetitionRegistrations.Select(p => p.Season);

            return seasons.Except(currentSeasons).ToList();

        }

        public void RegisterForSeason(Data.Model.Team team, Data.Model.Season season)
        {
            team.CompetitionRegistrations.Add(new Data.Model.CompetitionRegistration
                {
                    Team = team,
                    Season = season
                });
        }

        public async Task<bool> CanAdd(Data.Model.CompetitionRegistration ev, IErrorHandler errorHandler)
        {
            var exists = await _baseService.AnyAsync(p => p.SeasonId == ev.SeasonId && p.TeamId == ev.TeamId);

            if (exists)
            {
                var team = ev.Team ?? await GetTeam(ev.TeamId);
                errorHandler.AddError("SeasonId", "The " + team.Name + " team has already been added to the competition", ErrorTypeEnum.Error);
            }

            return await _baseService.CanAdd(ev, errorHandler);
        }

        public Data.Model.CompetitionRegistration Add(Data.Model.CompetitionRegistration ev)
        {
            return _baseService.Add(ev);
        }

        public void Remove(Data.Model.CompetitionRegistration ev)
        {
            _baseService.Remove(ev);
        }

        public Task<Data.Model.CompetitionRegistration> GetByIdAsync(int id)
        {
            return _baseService.SingleAsync(p => p.Id == id);
        }

        public void SaveAnyChanges()
        {
            _baseService.SaveAnyChanges();
        }

        internal class ServiceBaseImpl : ServiceBase<Data.Model.CompetitionRegistration>
        {
            internal ServiceBaseImpl(IUnitOfWork unitOfWork) : base(unitOfWork)
            {

            }
        }
    }
}
