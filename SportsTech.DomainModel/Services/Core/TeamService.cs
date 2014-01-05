using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using LinqKit;

namespace SportsTech.Domain.Services.Core
{
    public class TeamService : ServiceBase<Data.Model.Team>, ITeamService
    {
        private readonly Data.Model.Club _club;

        public TeamService(
            Data.Model.Club club,
            Data.IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _club = club;
        }

        protected override IQueryable<Data.Model.Team> QueryAsync
        {
            get
            {
                return Repository.AsQueryable().Where(p => p.ClubId == _club.Id);
            }
        }

        public override Data.Model.Team Add(Data.Model.Team ev)
        {
            ev.Club = _club;
            return base.Add(ev);
        }

        public override async Task<bool> CanAdd(Data.Model.Team ev, IErrorHandler errorHandler)
        {
            var exists = await AnyAsync(p => p.Name == ev.Name && p.ClubId == _club.Id);
            
            if (exists)
            {
                errorHandler.AddError("Name", "This team already exists", ErrorTypeEnum.Error);
            }

            return await base.CanAdd(ev, errorHandler);
        }

        public Task<Data.Model.Team> GetByIdAsync(int id)
        {
            return SingleAsync(p => p.Id == id && p.ClubId == _club.Id);
        }
    }
}
