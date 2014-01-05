using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

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

        public override Data.Model.Team Add(Data.Model.Team ev)
        {
            ev.Club = _club;
            return base.Add(ev);
        }

        public override async Task<List<Data.Model.Team>> GetAllAsync()
        {
            return await Repository
                .AsQueryable()
                .Where(p => p.ClubId == _club.Id)
                .ToListAsync();
        }

        public override bool CanAdd(Data.Model.Team ev, IErrorHandler errorHandler)
        {
            var exists = AnyAsync(p => p.Name == ev.Name && p.ClubId == _club.Id);
            exists.Wait();

            if (exists.Result)
            {
                errorHandler.AddError("Name", "This team already exists", ErrorTypeEnum.Error);
            }

            return base.CanAdd(ev, errorHandler);
        }
    }
}
