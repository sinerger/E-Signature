using System;
using E_Signature.Extensions;

namespace E_Signature
{
    public static class Signature
    {
        private static  readonly TimeSpan _defaultTimeRounding = TimeSpan.FromMilliseconds(50);
        public static TimeSpan TimeRounding { get; private set; }

        static Signature()
        {
            TimeRounding = _defaultTimeRounding;
        }

        public static void Configure(TimeSpan timeRounding)
        {
            TimeRounding = timeRounding;
        }

        public static string GetSing(string inputBody, string secretKey)
        {
            if (!string.IsNullOrEmpty(inputBody) && !string.IsNullOrEmpty(secretKey))
            {
                var result = GetSing(inputBody, secretKey, DateTime.Now);

                return result;
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

        private static string GetSing(string inputBody, string secretKey, DateTime time)
        {
            if (!string.IsNullOrEmpty(inputBody) && !string.IsNullOrEmpty(secretKey))
            {
                var timeNow = time.Round(TimeRounding);
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

        public static bool ConfirmSing(string inputBody, string inputSing, string secretKey, TimeSpan timeDrift)
        {
            if (!string.IsNullOrEmpty(inputBody) && !string.IsNullOrEmpty(secretKey) && !string.IsNullOrEmpty(inputSing) && timeDrift != default)
            {
                var result = ConfirmSing(inputBody, inputSing, secretKey, timeDrift, DateTime.Now);

                return result;
            }
            else if (string.IsNullOrEmpty(inputBody))
            {
                throw new ArgumentException("String inputBody is empty or null ");
            }
            else if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentException("String secretKey is empty or null ");
            }
            else if (string.IsNullOrEmpty(inputSing))
            {
                throw new ArgumentException("String inputSing is empty or null ");
            }
            else
            {
                throw new ArgumentException("Time span is default");
            }
        }

        public static bool ConfirmSing(string inputBody, string inputSing, string secretKey, TimeSpan timeDrift, DateTime targetTime)
        {
            if (!string.IsNullOrEmpty(inputBody) && !string.IsNullOrEmpty(secretKey) && !string.IsNullOrEmpty(inputSing) && timeDrift != default)
            {
                var result = false;
                var startTime = targetTime - timeDrift;
                var finishTime = targetTime + timeDrift;

                for (var i = startTime; i <= finishTime; i += TimeRounding)
                {
                    var sing = GetSing(inputBody, secretKey, i);

                    if (sing == inputSing)
                    {
                        result = true;

                        break;
                    }
                }

                return result;
            }
            else if (string.IsNullOrEmpty(inputBody))
            {
                throw new ArgumentException("String inputBody is empty or null ");
            }
            else if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentException("String secretKey is empty or null ");
            }
            else if (string.IsNullOrEmpty(inputSing))
            {
                throw new ArgumentException("String inputSing is empty or null ");
            }
            else
            {
                throw new ArgumentException("Time span is default");
            }
        }
    }
}
