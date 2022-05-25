using DepreciationDBApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepreciationDBApp.Applications.Interfaces
{
    public interface IEmployeeServices : IService<Employee>
    {
        Employee FindByDni(string dni);
        Employee FindByEmail(string email);
        IEnumerable<Employee> FindByLastnames(string lastName);
        bool SetAssetToEmployee(Employee employee, Asset asset, DateTime efectiveDate);
        bool SetAssetsToEmployee(Employee employee, List<Asset> assets, DateTime efectiveDate);
        bool UnsetAssetToEmployee(Employee employee, Asset asset);
        bool UnsetAssetsToEmployee(Employee employee, List<Asset> assets);
    }
}
