using System;
using System.Collections.Generic;
using MySqlConnector;
using Backend_Uc12_FiadoFacil.Models;

namespace Backend_Uc12_FiadoFacil.Repositories
{
    public class UserRepository
    {
        public void Insert(User user)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = "INSERT INTO users (name, type, email, senha, createAt, updateAt) VALUES (@name, @type, @email, @senha, @createAt, @updateAt)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@type", user.Type);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@senha", user.Senha);
            command.Parameters.AddWithValue("@createAt", DateTime.Now);
            command.Parameters.AddWithValue("@updateAt", DateTime.Now);
            command.ExecuteNonQuery();
            user.Id = (int)command.LastInsertedId;
        }

        public List<User> GetAll()
        {
            var users = new List<User>();
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = "SELECT id, name, type, email, senha, createAt, updateAt FROM users";
            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
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

        public User? GetById(int id)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = "SELECT id, name, type, email, senha, createAt, updateAt FROM users WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
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

        public void Update(User user)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = "UPDATE users SET name = @name, type = @type, email = @email, senha = @senha, updateAt = @updateAt WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@type", user.Type);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@senha", user.Senha);
            command.Parameters.AddWithValue("@updateAt", DateTime.Now);
            command.Parameters.AddWithValue("@id", user.Id);
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            // Delete cascading manually or relying on DB CASCADE
            // Context asks for cascade delete. If DB has ON DELETE CASCADE it's fine, otherwise we need to do it here.
            // Let's rely on DB for FK, but delete users record
            string query = "DELETE FROM users WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}
