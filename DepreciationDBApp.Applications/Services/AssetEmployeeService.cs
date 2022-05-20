using DepreciationDBApp.Applications.Interfaces;
using DepreciationDBApp.Domain.Entities;
using DepreciationDBApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepreciationDBApp.Applications.Services
{
    public class AssetEmployeeService : IAssetEmployeeServices
    {
        private IAssetEmployeeRepository assetEmployeeRepository;
        public AssetEmployeeService(IAssetEmployeeRepository assetEmployeeRepository)
        {
            this.assetEmployeeRepository = assetEmployeeRepository;
        }
        public void Create(AssetEmployee t)
        {
            assetEmployeeRepository.Create(t);
        }

        public bool Delete(AssetEmployee t)
        {
            throw new NotImplementedException();
        }

        public List<AssetEmployee> FindByAssetId(int id)
        {
            throw new NotImplementedException();
        }

        public List<AssetEmployee> FindByEmployeeId(int id)
        {
            throw new NotImplementedException();
        }

        public AssetEmployee FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<AssetEmployee> GetAll()
        {
            return assetEmployeeRepository.GetAll();
        }

        public List<AssetEmployeeDTO> GetAllDTOs()
        {
            return assetEmployeeRepository.GetAll().Count == 0 ? new List<AssetEmployeeDTO>() : assetEmployeeRepository.GetAll().Select(x => AssetEmployeeDTO.CreateDTO(x)).ToList();
        }

        public int Update(AssetEmployee t)
        {
            throw new NotImplementedException();
        }
    }
}
