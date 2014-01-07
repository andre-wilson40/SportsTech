﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services.Core
{
    public class SeasonService : ServiceBase<Data.Model.Season>, ISeasonService
    {
        private readonly Data.Model.Competition _competition;
            
        public SeasonService(
            Data.Model.Competition competition,
            Data.IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _competition = competition;
        }

        protected override IQueryable<Data.Model.Season> QueryAsync
        {
            get
            {
                return Repository.AsQueryable().Where(p => p.CompetitionId == _competition.Id);
            }
        }

        public Task<Data.Model.Season> GetByIdAsync(int id)
        {
            return SingleAsync(p => p.Id == id);
        }
    }

    public class SeasonServiceFactory : ISeasonServiceFactory
    {
        private readonly ICompetitionService _competitionService;
        private readonly Data.IUnitOfWork _unitOfWork;

        public SeasonServiceFactory(
            Data.IUnitOfWork unitOfWork,
            ICompetitionService competitionService)
        {
            _unitOfWork = unitOfWork;
            _competitionService = competitionService;
        }

        public async Task<ISeasonService> CreateAsync(int competitionId)
        {
            var competition = await _competitionService.GetByIdAsync(competitionId);

            return new SeasonService(competition, _unitOfWork);
        }
    }
}
