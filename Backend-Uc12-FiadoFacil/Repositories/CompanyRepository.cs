using System;
using System.Collections.Generic;
using MySqlConnector;
using Backend_Uc12_FiadoFacil.Models;

namespace Backend_Uc12_FiadoFacil.Repositories
{
    public class CompanyRepository
    {
        public void Insert(Company company)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = @"INSERT INTO companies (name, category, cnpj, places, zip_code, addrres, phone, user_id, logoUrl, createAt, updateAt) 
                             VALUES (@name, @category, @cnpj, @places, @zipCode, @addrres, @phone, @userId, @logoUrl, @createAt, @updateAt)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", company.Name);
            command.Parameters.AddWithValue("@category", company.Category);
            command.Parameters.AddWithValue("@cnpj", company.Cnpj);
            command.Parameters.AddWithValue("@places", company.Places);
            command.Parameters.AddWithValue("@zipCode", company.ZipCode);
            command.Parameters.AddWithValue("@addrres", company.Addrres);
            command.Parameters.AddWithValue("@phone", company.Phone);
            command.Parameters.AddWithValue("@userId", company.UserId);
            command.Parameters.AddWithValue("@logoUrl", company.LogoUrl ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@createAt", DateTime.Now);
            command.Parameters.AddWithValue("@updateAt", DateTime.Now);
            command.ExecuteNonQuery();
            company.Id = (int)command.LastInsertedId;
        }

        public List<Company> GetAll()
        {
            var companies = new List<Company>();
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = "SELECT id, name, category, cnpj, places, zip_code, addrres, phone, user_id, logoUrl, deletedAt, createAt, updateAt FROM companies";
            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                companies.Add(new Company
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Category = reader.GetString("category"),
                    Cnpj = reader.GetString("cnpj"),
                    Places = reader.GetString("places"),
                    ZipCode = reader.GetString("zip_code"),
                    Addrres = reader.GetString("addrres"),
                    Phone = reader.GetString("phone"),
                    UserId = reader.GetInt32("user_id"),
                    LogoUrl = reader.IsDBNull(reader.GetOrdinal("logoUrl")) ? null : reader.GetString("logoUrl"),
                    DeletedAt = reader.IsDBNull(reader.GetOrdinal("deletedAt")) ? null : reader.GetDateTime("deletedAt"),
                    CreateAt = reader.GetDateTime("createAt"),
                    UpdateAt = reader.GetDateTime("updateAt")
                });
            }
            return companies;
        }

        public Company? GetById(int id)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = "SELECT id, name, category, cnpj, places, zip_code, addrres, phone, user_id, logoUrl, deletedAt, createAt, updateAt FROM companies WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Company
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Category = reader.GetString("category"),
                    Cnpj = reader.GetString("cnpj"),
                    Places = reader.GetString("places"),
                    ZipCode = reader.GetString("zip_code"),
                    Addrres = reader.GetString("addrres"),
                    Phone = reader.GetString("phone"),
                    UserId = reader.GetInt32("user_id"),
                    LogoUrl = reader.IsDBNull(reader.GetOrdinal("logoUrl")) ? null : reader.GetString("logoUrl"),
                    DeletedAt = reader.IsDBNull(reader.GetOrdinal("deletedAt")) ? null : reader.GetDateTime("deletedAt"),
                    CreateAt = reader.GetDateTime("createAt"),
                    UpdateAt = reader.GetDateTime("updateAt")
                };
            }
            return null;
        }

        public void Update(Company company)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = @"UPDATE companies 
                             SET name = @name, category = @category, cnpj = @cnpj, places = @places, 
                                 zip_code = @zipCode, addrres = @addrres, phone = @phone, user_id = @userId, 
                                 logoUrl = @logoUrl, updateAt = @updateAt 
                             WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", company.Name);
            command.Parameters.AddWithValue("@category", company.Category);
            command.Parameters.AddWithValue("@cnpj", company.Cnpj);
            command.Parameters.AddWithValue("@places", company.Places);
            command.Parameters.AddWithValue("@zipCode", company.ZipCode);
            command.Parameters.AddWithValue("@addrres", company.Addrres);
            command.Parameters.AddWithValue("@phone", company.Phone);
            command.Parameters.AddWithValue("@userId", company.UserId);
            command.Parameters.AddWithValue("@logoUrl", company.LogoUrl ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@updateAt", DateTime.Now);
            command.Parameters.AddWithValue("@id", company.Id);
            command.ExecuteNonQuery();
        }

        // Sem método Delete, conforme especificação
    }
}
