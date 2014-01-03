using SportsTech.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services.Core
{
    public class ClubService : ServiceBase<Data.Model.Club>, IClubService
    {
        public ClubService(IUnitOfWork unitOfWork)
            : base(unitOfWork)          
        {
        }

        
        public Task<int> AffliatedClubCount(int userId)
        {
            return CountAsync(p => p.UserProfiles.Any(u => u.Id == userId));
        }
    }
}
