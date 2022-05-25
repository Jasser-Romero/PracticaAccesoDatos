using DepreciationDBApp.Applications.Interfaces;
using DepreciationDBApp.Domain.Entities;
using DepreciationDBApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
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
        private IAssetRepository assetRepository;
        public EmployeeService(IAssetEmployeeRepository assetEmployeeRepository, IEmployeeRepository employeeRepository, IAssetRepository assetRepository)
        {
            this.assetEmployeeRepository = assetEmployeeRepository;
            this.employeeRepository = employeeRepository;
            this.assetRepository = assetRepository;
        }
        public int Create(Employee t)
        {
            return employeeRepository.Create(t);
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
            bool success = false;
            using IDbContextTransaction transaction = employeeRepository.GetTransaction();
            try
            {
                if (assets == null || assets.Count == 0)
                {
                    throw new ArgumentNullException("La lista no puede estar vacia");
                }
                foreach (Asset asset in assets)
                {
                    success = SetAssetToEmployee(employee, asset, efectiveDate);
                    if (!success)
                    {
                        throw new Exception($"Fallo al asignar el asseId{asset.Id} al empleado {employee.Id}.");
                        //break;
                    }
                    asset.Status = "Asignado";
                    success = assetRepository.Update(asset) > 0;

                    if (!success)
                    {
                        throw new Exception($"Fallo al actualizar el asseId{asset.Id}.");
                    }
                }


                if (success)
                {
                    transaction.Commit();
                }

                return success;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SetAssetToEmployee(Employee employee, Asset asset, DateTime efectiveDate)
        {
            if (asset.Status.Equals("Asignado"))
            {
                throw new Exception("El activo ya esta asignado");
            }
            
            AssetEmployee assetEmployee = new AssetEmployee()
            {
                AssetId = asset.Id,
                EmployeeId = employee.Id,
                Date = efectiveDate,
                IsActive = true
            };
            return assetEmployeeRepository.Create(assetEmployee) > 0;
            
        }

        public bool UnsetAssetsToEmployee(Employee employee, List<Asset> assets)
        {
            bool success = false;
            using IDbContextTransaction transaction = employeeRepository.GetTransaction();
            try
            {

                if (assets == null || assets.Count == 0)
                {
                    throw new ArgumentNullException("La lista no puede estar vacia");
                }
                foreach (Asset asset in assets)
                {
                    success = UnsetAssetToEmployee(employee, asset);
                    if (!success)
                    {
                        throw new Exception($"Fallo al actualizar el asset, Id{asset.Id}.");
                    }
                }

                if (success)
                {
                    transaction.Commit();
                }

                return success;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UnsetAssetToEmployee(Employee employee, Asset asset)
        {
            try
            {
                asset.Status = "Disponible";
                return assetRepository.Update(asset) > 0;
            }
            catch (Exception)
            {

                throw;
            }
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

            if (asset.Status.Equals("Asignado"))
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
