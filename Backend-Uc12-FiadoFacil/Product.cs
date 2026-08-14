using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

namespace Backend_Uc12_FiadoFacil
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public double Value { get; set; }
        public string Description { get; set; } = string.Empty;
        public string UrlImg { get; set; } = string.Empty;
        public int CompanyId { get; set; }

        public const string Tabela = "products";

        public Product() { }

        public async Task InserirAsync()
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"INSERT INTO {Tabela} (name, type, value, description, urlImg, companyId) VALUES (@name, @type, @value, @description, @urlImg, @companyId)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@type", Type);
            command.Parameters.AddWithValue("@value", Value);
            command.Parameters.AddWithValue("@description", Description);
            command.Parameters.AddWithValue("@urlImg", UrlImg);
            command.Parameters.AddWithValue("@companyId", CompanyId);
            await command.ExecuteNonQueryAsync();
            Id = (int)command.LastInsertedId;
        }

        public static async Task<List<Product>> BuscarTodosAsync()
        {
            var products = new List<Product>();
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"SELECT id, name, type, value, description, urlImg, companyId FROM {Tabela}";
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Type = reader.GetString("type"),
                    Value = reader.GetDouble("value"),
                    Description = reader.GetString("description"),
                    UrlImg = reader.GetString("urlImg"),
                    CompanyId = reader.GetInt32("companyId")
                });
            }
            return products;
        }

        public static async Task<Product?> BuscarPorIdAsync(int id)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"SELECT id, name, type, value, description, urlImg, companyId FROM {Tabela} WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new Product
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Type = reader.GetString("type"),
                    Value = reader.GetDouble("value"),
                    Description = reader.GetString("description"),
                    UrlImg = reader.GetString("urlImg"),
                    CompanyId = reader.GetInt32("companyId")
                };
            }
            return null;
        }

        public async Task AtualizarAsync()
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"UPDATE {Tabela} SET name = @name, type = @type, value = @value, description = @description, urlImg = @urlImg, companyId = @companyId WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@type", Type);
            command.Parameters.AddWithValue("@value", Value);
            command.Parameters.AddWithValue("@description", Description);
            command.Parameters.AddWithValue("@urlImg", UrlImg);
            command.Parameters.AddWithValue("@companyId", CompanyId);
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
