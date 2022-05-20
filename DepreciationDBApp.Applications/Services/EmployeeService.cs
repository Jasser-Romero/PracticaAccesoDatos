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
    public class EmployeeService : IEmployeeServices
    {
        private IEmployeeRepository employeeRepository;
        private IAssetEmployeeRepository assetEmployeeRepository;
        public EmployeeService(IAssetEmployeeRepository assetEmployeeRepository, IEmployeeRepository employeeRepository)
        {
            this.assetEmployeeRepository = assetEmployeeRepository;
            this.employeeRepository = employeeRepository;
        }
        public void Create(Employee t)
        {
            employeeRepository.Create(t);
        }

        public bool Delete(Employee t)
        {
            return employeeRepository.Delete(t);
        }

        public Employee FindByDni(string dni)
        {
            return employeeRepository.FindByDni(dni);
        }

        public Employee FindByEmail(string email)
        {
            return employeeRepository.FindByEmail(email);
        }

        public IEnumerable<Employee> FindByLastnames(string lastName)
        {
            return employeeRepository.FindByLastnames(lastName);
        }

        public List<Employee> GetAll()
        {
            return employeeRepository.GetAll();
        }

        public bool SetAssetsToEmployee(Employee employee, List<Asset> assets, DateTime efectiveDate)
        {
            foreach(Asset asset in assets)
            {
                SetAssetToEmployee(employee, asset, efectiveDate);
            }
            return true;
        }

        public bool SetAssetToEmployee(Employee employee, Asset asset, DateTime efectiveDate)
        {
            ValidateAssetEmployee(employee, asset);
            AssetEmployee assetEmployee = new AssetEmployee()
            {
                AssetId = asset.Id,
                EmployeeId = employee.Id,
                Date = efectiveDate,
                IsActive = true
            };
            assetEmployeeRepository.Create(assetEmployee);
            return true;
        }

        public int Update(Employee t)
        {
            throw new NotImplementedException();
        }

        private void ValidateAssetEmployee(Employee employee, Asset asset)
        {
            if (employee is null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            if (asset is null)
            {
                throw new ArgumentNullException(nameof(asset));
            }

            if (asset.Status.Equals("No disponible"))
            {
                //TODO: Add enums for asset and employee status
                throw new Exception("");
            }

            Employee employee1 = employeeRepository.FindByDni(employee.Dni);
            if (employee1 == null)
            {
                throw new ArgumentNullException($"El objeto employee con Dni{employee.Dni} no existe ");
            }
        }
    }
}
