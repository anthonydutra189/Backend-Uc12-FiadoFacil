using System;
using System.Collections.Generic;
using MySqlConnector;
using Backend_Uc12_FiadoFacil.Models;

namespace Backend_Uc12_FiadoFacil.Repositories
{
    public class PaymentRepository
    {
        public void Insert(Payment payment)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = @"INSERT INTO payments (value, method, to_date, due_date, user_id, company_id, createAt, updateAt) 
                             VALUES (@value, @method, @toDate, @dueDate, @userId, @companyId, @createAt, @updateAt)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@value", payment.Value);
            command.Parameters.AddWithValue("@method", payment.Method);
            command.Parameters.AddWithValue("@toDate", payment.ToDate);
            command.Parameters.AddWithValue("@dueDate", payment.DueDate);
            command.Parameters.AddWithValue("@userId", payment.UserId);
            command.Parameters.AddWithValue("@companyId", payment.CompanyId);
            command.Parameters.AddWithValue("@createAt", DateTime.Now);
            command.Parameters.AddWithValue("@updateAt", DateTime.Now);
            command.ExecuteNonQuery();
            payment.Id = (int)command.LastInsertedId;
        }

        public List<Payment> GetAll()
        {
            var payments = new List<Payment>();
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = "SELECT id, value, method, to_date, due_date, user_id, company_id, createAt, updateAt FROM payments";
            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                payments.Add(new Payment
                {
                    Id = reader.GetInt32("id"),
                    Value = reader.GetDouble("value"),
                    Method = reader.GetString("method"),
                    ToDate = reader.GetDateTime("to_date"),
                    DueDate = reader.GetDateTime("due_date"),
                    UserId = reader.GetInt32("user_id"),
                    CompanyId = reader.GetInt32("company_id"),
                    CreateAt = reader.GetDateTime("createAt"),
                    UpdateAt = reader.GetDateTime("updateAt")
                });
            }
            return payments;
        }

        public Payment? GetById(int id)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = "SELECT id, value, method, to_date, due_date, user_id, company_id, createAt, updateAt FROM payments WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Payment
                {
                    Id = reader.GetInt32("id"),
                    Value = reader.GetDouble("value"),
                    Method = reader.GetString("method"),
                    ToDate = reader.GetDateTime("to_date"),
                    DueDate = reader.GetDateTime("due_date"),
                    UserId = reader.GetInt32("user_id"),
                    CompanyId = reader.GetInt32("company_id"),
                    CreateAt = reader.GetDateTime("createAt"),
                    UpdateAt = reader.GetDateTime("updateAt")
                };
            }
            return null;
        }

        public void Delete(int id)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = "DELETE FROM payments WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        // Sem método Update, conforme especificação
    }
}
