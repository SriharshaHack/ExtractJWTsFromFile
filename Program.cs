using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ExtractJWTsFromFile
{
    class Program
    {
        static void Main(string[] args)
        {
            var allLines = File.ReadAllLines(@"D:\fileContainingJWTs.txt");
            var jwtRegex = "Bearer[\\s]+([A-Za-z0-9-_=]+\\.[A-Za-z0-9-_=]+\\.)";
            long lineNo = 0;
            foreach (var line in allLines)
            {
                lineNo++; 
                Match m = Regex.Match(line, jwtRegex, RegexOptions.IgnoreCase);
                string capturedString = m.Groups[1].Value;
                if (!string.IsNullOrWhiteSpace(capturedString))
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    JwtSecurityToken token = handler.ReadJwtToken(capturedString);
                    string audiences = string.Join(';',token.Audiences);
                    Console.WriteLine($"Line: {lineNo} - {audiences}");
                }
                
            }
        }
    }
}
