using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySqlConnector;

namespace Backend_Uc12_FiadoFacil
{
    public class ProductPayment
    {
        public const string Tabela = "_product_payment"; 
        
        // This is a join table. If it had an entity, it would have Product and Payment object references.
        // But since it's just a method class, we'll keep the static signature.
        // Or if the user wants this as an entity:
        public int Id { get; set; }
        public Product Product { get; set; }
        public Payment Payment { get; set; }

        public static async Task InserirAsync(Product product, Payment payment)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $"INSERT INTO {Tabela} (product_id, payment_id) VALUES (@productId, @paymentId)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@productId", product.Id);
            command.Parameters.AddWithValue("@paymentId", payment.Id);
            await command.ExecuteNonQueryAsync();
        }

        public static async Task<List<Product>> BuscarProdutosPorPagamentoAsync(int paymentId)
        {
            var products = new List<Product>();
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            await connection.OpenAsync();
            string query = $@"
                SELECT p.*, c.name as company_name 
                FROM {Tabela} pp
                INNER JOIN products p ON pp.product_id = p.id
                INNER JOIN companies c ON p.company_id = c.id
                WHERE pp.payment_id = @paymentId";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@paymentId", paymentId);
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
                    UrlImg = reader.GetString("url_img"),
                    Company = new Company { Id = reader.GetInt32("company_id"), Name = reader.GetString("company_name") }
                });
            }
            return products;
        }
    }
}
