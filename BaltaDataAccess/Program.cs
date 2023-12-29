using System;
using System.Data;
using BaltaDataAccess.Models.Domain;
using Dapper;
using Microsoft.Data.SqlClient;

namespace BaltaDataAccess
{
    class Program{

        static void Main(string[] args)
        {
            //apontar servidor
            const string connectionString = ";database = balta;trusted_connection = true;TrustServerCertificate=true";
            //Microsoft.Data.SqlClient (NUGET)

            


            using(var connection = new SqlConnection(connectionString))
            {

                ExecuteProcedure(connection);
                //CreateManyStudenties(connection);



            }





            
            
        }


        static void ListCategories(SqlConnection connection)
        {
            var categories = connection.Query<Category>("SELECT [Id], [Title] FROM [Category]");
            foreach (var item in categories)
            {
                Console.WriteLine($"{item.Id} - {item.Title}");
            }

        }

        static void CreateCategory(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviço do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var insertSql = @"INSERT INTO 
                    [Category] 
                VALUES(
                    @Id,
                    @Title,
                    @Url,
                    @Summary,
                    @Order,
                    @Description,
                    @Featured)";

            var rows = connection.Execute(insertSql, new
            {
                category.Id,
                category.Title,
                category.Url,
                category.Summary,
                category.Order,
                category.Description,
                category.Featured

            });
            Console.WriteLine($"{rows} linhas inseridas");



        }

        static void UpdateCategory(SqlConnection connection)
        {
            var updateQuery = @"UPDATE 
                    [Category]
                SET
                    [Title]=@title
                WHERE
                    [Id]=@id";

            var rows = connection.Execute(updateQuery, new
            {
                id = new Guid ("AF3407AA-11AE-4621-A2EF-2028B85507C4"),
                title = "Frontend 2022"
            });

            Console.WriteLine($"{rows} registros atualizados");


        }

        static void DeleteCategory(SqlConnection connection)
        {
            var deleteQuery = @"DELETE FROM
                    [Category]
                WHERE
                    [Id]=@id";

            var rows = connection.Execute(deleteQuery, new
            {
                id = new Guid("AF829190-DD0E-43E3-8103-95A089C3AF3D"),
                
            });

            Console.WriteLine($"{rows} registros apagados");





        }

        static void GetCategory(SqlConnection connection)
        {
            var getCategory = @"SELECT TOP 1 
                    [Id],
                    [Title]
                FROM
                    [Category]
                WHERE 
                    [Id]=@id";

            var category = connection.QueryFirstOrDefault<Category>( getCategory, new {id = "09CE0B7B-CFCA-497B-92C0-3290AD9D5142" });

            Console.WriteLine($"{category.Id} - {category.Title}");

        }

        static void CreateManyCategories(SqlConnection connection)
        {
            var category = new Category();
            category.Id = Guid.NewGuid();
            category.Title = "Amazon AWS";
            category.Url = "amazon";
            category.Description = "Categoria destinada a serviço do AWS";
            category.Order = 8;
            category.Summary = "AWS Cloud";
            category.Featured = false;

            var category2 = new Category();
            category2.Id = Guid.NewGuid();
            category2.Title = "Categoria Nova";
            category2.Url = "categoria-nova";
            category2.Description = "Categoria Nova";
            category2.Order = 9;
            category2.Summary = "Categoria";
            category2.Featured = false;

            var insertSql = @"INSERT INTO 
                    [Category] 
                VALUES(
                    @Id,
                    @Title,
                    @Url,
                    @Summary,
                    @Order,
                    @Description,
                    @Featured)";

            var rows = connection.Execute(insertSql, new[] 
            {
                new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured

                },
                new
                {
                    category2.Id,
                    category2.Title,
                    category2.Url,
                    category2.Summary,
                    category2.Order,
                    category2.Description,
                    category2.Featured

                }
            });
            Console.WriteLine($"{rows} linhas inseridas");



        }
        static void CreateManyStudenties(SqlConnection connection)
        {
            var student = new Student();
            student.Id = Guid.NewGuid();
            student.Name = "Rafael Freire";
            student.Email = "rafael@email.com";
            student.Document = "document";
            student.Phone = "1234567890";
            student.Birthdate = new DateTime(2001,10,10);
            student.CreateDate = new DateTime(2023, 10, 29);

            var student2 = new Student();
            student2.Id = Guid.NewGuid();
            student2.Name = "Gabriel Freire";
            student2.Email = "gabriel@email.com";
            student2.Document = "document";
            student2.Phone = "1234567890";
            student2.Birthdate = new DateTime(2001, 10, 10);
            student2.CreateDate = new DateTime(2023, 11, 29);

            var insertSql = @"INSERT INTO 
                    [Student] 
                VALUES(
                    @Id,
                    @Name,
                    @Email,
                    @Document,
                    @Phone,
                    @Birthdate,
                    @CreateDate)";

            var rows = connection.Execute(insertSql, new[]
            {
                new
                {
                    student.Id,
                    student.Name,
                    student.Email,
                    student.Document,
                    student.Phone,
                    student.Birthdate,
                    student.CreateDate
                },
                new
                {
                    student2.Id,
                    student2.Name,
                    student2.Email,
                    student2.Document,
                    student2.Phone,
                    student2.Birthdate,
                    student2.CreateDate
                }
            }
            
            
            
            
            
            
            );
            Console.WriteLine($"{rows} linhas inseridas");



        }

        static void ExecuteProcedure(SqlConnection connection) 
        {

            var procedure = "[spDeleteStudent]";
            var pars = new { StudentId = "859B79E3-B091-4131-888A-962E3E5CFB33" };

            var affectedRows = connection.Execute( procedure, pars, commandType: CommandType.StoredProcedure );

            Console.WriteLine($"{affectedRows} linhas afetadas.");
        }

    }

}