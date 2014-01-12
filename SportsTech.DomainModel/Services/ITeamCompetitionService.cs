using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services
{
    public interface ITeamCompetitionService : IService, IWritableService<Data.Model.CompetitionRegistration>
    {
        Task<Data.Model.CompetitionRegistration> GetByIdAsync(int id);
        Task<Data.Model.Team> GetTeam(int teamId);
        Task<List<Data.Model.CompetitionRegistration>> GetCompetitionsAsync(int teamId);
        Task<List<Data.Model.CompetitionRegistration>> GetCompetitionsAsync(int teamId, Expression<Func<Data.Model.CompetitionRegistration, bool>> expression);
        Task<List<Data.Model.Season>> GetUnRegisteredCompetitionsAsync(int teamId, int clubId);
    }
}
