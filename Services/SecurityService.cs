using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace personal_website.Services
{
    public class SecurityService
    {
        private readonly HttpClient _httpClient;
        private readonly IWebAssemblyHostEnvironment _environment;

        public SecurityService(HttpClient httpClient, IWebAssemblyHostEnvironment environment)
        {
            _httpClient = httpClient;
            _environment = environment;
        }

        /// <summary>
        /// Configures HttpClient with security best practices
        /// </summary>
        public void ConfigureHttpClientSecurity()
        {
            // Disable caching for sensitive requests
            _httpClient.DefaultRequestHeaders.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
            {
                NoCache = true,
                NoStore = true
            };

            // Add additional security headers
            _httpClient.DefaultRequestHeaders.Add("X-Content-Type-Options", "nosniff");
            _httpClient.DefaultRequestHeaders.Add("X-Frame-Options", "DENY");
            _httpClient.DefaultRequestHeaders.Add("Referrer-Policy", "strict-origin-when-cross-origin");
        }

        /// <summary>
        /// Generates a Content Security Policy (CSP)
        /// </summary>
        /// <returns>CSP header value</returns>
        public string GenerateContentSecurityPolicy()
        {
            var baseAddress = _environment.BaseAddress;
            var cspDirectives = new System.Collections.Generic.List<string>
            {
                "default-src 'self'",
                $"script-src 'self' 'unsafe-inline' {baseAddress}",
                $"style-src 'self' 'unsafe-inline' {baseAddress}",
                $"img-src 'self' data: {baseAddress}",
                "connect-src 'self'",
                "font-src 'self'",
                "object-src 'none'",
                "base-uri 'self'"
            };

            return string.Join("; ", cspDirectives);
        }

        /// <summary>
        /// Validates and sanitizes input to prevent XSS
        /// </summary>
        /// <param name="input">Input string to sanitize</param>
        /// <returns>Sanitized input</returns>
        public string SanitizeInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            // Remove potentially dangerous HTML tags and attributes
            string sanitized = Regex.Replace(input, @"<[^>]+>", string.Empty);
            
            // Remove potential script injection
            sanitized = Regex.Replace(sanitized, @"<script.*?</script>", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            
            // Remove potential event handlers
            sanitized = Regex.Replace(sanitized, @"on\w+=""[^""]*""", string.Empty, RegexOptions.IgnoreCase);
            
            // Encode special characters
            sanitized = HtmlEncode(sanitized);

            return sanitized;
        }

        /// <summary>
        /// Custom HTML encoding method
        /// </summary>
        /// <param name="input">Input string to encode</param>
        /// <returns>HTML-encoded string</returns>
        private static string HtmlEncode(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&#x27;")
                .Replace("/", "&#x2F;");
        }

        /// <summary>
        /// Checks if a URL is safe to navigate to
        /// </summary>
        /// <param name="url">URL to validate</param>
        /// <returns>True if URL is safe, false otherwise</returns>
        public bool IsUrlSafe(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            try
            {
                var uri = new Uri(url, UriKind.RelativeOrAbsolute);
                
                // Only allow http, https, and relative URLs
                return !uri.IsAbsoluteUri || 
                       uri.Scheme == Uri.UriSchemeHttps || 
                       uri.Scheme == Uri.UriSchemeHttp;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Generates a secure random token
        /// </summary>
        /// <param name="length">Length of the token</param>
        /// <returns>Cryptographically secure random token</returns>
        public string GenerateSecureToken(int length = 32)
        {
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            byte[] tokenData = new byte[length];
            rng.GetBytes(tokenData);
            return Convert.ToBase64String(tokenData);
        }
    }
}
