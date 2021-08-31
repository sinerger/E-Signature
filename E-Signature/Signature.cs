using System;
using E_Signature.Extensions;

namespace E_Signature
{
    public static class Signature
    {
        private static TimeSpan _timeRounding;

        static Signature()
        {
            _timeRounding = TimeSpan.FromSeconds(5);
        }

        public static void Configure(TimeSpan timeRounding)
        {
            _timeRounding = timeRounding;
        }

        public static string GetSing(string inputBody, string secretKey)
        {
            var result = GetSing(inputBody, secretKey, DateTime.Now);

            return result.ToString();
        }

        public static bool IsValidSing(string inputBody, string inputSing, string secretKey, TimeSpan timeDrift)
        {
            if (!string.IsNullOrEmpty(inputBody) && !string.IsNullOrEmpty(secretKey) && !string.IsNullOrEmpty(inputSing) && timeDrift != default)
            {
                var startTime = DateTime.Now - timeDrift;
                var finishTime = DateTime.Now + timeDrift;
                var result = false;

                var count = 0;
                for (var i = startTime; i < finishTime; i += _timeRounding)
                {
                    count++;
                    var sing = GetSing(inputBody, secretKey, i);

                    if (sing == inputSing)
                    {
                        result = true;

                        break;
                    }
                }

                return true;
            }

            throw new ArgumentException();
        }

        private static string GetSing(string inputBody, string secretKey, DateTime time)
        {
            if (!string.IsNullOrEmpty(inputBody) && !string.IsNullOrEmpty(secretKey))
            {
                var timeNow = time.Round(_timeRounding);
                var currentTime = timeNow.ToString("yyyy.MM.dd HH:mm:ss:ffff");
                var sing = inputBody.GetHashCode() + secretKey.GetHashCode() + currentTime.GetHashCode();

                return sing.ToString();
            }
            else if (string.IsNullOrEmpty(inputBody))
            {
                throw new ArgumentNullException("String inputBody is empty or null");
            }
            else
            {
                throw new ArgumentNullException("String secretKey is empty or null");
            }

        }
    }
}
