namespace FileReader
{
    public class FileReader
    {
        public static string[] FileToArray(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}