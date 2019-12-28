using System.IO;

namespace WebScrapper
{
    public class CleaningHandler
    {
        public void DeleteArtifacts()
        {
            var pathToTempDirectory = Path.Combine(Path.GetTempPath(), "ETLArtifacts");

            var files = Directory.EnumerateFiles(pathToTempDirectory);

            foreach (var file in files)
            {
                File.Delete(file);
            }
        }
    }
}
