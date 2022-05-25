using DepreciationDBApp.Domain.Entities;
using DepreciationDBApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepreciationDBApp.Infrastructure.Repositories
{
    public class EFAssetRepository : IAssetRepository
    {
        public IDepreciationDbContext depreciationDbContext;

        public EFAssetRepository(IDepreciationDbContext depreciationDbContext)
        {
            this.depreciationDbContext = depreciationDbContext;
        }
        public int Create(Asset t)
        {
            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            depreciationDbContext.Assets.Add(t);
            return depreciationDbContext.SaveChanges();
        }

        public bool Delete(Asset t)
        {
            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            depreciationDbContext.Assets.Remove(t);
            int result = depreciationDbContext.SaveChanges();
            return result > 0;
        }

        public Asset FindByCode(string code)
        {
            return depreciationDbContext.Assets.FirstOrDefault(x => x.Code.Equals(code));
        }

        public Asset FindById(int id)
        {
            return depreciationDbContext.Assets
                                        .FirstOrDefault(x => x.Id == id);
        }

        public List<Asset> FindByName(string name)
        {
            return depreciationDbContext.Assets.Where(x => x.Name.Equals(name)).Select(x => x).ToList();
        }

        public List<Asset> GetAll()
        {
            return depreciationDbContext.Assets.ToList();
        }

        public int Update(Asset t)
        {
            if (t is null)
            {
                throw new ArgumentNullException(nameof(t));
            }

            depreciationDbContext.Assets.Update(t);
            return depreciationDbContext.SaveChanges();
        }
    }
}
