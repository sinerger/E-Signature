using System;

namespace E_Signature
{
    public static class Signature
    {
        public static DateTime timeAccuracy { get; set; }
        public static int GetSing(string body, string secretKey)
        {
            if (body != null && secretKey != null)
            {
                var time = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss:ffff");
                var sing = body.GetHashCode() + body.GetHashCode()+ time.GetHashCode();
                return sing;
            }

            throw  new ArgumentNullException();
        }

        public static bool IsValidSing(string inputJson, string secretKey, TimeSpan timeDrift, bool expected)
        {

        }

    }
}
