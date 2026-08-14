using System;
using System.Collections.Generic;
using MySqlConnector;
using Backend_Uc12_FiadoFacil.Models;

namespace Backend_Uc12_FiadoFacil.Repositories
{
    public class ProductRepository
    {
        public void Insert(Product product)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = @"INSERT INTO products (name, type, value, description, url_img, company_id, createAt, updateAt) 
                             VALUES (@name, @type, @value, @description, @urlImg, @companyId, @createAt, @updateAt)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@type", product.Type);
            command.Parameters.AddWithValue("@value", product.Value);
            command.Parameters.AddWithValue("@description", product.Description);
            command.Parameters.AddWithValue("@urlImg", product.UrlImg);
            command.Parameters.AddWithValue("@companyId", product.CompanyId);
            command.Parameters.AddWithValue("@createAt", DateTime.Now);
            command.Parameters.AddWithValue("@updateAt", DateTime.Now);
            command.ExecuteNonQuery();
            product.Id = (int)command.LastInsertedId;
        }

        public List<Product> GetAll()
        {
            var products = new List<Product>();
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = "SELECT id, name, type, value, description, url_img, company_id, createAt, updateAt FROM products";
            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Type = reader.GetString("type"),
                    Value = reader.GetDouble("value"),
                    Description = reader.GetString("description"),
                    UrlImg = reader.GetString("url_img"),
                    CompanyId = reader.GetInt32("company_id"),
                    CreateAt = reader.GetDateTime("createAt"),
                    UpdateAt = reader.GetDateTime("updateAt")
                });
            }
            return products;
        }

        public Product? GetById(int id)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = "SELECT id, name, type, value, description, url_img, company_id, createAt, updateAt FROM products WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new Product
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    Type = reader.GetString("type"),
                    Value = reader.GetDouble("value"),
                    Description = reader.GetString("description"),
                    UrlImg = reader.GetString("url_img"),
                    CompanyId = reader.GetInt32("company_id"),
                    CreateAt = reader.GetDateTime("createAt"),
                    UpdateAt = reader.GetDateTime("updateAt")
                };
            }
            return null;
        }

        public void Update(Product product)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = @"UPDATE products 
                             SET name = @name, type = @type, value = @value, description = @description, 
                                 url_img = @urlImg, company_id = @companyId, updateAt = @updateAt 
                             WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@type", product.Type);
            command.Parameters.AddWithValue("@value", product.Value);
            command.Parameters.AddWithValue("@description", product.Description);
            command.Parameters.AddWithValue("@urlImg", product.UrlImg);
            command.Parameters.AddWithValue("@companyId", product.CompanyId);
            command.Parameters.AddWithValue("@updateAt", DateTime.Now);
            command.Parameters.AddWithValue("@id", product.Id);
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new MySqlConnection(ConfiguracaoBD.connectionString);
            connection.Open();
            string query = "DELETE FROM products WHERE id = @id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}
