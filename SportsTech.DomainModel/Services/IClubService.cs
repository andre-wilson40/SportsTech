using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services
{
    public interface IClubService : IService, IService<Data.Model.Club>
    {
        Task<int> AffliatedClubCount(int userId);
    }
}
