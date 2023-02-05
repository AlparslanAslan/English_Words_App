namespace mvc_project.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
public class FileContext : DbContext
{
    public string getNames()
    {
        string? sonuc = "";
        try
        {
            string cs = @"
                Server=172.17.0.2 ;Database=test ;User Id=sa;Password=balamir1234;Trusted_Connection=False;TrustServerCertificate=true;
                ";
            var con = new SqlConnection(cs);
            con.Open();
            var name = con.ExecuteScalar<string>("select top 1 word from english_words");
            sonuc = name;
            File.WriteAllText("/home/alparslan/Documents/test.txt",name);
        }
        catch(Exception e)
        {
            File.WriteAllText("/home/alparslan/Documents/test_error.txt",e.Message);
        }
        return sonuc;
    }
    public List<string> getMissingWords(List<string> words)
    {   
        var matchedList = new List<string>();
        try
        {
            string cs = @"
                Server=172.17.0.2 ;Database=test ;User Id=sa;Password=balamir1234;Trusted_Connection=False;TrustServerCertificate=true;
                ";
            var con = new SqlConnection(cs);
            con.Open();
            var parameters = new { wordList = words };
            var sql = "select word from english_words where word in (@wordList)";
            var result = con.Query<string>(sql, parameters);
            matchedList = result.ToList<string>();
        }
        catch(Exception e)
        {
            File.WriteAllText("/home/alparslan/Documents/test_error.txt",e.Message);
        }
        if(matchedList.Count>0)
        {
            foreach (var item in matchedList)
            {
               words.Remove(item); 
            }
        }
        return words;
    }

}
/*
var cs = @"Server=localhost\SQLEXPRESS;Database=testdb;Trusted_Connection=True;";

            using var con = new SqlConnection(cs);
            con.Open();

            var version = con.ExecuteScalar<string>("SELECT @@VERSION");

            Console.WriteLine(version);
*/