using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

namespace Backend_Uc12_FiadoFacil
{
    public class Payment
    {
        public int Id { get; set; }
        public double Value { get; set; }
        public string Method { get; set; } = string.Empty;
        public DateTime ToDate { get; set; }
        public DateTime DueDate { get; set; }
        public User User { get; set; }
        public Company Company { get; set; }

        public const string Tabela = "payments";

        public Payment() { }

        public async Task InserirAsync()
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"INSERT INTO {Tabela} (value, method, to_date, due_date, user_id, company_id) VALUES (@value, @method, @toDate, @dueDate, @userId, @companyId)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@value", Value);
            command.Parameters.AddWithValue("@method", Method);
            command.Parameters.AddWithValue("@toDate", ToDate);
            command.Parameters.AddWithValue("@dueDate", DueDate);
            command.Parameters.AddWithValue("@userId", User?.Id);
            command.Parameters.AddWithValue("@companyId", Company?.Id);
            await command.ExecuteNonQueryAsync();
            Id = (int)command.LastInsertedId;
        }

        public static async Task<List<Payment>> BuscarTodosAsync()
        {
            var payments = new List<Payment>();
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $@"
                SELECT p.*, u.name AS user_name, c.name AS company_name 
                FROM {Tabela} p
                INNER JOIN users u ON p.user_id = u.id
                INNER JOIN companies c ON p.company_id = c.id";
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                payments.Add(new Payment
                {
                    Id = reader.GetInt32("id"),
                    Value = reader.GetDouble("value"),
                    Method = reader.GetString("method"),
                    ToDate = reader.GetDateTime("to_date"),
                    DueDate = reader.GetDateTime("due_date"),
                    User = new User { Id = reader.GetInt32("user_id"), Name = reader.GetString("user_name") },
                    Company = new Company { Id = reader.GetInt32("company_id"), Name = reader.GetString("company_name") }
                });
            }
            return payments;
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
