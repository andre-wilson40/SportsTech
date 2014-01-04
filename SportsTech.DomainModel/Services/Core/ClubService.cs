using SportsTech.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SportsTech.Domain.Services.Core
{
    public class ClubService : ServiceBase<Data.Model.Club>, IClubService
    {
        private int _currentUserId;

        public ClubService(
            IUnitOfWork unitOfWork,
            IUserService userService)
            : base(unitOfWork)          
        {
            _currentUserId = userService.CurrentUserProfile().Id;           
        }

        
        public Task<int> AffliatedClubCount()
        {
            return CountAsync(p => p.UserProfiles.Any(u => u.Id == _currentUserId));
        }

        public override async Task<List<Data.Model.Club>> GetAllAsync()
        {
            return await Repository
                .AsQueryable()
                .Where(p => p.UserProfiles.Any(u => u.Id == _currentUserId))
                .ToListAsync();
        }
    }
}
