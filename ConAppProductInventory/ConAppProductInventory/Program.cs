using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConAppProductInventory
{
    internal class Program
    {
        static SqlConnection con;
        static SqlCommand cmd;
        static string conStr = "server=LAPTOP-HURDBM1G;database=ProductInventoryDB;trusted_connection=true;";
        static void Main(string[] args)
        {
            try
            {
                con = new SqlConnection(conStr);
                con.Open();
                Console.WriteLine("Connected to Database!");

                ViewProductInventory();

                AddNewProduct();

                UpdateProductQuantity();

                RemoveProduct();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }

            Console.ReadKey();
        }

        static void ViewProductInventory()
        {
            string query = "SELECT * FROM Products";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = cmd.ExecuteReader();

            Console.WriteLine("\n-- Product Inventory --");
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["ProductId"]}, Name: {reader["ProductName"]}, Price: {reader["Price"]}, Quantity: {reader["Quantity"]}");
            }
            reader.Close();
        }

        static void AddNewProduct()
        {
            Console.WriteLine("\n-- Add New Product --");
            Console.Write("Enter Product ID: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Enter Product Name: ");
            string productName = Console.ReadLine();

            Console.Write("Enter Product Price: ");
            float price = float.Parse(Console.ReadLine());

            Console.Write("Enter Product Quantity: ");
            int quantity = int.Parse(Console.ReadLine());

            Console.Write("Enter Manufacturing Date (YYYY-MM-DD): ");
            DateTime mfDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Expiry Date (YYYY-MM-DD): ");
            DateTime expDate = DateTime.Parse(Console.ReadLine());

            string query = "INSERT INTO Products VALUES (@ProductId, @ProductName, @Price, @Quantity, @MfDate, @ExpDate)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductId", productId);
            cmd.Parameters.AddWithValue("@ProductName", productName);
            cmd.Parameters.AddWithValue("@Price", price);
            cmd.Parameters.AddWithValue("@Quantity", quantity);
            cmd.Parameters.AddWithValue("@MfDate", mfDate);
            cmd.Parameters.AddWithValue("@ExpDate", expDate);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
                Console.WriteLine("New Product Added Successfully!");
        }

        static void UpdateProductQuantity()
        {
            Console.WriteLine("\n-- Update Product Quantity --");
            Console.Write("Enter Product ID to update quantity: ");
            int productId = int.Parse(Console.ReadLine());

            Console.Write("Enter Updated Quantity: ");
            int newQuantity = int.Parse(Console.ReadLine());

            string query = "UPDATE Products SET Quantity = @Quantity WHERE ProductId = @ProductId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Quantity", newQuantity);
            cmd.Parameters.AddWithValue("@ProductId", productId);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
                Console.WriteLine("Product Quantity Updated Successfully!");
        }

        static void RemoveProduct()
        {
            Console.WriteLine("\n-- Remove Product --");
            Console.Write("Enter Product ID to remove: ");
            int productId = int.Parse(Console.ReadLine());

            string query = "DELETE FROM Products WHERE ProductId = @ProductId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@ProductId", productId);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected > 0)
                Console.WriteLine("Product Removed Successfully!");
        }
    }
}

