namespace NYSM.Model
{
    public class TestResult
    {
        public int Id { get; set; }
        public User User { get; set; }
        public TestConfig TestcCnfig { get; set; }
        public double Percentage { get; set; }
    }
}