using DepreciationDBApp.Domain.Entities;
using DepreciationDBApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepreciationDBApp.Infrastructure.Repositories
{
    public class EFEmployeeRepository : IEmployeeRepository
    {
        private IDepreciationDbContext depreciationDbContext;
        public EFEmployeeRepository(IDepreciationDbContext depreciationDbContext)
        {
            this.depreciationDbContext = depreciationDbContext;

        }
        public void Create(Employee t)
        {
            try
            {
                depreciationDbContext.Employees.Add(t);
                depreciationDbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(Employee t)
        {
            try
            {
                if (t is null)
                {
                    throw new ArgumentNullException(nameof(t));
                }

                Employee employee = FindByDni(t.Dni);
                depreciationDbContext.Employees.Remove(employee);
                int result = depreciationDbContext.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Employee FindByDni(string dni)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dni))
                {
                    throw new ArgumentException($"'{nameof(dni)}' cannot be null or whitespace.", nameof(dni));
                }

                return depreciationDbContext.Employees.FirstOrDefault(x => x.Dni.Equals(dni));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Employee FindByEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                {
                    throw new ArgumentException($"'{nameof(email)}' cannot be null or whitespace.", nameof(email));
                }

                return depreciationDbContext.Employees.FirstOrDefault(x => x.Email.Equals(email));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Employee> FindByLastnames(string lastName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(lastName))
                {
                    throw new ArgumentException($"'{nameof(lastName)}' cannot be null or whitespace.", nameof(lastName));
                }

                return depreciationDbContext.Employees.Where(x => x.Names.Equals(lastName, StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Employee> GetAll()
        {
            return depreciationDbContext.Employees.ToList();
        }

        public int Update(Employee t)
        {
            try
            {
                if (t == null)
                {
                    throw new ArgumentNullException("El objeto employee no puede ser null.");
                }

                Employee employee = FindByDni(t.Dni);
                if (employee == null)
                {
                    throw new Exception($"El objeto employee con Dni {t.Dni} no existe.");
                }

                employee.Names = t.Names;
                employee.Email = t.Email;
                employee.Lastnames = t.Lastnames;
                employee.Phone = t.Phone;

                depreciationDbContext.Employees.Update(employee);
                return depreciationDbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
