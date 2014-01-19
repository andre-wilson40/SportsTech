using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportsTech.Web.Areas.Clubs.Mapping.Draw
{
    public class DataTableMap : Domain.IMap<ViewModels.Draw.ListItemViewModel, Data.Model.Event>
    {
        public ViewModels.Draw.ListItemViewModel Map(Data.Model.Event source)
        {
            return new ViewModels.Draw.ListItemViewModel
            {
                EventDate = source.EventDate,
                Id = source.Id,
                IsHomeGame = source.Participants.IsHomeGame,
                Name = string.Format("{0} vs {1}", source.Participants.Team.Name, source.Participants.Opposition.Name),
                Score = string.Format("{0} - {1}", source.Participants.ScoreFor, source.Participants.ScoreAgainst)
            };
        }
    }
}