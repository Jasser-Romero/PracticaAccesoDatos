using DepreciationDBApp.Domain.Entities;
using DepreciationDBApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepreciationDBApp.Infrastructure.Repositories
{
    public class EFAssetEmployeeRepository : IAssetEmployeeRepository
    {
        private IDepreciationDbContext depreciationDbContext;
        public EFAssetEmployeeRepository(IDepreciationDbContext depreciationDbContext)
        {
            this.depreciationDbContext = depreciationDbContext;
        }
        public void Create(AssetEmployee t)
        {
            try
            {
                depreciationDbContext.AssetEmployees.Add(t);
                depreciationDbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(AssetEmployee t)
        {
            throw new NotImplementedException();
        }

        public List<AssetEmployee> FindByAssetId(int id)
        {
            return depreciationDbContext.AssetEmployees.Where(x => x.AssetId == id).ToList();
        }

        public List<AssetEmployee> FindByEmployeeId(int id)
        {
            return depreciationDbContext.AssetEmployees.Where(x => x.EmployeeId == id).ToList();
        }

        public AssetEmployee FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<AssetEmployee> GetAll()
        {
            return depreciationDbContext.AssetEmployees.ToList();
        }

        public int Update(AssetEmployee t)
        {
            throw new NotImplementedException();
        }
    }
}
