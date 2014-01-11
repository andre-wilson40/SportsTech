using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services.Core
{
    public class CompetitionService : ServiceBase<Data.Model.Competition>, ICompetitionService
    {
        private readonly Data.Model.Club _club;
        
        public CompetitionService(
            Data.Model.Club club,
            Data.IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            _club = club;
        }

        protected override IQueryable<Data.Model.Competition> QueryAsync
        {
            get
            {
                return Repository.AsQueryable().Where(p => p.ClubId == _club.Id);
            }
        }

        public override Data.Model.Competition Add(Data.Model.Competition ev)
        {
            ev.Club = _club;
            return base.Add(ev);
        }

        public override async Task<bool> CanAdd(Data.Model.Competition ev, IErrorHandler errorHandler)
        {
            var exists = await AnyAsync(p => p.Name == ev.Name && p.Id != ev.Id);
            
            if (exists)
            {
                errorHandler.AddError("Name", "This competition already exists", ErrorTypeEnum.Error);
            }

            return await base.CanAdd(ev, errorHandler);
        }

        public Task<Data.Model.Competition> GetByIdAsync(int id)
        {
            return SingleAsync(p => p.Id == id);
        }
    }
}
