using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4Service.Models
{
    public class UserApplication
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public short DefaultModule { get; set; }
        public long CompanyId { get; set; }
        public string CompanyName { get; set; }
        public long DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public DateTime LicenseEnd { get; set; }
        public DateTime GraceEnd { get; set; }
        public Dictionary<long, string> Modules { get; set; }

        public UserApplication() { }

        public UserApplication(long id, string username, string firstName, string lastName, string email, string role, short defaultModule, long companyId, string companyName, long departmentId, string departmentName, DateTime licenseEnd, DateTime graceEnd, Dictionary<long, string> modules)
        {
            Id = id;
            Username = username ?? throw new ArgumentNullException(nameof(username));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Role = role ?? throw new ArgumentNullException(nameof(role));
            DefaultModule = defaultModule;
            CompanyId = companyId;
            CompanyName = companyName ?? throw new ArgumentNullException(nameof(companyName));
            DepartmentId = departmentId;
            DepartmentName = departmentName ?? throw new ArgumentNullException(nameof(departmentName));
            LicenseEnd = licenseEnd;
            GraceEnd = graceEnd;
            Modules = modules ?? throw new ArgumentNullException(nameof(modules));
        }

        public UserApplication(int id, string username, string firstName, string lastName, string email, string role, short defaultModule, int companyId, string companyName, int departmentId, string departmentName, DateTime licenseEnd, DateTime graceEnd)
        {
            Id = id;
            Username = username ?? throw new ArgumentNullException(nameof(username));
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Role = role ?? throw new ArgumentNullException(nameof(role));
            DefaultModule = defaultModule;
            CompanyId = companyId;
            CompanyName = companyName ?? throw new ArgumentNullException(nameof(companyName));
            DepartmentId = departmentId;
            DepartmentName = departmentName ?? throw new ArgumentNullException(nameof(departmentName));
            LicenseEnd = licenseEnd;
            GraceEnd = graceEnd;
        }
    }
}
