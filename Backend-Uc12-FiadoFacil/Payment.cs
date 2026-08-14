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
        public int UserId { get; set; }
        public int CompanyId { get; set; }

        public const string Tabela = "payments";

        public Payment() { }

        public async Task InserirAsync()
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"INSERT INTO {Tabela} (value, method, toDate, dueDate, userId, companyId) VALUES (@value, @method, @toDate, @dueDate, @userId, @companyId)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@value", Value);
            command.Parameters.AddWithValue("@method", Method);
            command.Parameters.AddWithValue("@toDate", ToDate);
            command.Parameters.AddWithValue("@dueDate", DueDate);
            command.Parameters.AddWithValue("@userId", UserId);
            command.Parameters.AddWithValue("@companyId", CompanyId);
            await command.ExecuteNonQueryAsync();
            Id = (int)command.LastInsertedId;
        }

        public static async Task<List<Payment>> BuscarTodosAsync()
        {
            var payments = new List<Payment>();
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"SELECT id, value, method, toDate, dueDate, userId, companyId FROM {Tabela}";
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                payments.Add(new Payment
                {
                    Id = reader.GetInt32("id"),
                    Value = reader.GetDouble("value"),
                    Method = reader.GetString("method"),
                    ToDate = reader.GetDateTime("toDate"),
                    DueDate = reader.GetDateTime("dueDate"),
                    UserId = reader.GetInt32("userId"),
                    CompanyId = reader.GetInt32("companyId")
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
