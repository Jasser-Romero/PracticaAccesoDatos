using DepreciationDBApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepreciationDBApp.Applications.Interfaces
{
    public interface IAssetEmployeeServices : IService<AssetEmployee>
    {
        List<AssetEmployee> FindByAssetId(int id);
        List<AssetEmployee> FindByEmployeeId(int id);
        AssetEmployee FindById(int id);
        List<AssetEmployeeDTO> GetAllDTOs();
    }
}
