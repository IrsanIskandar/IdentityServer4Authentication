using IdentityServer4Service.IdentityServerConfig.IdentityNeeds;
using IdentityServer4Service.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer4Service.IdentityServerConfig.Repository
{
    public class UserRepository : IUserRepository
    {
        private static UserApplication user = new UserApplication();

        public string EncryptPassword(string password, string salt)
        {
            string encrypted = String.Empty;
            byte[] saltByte = Convert.FromBase64String(salt);
            encrypted = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltByte,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return encrypted;
        }

        public List<Claim> GetClaims()
        {
            string modules = JsonConvert.SerializeObject(user.Modules);

            return new List<Claim>
            {
                new Claim("UserID", user.Id.ToString()),
                new Claim("username", user.Username),
                new Claim("firstName", user.FirstName),
                new Claim("lastName", user.LastName),
                new Claim("email", user.Email),
                new Claim("role", user.Role),
                //new Claim("defaultModule", user.AdditionalModuleID.ToString()),
                new Claim("companyId", user.CompanyId.ToString()),
                new Claim("companyName", user.CompanyName),
                new Claim("departmentId", user.DepartmentId.ToString()),
                new Claim("departmentName", user.DepartmentName),
                new Claim("licenseEnd", user.LicenseEnd.ToString(), ClaimValueTypes.DateTime),
                new Claim("graceEnd", user.GraceEnd.ToString(), ClaimValueTypes.DateTime),
                new Claim("modules", modules)
            };
        }

        public string GetSalt(string username)
        {
            string salt = string.Empty;
            try
            {
                using (var conn = new SqlConnection(Constants.connStr))
                {
                    using (var cmd = new SqlCommand(Constants.SP_GETSALT, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue(Constants.USERNAME, username);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                salt = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Write(e.Source + ": " + e.Message);
            }

            return salt;
        }

        public UserApplication GetUser()
        {
            return user;
        }

        public UserApplication Login(string username, string password)
        {
            user = new UserApplication();
            Dictionary<long, string> modules = new Dictionary<long, string>();
            try
            {
                using (var conn = new SqlConnection(Constants.connStr))
                {
                    using (var cmd = new SqlCommand(Constants.SP_USERLOGIN, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue(Constants.USERNAME, username);
                        cmd.Parameters.AddWithValue(Constants.PASSWORD, password);
                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (!String.IsNullOrEmpty(user.Username))
                                {
                                    user = new UserApplication(
                                        id: reader.GetInt32(0),
                                        username: reader.GetString(1),
                                        firstName: reader.GetString(2),
                                        lastName: reader.GetString(3),
                                        email: reader.GetString(4),
                                        role: reader.GetString(5),
                                        defaultModule: reader.GetInt16(6),
                                        companyId: reader.GetInt32(7),
                                        companyName: reader.GetString(8),
                                        departmentId: reader.GetInt32(9),
                                        departmentName: reader.GetString(10),
                                        licenseEnd: reader.GetDateTime(11),
                                        graceEnd: reader.GetDateTime(12)
                                    );
                                }
                                modules.Add(reader.GetInt64(13), reader.GetString(14));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Write(e.Source + ": " + e.Message);
            }
            finally
            {
                user.Modules = modules;
            }
            return user;
        }
    }
}
