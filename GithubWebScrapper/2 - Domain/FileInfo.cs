using System;

namespace GitHubWebScrapper.Domain
{
    public class GithubFileInfo
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Extension { get; set; }
        public float Bytes { get; set; }
        public int Lines { get; set; }

        public GithubFileInfo(string name, string url, string extension, float bytes, int lines)
        {
            this.SetUrl(url);
            this.SetName(name);
            this.SetExtension(extension);
            this.SetBytes(bytes);
            this.SetLines(lines);
        }

        public void SetUrl(string url)
        {
            this.Url = url;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }
        public void SetExtension(string extension)
        {
            this.Extension = extension;
        }
        public void SetBytes(float bytes)
        {
            this.Bytes = bytes;
        }
        public void SetLines(int lines)
        {
            this.Lines = lines;
        }
    }
}
