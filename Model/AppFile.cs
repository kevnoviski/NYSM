namespace NYSM.Model
{
    public class AppFile
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Description { get; set; }
        public int LineCount { get; set; }
        public int WordCount { get; set; }
    }
}