using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

namespace Backend_Uc12_FiadoFacil
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string Places { get; set; } = string.Empty;
        public string ZipCode { get; set; } = string.Empty;
        public string Addrres { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public int UserId { get; set; }

        public const string Tabela = "companies";

        public Company() { }

        public async Task InserirAsync()
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"INSERT INTO {Tabela} (name, category, cnpj, places, zipCode, addrres, phone, userId) VALUES (@name, @category, @cnpj, @places, @zipCode, @addrres, @phone, @userId)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@category", Category);
            command.Parameters.AddWithValue("@cnpj", Cnpj);
            command.Parameters.AddWithValue("@places", Places);
            command.Parameters.AddWithValue("@zipCode", ZipCode);
            command.Parameters.AddWithValue("@addrres", Addrres);
            command.Parameters.AddWithValue("@phone", Phone);
            command.Parameters.AddWithValue("@userId", UserId);
            await command.ExecuteNonQueryAsync();
            Id = (int)command.LastInsertedId;
        }

        public static async Task<List<Company>> BuscarTodosAsync()
        {
            var companies = new List<Company>();
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"SELECT id, name, category, cnpj, places, zipCode, addrres, phone, userId FROM {Tabela}";
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                companies.Add(new Company
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Category = reader.GetString("category"),
                    Cnpj = reader.GetString("cnpj"),
                    Places = reader.GetString("places"),
                    ZipCode = reader.GetString("zipCode"),
                    Addrres = reader.GetString("addrres"),
                    Phone = reader.GetString("phone"),
                    UserId = reader.GetInt32("userId")
                });
            }
            return companies;
        }

        public static async Task<Company?> BuscarPorIdAsync(int id)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"SELECT id, name, category, cnpj, places, zipCode, addrres, phone, userId FROM {Tabela} WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Company
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Category = reader.GetString("category"),
                    Cnpj = reader.GetString("cnpj"),
                    Places = reader.GetString("places"),
                    ZipCode = reader.GetString("zipCode"),
                    Addrres = reader.GetString("addrres"),
                    Phone = reader.GetString("phone"),
                    UserId = reader.GetInt32("userId")
                };
            }
            return null;
        }

        public async Task AtualizarAsync()
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"UPDATE {Tabela} SET name = @name, category = @category, cnpj = @cnpj, places = @places, zipCode = @zipCode, addrres = @addrres, phone = @phone, userId = @userId WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@category", Category);
            command.Parameters.AddWithValue("@cnpj", Cnpj);
            command.Parameters.AddWithValue("@places", Places);
            command.Parameters.AddWithValue("@zipCode", ZipCode);
            command.Parameters.AddWithValue("@addrres", Addrres);
            command.Parameters.AddWithValue("@phone", Phone);
            command.Parameters.AddWithValue("@userId", UserId);
            command.Parameters.AddWithValue("@id", Id);
            await command.ExecuteNonQueryAsync();
        }

        public static async Task DeletarAsync(int id)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"DELETE FROM {Tabela} WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            await command.ExecuteNonQueryAsync();
        }
    }
}
