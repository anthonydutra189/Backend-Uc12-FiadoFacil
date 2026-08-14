using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

namespace Backend_Uc12_FiadoFacil
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Senha { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }

        public const string Tabela = "users";

        public User() { }

        public async Task InserirAsync()
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"INSERT INTO {Tabela} (name, type, email, senha, createAt, updateAt) VALUES (@name, @type, @email, @senha, @createAt, @updateAt)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@type", Type);
            command.Parameters.AddWithValue("@email", Email);
            command.Parameters.AddWithValue("@senha", Senha);
            command.Parameters.AddWithValue("@createAt", DateTime.Now);
            command.Parameters.AddWithValue("@updateAt", DateTime.Now);
            await command.ExecuteNonQueryAsync();
            Id = (int)command.LastInsertedId;
        }

        public static async Task<List<User>> BuscarTodosAsync()
        {
            var users = new List<User>();
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"SELECT id, name, type, email, senha, createAt, updateAt FROM {Tabela}";
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Type = reader.GetString("type"),
                    Email = reader.GetString("email"),
                    Senha = reader.GetString("senha"),
                    CreateAt = reader.GetDateTime("createAt"),
                    UpdateAt = reader.GetDateTime("updateAt")
                });
            }
            return users;
        }

        public static async Task<User?> BuscarPorIdAsync(int id)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"SELECT id, name, type, email, senha, createAt, updateAt FROM {Tabela} WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new User
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Type = reader.GetString("type"),
                    Email = reader.GetString("email"),
                    Senha = reader.GetString("senha"),
                    CreateAt = reader.GetDateTime("createAt"),
                    UpdateAt = reader.GetDateTime("updateAt")
                };
            }
            return null;
        }

        public async Task AtualizarAsync()
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"UPDATE {Tabela} SET name = @name, type = @type, email = @email, senha = @senha, updateAt = @updateAt WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", Name);
            command.Parameters.AddWithValue("@type", Type);
            command.Parameters.AddWithValue("@email", Email);
            command.Parameters.AddWithValue("@senha", Senha);
            command.Parameters.AddWithValue("@updateAt", DateTime.Now);
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
