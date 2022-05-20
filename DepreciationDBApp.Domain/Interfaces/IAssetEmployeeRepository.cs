using DepreciationDBApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepreciationDBApp.Domain.Interfaces
{
    public interface IAssetEmployeeRepository : IRepository<AssetEmployee>
    {
        List<AssetEmployee> FindByAssetId(int id);
        List<AssetEmployee> FindByEmployeeId(int id);
        AssetEmployee FindById(int id);
    }
}
