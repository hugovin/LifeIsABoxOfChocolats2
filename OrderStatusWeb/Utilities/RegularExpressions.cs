using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Web;

namespace OrderStatusWeb.Utilities
{
    public class RegularExpressions
    {
        public static bool UrlIsValid(string smtpHost)
        {
            try
            {
                string url = "http://" + smtpHost;
                new Uri(url);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public  static bool ApiKeyIsvalid(string key)
        {
            try
            {
                Regex regex = new Regex("^[0-9]+$");
                if (key.Length == 32)
                {
                    if (regex.IsMatch(key))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
                return false;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}