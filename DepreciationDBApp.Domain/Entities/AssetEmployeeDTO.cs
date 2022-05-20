using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepreciationDBApp.Domain.Entities
{
    public class AssetEmployeeDTO
    {
        public int Id { get; set; }
        public string Asset { get; set; }
        public string Employee { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool IsActive { get; set; }

        public static AssetEmployeeDTO CreateDTO(AssetEmployee t)
        {
            return new AssetEmployeeDTO()
            {
                Id = t.Id,
                Asset = t.Asset.Name,
                Employee = t.Employee.Names,
                EffectiveDate = t.Date,
                IsActive = t.IsActive
            };
        }
    }
}
