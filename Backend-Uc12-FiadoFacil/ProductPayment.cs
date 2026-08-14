using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

namespace Backend_Uc12_FiadoFacil
{
    public class ProductPayment
    {
        public const string Tabela = "product_payments"; // Assuming this is the table name

        public static async Task InserirAsync(int productId, int paymentId)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"INSERT INTO {Tabela} (productId, paymentId) VALUES (@productId, @paymentId)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@productId", productId);
            command.Parameters.AddWithValue("@paymentId", paymentId);
            await command.ExecuteNonQueryAsync();
        }

        public static async Task<List<int>> BuscarProdutosPorPagamentoAsync(int paymentId)
        {
            var productIds = new List<int>();
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"SELECT productId FROM {Tabela} WHERE paymentId = @paymentId";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@paymentId", paymentId);
            using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                productIds.Add(reader.GetInt32("productId"));
            }
            return productIds;
        }
    }
}
