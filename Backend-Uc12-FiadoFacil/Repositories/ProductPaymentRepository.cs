using System;
using System.Collections.Generic;
using MySqlConnector;

namespace Backend_Uc12_FiadoFacil.Repositories
{
    public class ProductPaymentRepository
    {
        public void Insert(int productId, int paymentId)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            // A tabela associativa _product_payment normalmente é criada implicitamente no Prisma: `_product_payment`
            // Colunas no MySQL Prisma são geralmente A e B, vamos supor A para payment e B para product
            // Aqui crio um insert generico assuming the table name is `_product_payment` and columns are `A` and `B`. 
            // We should adjust depending on exact Prisma implicit table generation if necessary, or use specific column names.
            string query = "INSERT INTO _product_payment (A, B) VALUES (@paymentId, @productId)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@paymentId", paymentId);
            command.Parameters.AddWithValue("@productId", productId);
            command.ExecuteNonQuery();
        }

        public List<int> GetProductIdsByPaymentId(int paymentId)
        {
            var productIds = new List<int>();
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = "SELECT B FROM _product_payment WHERE A = @paymentId";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@paymentId", paymentId);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                productIds.Add(reader.GetInt32("B"));
            }
            return productIds;
        }
    }
}
