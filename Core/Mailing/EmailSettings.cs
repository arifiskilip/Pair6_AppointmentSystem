namespace Core.Mailing
{
    public class EmailSettings
    {
        public string SmptpServer { get; set; }
        public int SmptpPort { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
