using Spectre.Console;
using System.Data;
using System.Data.SqlClient;
namespace ProductManagement2
{
    class Management
    {
        public static void AddProduct(SqlConnection con)
        {
            Console.WriteLine("Enter the Product Name: ");
            string pname = Console.ReadLine();
            Console.WriteLine("Enter the Product Description: ");
            string description = Console.ReadLine();
            Console.WriteLine("Enter Quantity: ");
            int qty = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Price: ");
            int price = Convert.ToInt32(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter("Select * from Product", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            var row = ds.Tables[0].NewRow();
            row["ProductName"] = pname;
            row["ProductDescription"] = description;
            row["Quantity"] = qty;
            row["Price"] = price;

            ds.Tables[0].Rows.Add(row);

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Database Updated");
        }

        public static void GetProduct(SqlConnection con)
        {
            Console.Write("Enter Product Id: ");
            int Id = Convert.ToInt32(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Product where ProductId ={Id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            var table = new Table();
            table.AddColumn("Product Id");
            table.AddColumn("Product Name");
            table.AddColumn("Product Description");
            table.AddColumn("Quantity");
            table.AddColumn("Price");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][4].ToString());
            }
            AnsiConsole.Write(new FigletText("Customer Details").Centered().Color(Color.Khaki1));
            AnsiConsole.Write(table);
        }

        public static void UpdateProduct(SqlConnection con)
        {
            Console.WriteLine("Enter the Product id you want to Update");
            int id = Convert.ToInt16(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Product where ProductId ={id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            Console.WriteLine("Enter New Product Name: ");
            string str1 = Console.ReadLine();
            Console.WriteLine("Enter New Description: ");
            string str2 = Console.ReadLine();
            Console.WriteLine("Enter Quantity: ");
            int qty = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the Price: ");
            int price = Convert.ToInt32(Console.ReadLine());
            ds.Tables[0].Rows[0][1] = $"{str1}";
            ds.Tables[0].Rows[0][2] = $"{str2}";
            ds.Tables[0].Rows[0][3] = $"{qty}";
            ds.Tables[0].Rows[0][4] = $"{price}";

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Database Updated");
        }

        public static void GetProducts(SqlConnection con)
        {
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Product", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            var table = new Table();
            table.AddColumn("Product Id");
            table.AddColumn("Product Name");
            table.AddColumn("Product Description");
            table.AddColumn("Quantity");
            table.AddColumn("Price");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                table.AddRow(ds.Tables[0].Rows[i][0].ToString(), ds.Tables[0].Rows[i][1].ToString(), ds.Tables[0].Rows[i][2].ToString(), ds.Tables[0].Rows[i][3].ToString(), ds.Tables[0].Rows[i][4].ToString());
            }
            AnsiConsole.Write(new FigletText("Customer Details").Centered().Color(Color.Khaki1));
            AnsiConsole.Write(table);
        }

        public static void DeleteProduct(SqlConnection con)
        {
            Console.WriteLine("Enter the Product id you want to Delete");
            int id = Convert.ToInt32(Console.ReadLine());
            SqlDataAdapter adp = new SqlDataAdapter($"Select * from Product where ProductId ={id}", con);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            ds.Tables[0].Rows[0].Delete();

            SqlCommandBuilder builder = new SqlCommandBuilder(adp);
            adp.Update(ds);
            Console.WriteLine("Entry Deleted");
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Server= US-513K9S3; database=ProductManagement; Integrated Security=true");
            string ans = "";
            int id;
            do
            {
                AnsiConsole.Write(new FigletText("Welcome to Product Management App").Centered().Color(Color.Red));
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .Title("[green] Select an option[/]")
                    .AddChoices(new[]
                    {
                        "Add New Product", "Get Product", "Get All Products", "Update Product", "Delete Product"
                    }));
                switch (choice)
                {
                    case "Add New Product":
                        { 
                            Management.AddProduct(con);
                            break;
                        }
                    case "Get All Products":
                        {
                            Management.GetProducts(con);
                            break;
                        }
                    case "Get Product":
                        {
                     
                            Management.GetProduct(con);
                            break;
                        }
                    case "Update Product":
                        {
                            
                            Management.UpdateProduct(con);
                            break;
                        }
                    case "Delete Product":
                        {
                            
                            Management.DeleteProduct(con);
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid choice!!!!");
                            break;
                        }
                }
                Console.WriteLine();
                Console.WriteLine("Do you wish to continue? [y/n] ");
                ans = Console.ReadLine();
                Console.WriteLine();

            } while (ans.ToLower() == "y");
        }
    }
}