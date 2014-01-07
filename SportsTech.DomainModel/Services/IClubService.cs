using SportsTech.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsTech.Domain.Services
{
    public interface IClubService : IService, IService<Data.Model.Club>
    {
        Task<int> AffliatedClubCount();
        Task<Data.Model.Club> GetByIdAsync(int id);
    }
}
