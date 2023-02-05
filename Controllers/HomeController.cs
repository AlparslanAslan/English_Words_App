using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvc_project.Models;
using mvc_project.Data;
using mvc_project.Methods;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace mvc_project.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {

        return View();
    }
    [HttpPost]
    public ActionResult Index(mvc_project.Models.File file)
    {
        //string s = Request.Form.Files[0].FileName;
        //string extention = Path.GetExtension (Request.Form.Files[0].FileName);
        
        var meth = new Home();
        var wordList = meth.GetWordsFromFile(Request.Form.Files[0]);
        var newWords = meth.GetMissingWordsInDB(wordList);
        meth.Find_WriteMeaning(newWords);

        string sonuc = "";
        foreach (var item in wordList)
        {
            sonuc += item + '\n';
        }
        return Content(sonuc) ;

    }
    public  IActionResult Get()
    {
        string str = "";
        string path = @"/home/alparslan/Documents/project_files/english_words.txt";
        using (FileStream fs = System.IO.File.Open(path,FileMode.Open)) 
        {
            byte[] b = new byte[1024];
           UTF8Encoding temp = new UTF8Encoding(true);
            //UTF32Encoding temp = new UTF32Encoding();
            //ASCIIEncoding temp = new ASCIIEncoding();

            
            while (fs.Read(b,0,b.Length) > 0)
            {
                str += temp.GetString(b) + " ";
            }
            
        }
        var words = str.Split('\n');
        Task<User> us = ApiMethodes.getApiInfo();
        var x = us.Result;
        
        Console.WriteLine(
            "id -> " + x.id +
            "userid -> "+x.userId +
            "title -> "+x.title +
            "complated -> "+ x.completed 
        );
        return View();
    }

    public ActionResult Privacy()
    {
        var fc = new FileContext();
        var sonucList = fc.getMissingWords(new List<string>(){"landmark","loon"});
        string sonuc = "";
        foreach (var item in sonucList)
        {
            sonuc += item;
        }
        return Content(sonuc);
    }
     public IActionResult About()
    {
        return View();
    }
    public IActionResult Test()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
