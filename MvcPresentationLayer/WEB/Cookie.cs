using BusinessLogicalLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPresentationLayer.Models
{
    public static class Cookie
    {
        private const string CookieSetting = "Cookie.Duration";
        private const string CookieIsHttp = "Cookie.IsHttp";
        private static HttpContext _context { get { return HttpContext.Current; } }

        static Cookie()
        {
        }

        public static void Set(string key, string value)
        {
            value = RijndaelManagedEncryption.EncryptRijndael(value, "SENTINELADOVALESALT");
            var c = new HttpCookie(key)
            {
                Value = value,
                Expires = DateTime.Now.AddDays(10),
                HttpOnly = true
            };
            _context.Response.Cookies.Add(c);
        }

        public static string Get(string key)
        {
            var value = string.Empty;

            var c = _context.Request.Cookies[key];
            return c != null
                    ? _context.Server.HtmlEncode(RijndaelManagedEncryption.DecryptRijndael(c.Value, "SENTINELADOVALESALT")).Trim()
                    : value;
        }

        public static bool Exists(string key)
        {
            return _context.Request.Cookies[key] != null;
        }

        public static void Delete(string key)
        {
            if (Exists(key))
            {
                var c = new HttpCookie(key) { Expires = DateTime.Now.AddDays(-1) };
                _context.Response.Cookies.Add(c);
            }
        }

        public static void DeleteAll()
        {
            for (int i = 0; i <= _context.Request.Cookies.Count - 1; i++)
            {
                if (_context.Request.Cookies[i] != null)
                    Delete(_context.Request.Cookies[i].Name);
            }
        }
    }
}