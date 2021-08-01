using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Text;

namespace Object13.Core.Security
{
    public static class XssSecurity
    {
        public static string SanitizeText(this string text)
        {
            var htmlSanitizer = new HtmlSanitizer();

            htmlSanitizer.KeepChildNodes = true;

            htmlSanitizer.AllowDataAttributes = true;

            return htmlSanitizer.Sanitize(text);
        }

        public static bool SanitizeBool(this string text)
        {
            try
            {
                var htmlSanitizer = new HtmlSanitizer();

                htmlSanitizer.KeepChildNodes = true;

                htmlSanitizer.AllowDataAttributes = true;

                return htmlSanitizer.Sanitize(text).ToBoolean();
            }
            catch
            {
                return false;
            }
            
        }
    }
}
