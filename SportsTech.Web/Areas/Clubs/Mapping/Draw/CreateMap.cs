using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SportsTech.Web.Areas.Clubs.Mapping.Draw
{
    public class CreateDataModelMap : SportsTech.Domain.IMap<Data.Model.Event, ViewModels.Draw.CreateViewModel>
    {
        private readonly Data.Model.Event _event;

        public CreateDataModelMap(Data.Model.Event ev)
        {
            _event = ev;
        }

        public Data.Model.Event Map(ViewModels.Draw.CreateViewModel source)
        {            
            _event.EventDate = source.EventDate;
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
        private string _returnUrl;

        public CreateViewModelMap(IEnumerable<Data.Model.Team> teams, string returnUrl)
        {
            _teams = teams;
            _returnUrl = returnUrl;
        }

        public ViewModels.Draw.CreateViewModel Map(Data.Model.Event source)
        {
            return new ViewModels.Draw.CreateViewModel
            {
                SeasonId = source.SeasonId,                
                EventDate = DateTime.Now,
                ReturnUrl = _returnUrl,
                IsHomeGame = source.Participants.IsHomeGame,
                Against = string.Empty,
                TeamId = source.Participants.TeamId,
                Teams = _teams.Select(p => new SelectListItem
                {
                    Selected =  source.Participants.TeamId == p.Id,
                    Text = p.Name,
                    Value = p.Id.ToString()
                }).ToList()
            };
        }
    }
}