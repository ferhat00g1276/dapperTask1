using System.Data;
using System.Data.SqlClient;
using System.Net;
using Dapper;
using dapperTask1.Models;
using static System.Reflection.Metadata.BlobBuilder;

var conStr =  $"Data Source=LAPTOP-BG3587VM\\MSSQLSERVER01;Initial Catalog=Library;Integrated Security=true";

using var sqlConnection = new SqlConnection(conStr);


//var query = "SELECT Books.Name,Categories.Id,Categories.Name FROM Books JOIN Categories ON Books.Id_Category=Categories.Id";

//var BooksWithCategory = sqlConnection.Query<Book,Category,Book>(query,
//    map:
//    (book,category) => { book.Category = category; return book; },
//    splitOn:"Id"
//    ).ToList();
//foreach(var book in BooksWithCategory)
//{
//    Console.WriteLine(book.Name," ",book.Category.Name);
//}

var query = "SELECT Books.Name,Categories.Id,Categories.Name FROM Books JOIN Categories ON Categories.Id=Books.Id_Category";

var CategoryDict = new Dictionary<int, Category>();

var BooksWithCategory = sqlConnection.Query<Book, Category, Category>(query,
    map:
    (book, category) => {
    if(!CategoryDict.TryGetValue(category.Id, out Category currentCateg))
        {
            currentCateg = category;
            CategoryDict.Add(category.Id, currentCateg);
        }
    book.Category = currentCateg;
    currentCateg.Books.Add(book);
    return currentCateg;
    },
    splitOn: "Id"
    );


//string bookname = "SQL";
//var books = sqlConnection.Query<Book>("SelectBook", new {Name = bookname },commandType:CommandType.StoredProcedure).ToList();
//foreach (var book in books)
//{
//    Console.WriteLine($"Book ID: {book.Id}, Name: {book.Name}");
//}
Console.WriteLine();





