using System.Text;
using System.Text.RegularExpressions;
using mvc_project.Data;

namespace mvc_project.Methods;

public class Home
{
    public List<string> GetWordsFromFile(IFormFile file)
    {
        var fs = file.OpenReadStream();
        byte[] b = new byte[1024];
        string str = "";
        string sonuc = "";
        UTF8Encoding temp = new UTF8Encoding(true);
        string patternText = "[a-z]";
        Regex reg = new Regex(patternText);

        while (fs.Read(b,0,b.Length) > 0)
        {
            str += temp.GetString(b);
        }
        var _str =  str.Split('\n');
        var list = new List<string>(_str);
        
        foreach (var x in list.ToList())
        {
            if (reg.IsMatch(x))
                sonuc += x + "\n";
            else
                list.Remove(x);

        }
        return list;
    }
    public List<string> GetMissingWordsInDB(List<string> fileWords)
    {
        var db = new FileContext();
        return db.getMissingWords(fileWords);
    }
    public void Find_WriteMeaning(List<string> newWords)
    {
        foreach (var item in newWords)
        {
            
        }
    }

}
