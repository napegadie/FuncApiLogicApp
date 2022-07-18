using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace FuncBookApi
{
    public static class ReadApiBook
    {
        [FunctionName("ReadApiBook")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

        List<Book> _lst = new List<Book>();

            // Get the connection string from app settings and use it to create a connection.
            //var str = Environment.GetEnvironmentVariable("ConnectionStrings:sqldb_connection");
            var str = Environment.GetEnvironmentVariable("SQLAZURECONNSTR_SQLConnectionString");

            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                var text = "SELECT BookId,BookTitle,BookLocationName,BookAuthor,BookGenre,DatePublished from dbo.Books";

                using (SqlCommand cmd = new SqlCommand(text, conn))
                {
                    using (SqlDataReader _reader = cmd.ExecuteReader())
                    {
                        while (_reader.Read())
                        {
                            Book _book = new Book()
                            {
                                BookId = _reader.GetInt32(0),
                                BookTitle = _reader.GetString(1),
                                BookLocationName = _reader.GetString(1),
                                BookAuthor = _reader.GetString(1),
                                BookGenre = _reader.GetString(1),
                                //DatePublished = _reader.GetDateTime(2)
                            };

                            _lst.Add(_book);
                        }
                    }

                }
            }

            return new OkObjectResult(_lst);
        }
    }
}
