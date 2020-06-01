using System;

namespace FeatuR.EntityFramework.SqlServer
{
    public class FeatuRSettings
    {
        public string Host { get; set; }
        public int Port { get; set; } = 5434;
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string GetConnectionString() => $"Server={Host},{Port};User id={User};Pwd={Password};Database={Database};";

        public void ValidateSettings()
        {
            if (string.IsNullOrWhiteSpace(Host)) throw new InvalidOperationException("The host cannot be null or empty");
            if (string.IsNullOrWhiteSpace(User)) throw new InvalidOperationException("The user cannot be null or empty");
            if (string.IsNullOrWhiteSpace(Password)) throw new InvalidOperationException("The password cannot be null or empty");
            if (string.IsNullOrWhiteSpace(Database)) throw new InvalidOperationException("The database cannot be null or empty");
            if (Port < 0) throw new InvalidOperationException("The port should be positive");
        }
    }
}
