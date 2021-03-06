﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Areas.Clubs.Mapping.Draw
{
    public class CreateDataModelMap : SportsTech.Domain.IMap<Data.Model.Event, ViewModels.Draw.CreateViewModel>
    {
        private readonly Data.Model.Event _event;

        public CreateDataModelMap()
            : this(new Data.Model.Event())
        {

        }

        public CreateDataModelMap(Data.Model.Event ev)
        {
            _event = ev;
        }

        public Data.Model.Event Map(ViewModels.Draw.CreateViewModel source)
        {
            _event.EventDate = source.EventDate;
            _event.SeasonRoundId = source.RoundId;
            _event.SeasonId = source.SeasonId;
            _event.Participants = new Data.Model.EventParticipant
                {
                    IsHomeGame = source.IsHomeGame,
                    TeamId = source.TeamId,
                    Opposition = new Data.Model.Opposition // TODO:  Use existing if present
                    {
                        Name = source.Against
                    }
                };

            return _event;
        }
    }

    public class CreateViewModelMap : SportsTech.Domain.IMap<ViewModels.Draw.CreateViewModel, Data.Model.Event>
    {
        private IEnumerable<Data.Model.Team> _teams;
        private IEnumerable<Data.Model.SeasonRound> _rounds;
        private string _returnUrl;

        public CreateViewModelMap(IEnumerable<Data.Model.Team> teams, IEnumerable<Data.Model.SeasonRound> rounds, string returnUrl)
        {
            _teams = teams;
            _rounds = rounds;
            _returnUrl = returnUrl;
        }

        public ViewModels.Draw.CreateViewModel Map(Data.Model.Event source)
        {
            var participants = source.Participants ?? new Data.Model.EventParticipant();
            var opposition = participants.Opposition ?? new Data.Model.Opposition();
            var round = source.Round ?? new Data.Model.SeasonRound();

            return new ViewModels.Draw.CreateViewModel
            {
                SeasonId = source.SeasonId,                
                EventDate = source.EventDate,
                ReturnUrl = _returnUrl,
                IsHomeGame = participants.IsHomeGame,
                Against = opposition.Name,
                TeamId = participants.TeamId,                
                Teams = _teams.Select(p => new SelectListItem
                {
                    Selected = participants.TeamId == p.Id,
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList(),
                RoundId = round.Id,
                Rounds = _rounds.Select(p => new SelectListItem
                {
                    Selected = source.SeasonRoundId == p.Id,
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList()
            };
        }
    }
}