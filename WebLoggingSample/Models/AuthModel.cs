using WebLoggingSample.Attributes;

namespace WebLoggingSample.Models
{
    [Logless("Password")]
    public class IndexAuthModel
    {
        public string Login { get; set; }

        public string Password { get; set; }
    }
}
