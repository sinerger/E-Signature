using System;
using E_Signature.Extensions;

namespace E_Signature
{
    public static class Signature
    {
        private static readonly TimeSpan _defaultTimeRounding = TimeSpan.FromMilliseconds(50);
        public static TimeSpan TimeRounding { get; private set; }

        static Signature()
        {
            TimeRounding = _defaultTimeRounding;
        }

        public static void Configure(TimeSpan timeRounding)
        {
            TimeRounding = timeRounding;
        }

        /// <summary>
        /// This method creates a signature for current time
        /// </summary>
        /// <param name="inputBody">The body of the document </param>
        /// <param name="secretKey">Secret Key</param>
        /// <exception cref="System.ArgumentNullException">Thrown when inputBody or secretKey is null oe empty.</exception>
        /// <returns>Return signature like a string</returns>
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

        /// <summary>
        /// This method checks if signature can be created for the current time
        /// </summary>
        /// <param name="inputBody">The body of the document which comes like a JSON</param>
        /// <param name="secretKey">Secret Key</param>
        /// <param name="sing">Document signature</param>
        /// <returns>Return true if signature was created and false if it stayed empty</returns>
        public static bool TryGetSing(string inputBody, string secretKey, out string sing)
        {
            var result = false;

            if (!string.IsNullOrEmpty(inputBody) && !string.IsNullOrEmpty(secretKey))
            {
                sing = GetSing(inputBody, secretKey, DateTime.Now);
                result = true;
            }
            else
            {
                sing = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// This method creates a signature
        /// </summary>
        /// <param name="inputBody">The body of the document which comes like a JSON</param>
        /// <param name="secretKey">Secret Key</param>
        /// <param name="time">Time when the document was signed</param>
        /// <exception cref="System.ArgumentNullException">Thrown when inputBody or secretKey is null or empty and time has default value.</exception>
        /// <returns>Return signature like a string</returns>
        public static string GetSing(string inputBody, string secretKey, DateTime time)
        {
            if (!string.IsNullOrEmpty(inputBody) && !string.IsNullOrEmpty(secretKey) && time != default)
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
            else if (time == default)
            {
                throw new ArgumentNullException("Date time is default");
            }
            else
            {
                throw new ArgumentNullException("String secretKey is empty or null");
            }
        }

        /// <summary>
        /// This method checks if signature can be created
        /// </summary>
        /// <param name="inputBody">The body of the document which comes like a JSON</param>
        /// <param name="secretKey">Secret Key</param>
        /// <param name="time">Time when the document was signed</param>
        /// <param name="sing">Document signature </param>
        /// <returns>Return true if signature was created and false if it stayed empty</returns>
        public static bool TryGetSing(string inputBody, string secretKey, DateTime time, out string sing)
        {
            var result = false;

            if (!string.IsNullOrEmpty(inputBody) && !string.IsNullOrEmpty(secretKey) && time != default)
            {
                sing = GetSing(inputBody, secretKey, time);
                result = true;
            }
            else
            {
                sing = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// This method checks if the document is authentic for the current time
        /// </summary>
        /// <param name="inputBody">The body of the document which comes like a JSON</param>
        /// <param name="inputSing"> The signature that was received from the client</param>
        /// <param name="secretKey">Secret Key</param>
        /// <param name="timeDrift">Time gap concerning to current time</param>
        /// <exception cref="ArgumentException">Thrown when inputBody, inputSing or secretKey is null or empty and timeDrift has default value .</exception>
        /// <returns>Return true if signature was confirm and false if signature was wrong</returns>
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

        /// <summary>
        /// This method checks if the document signature can be confirmed
        /// </summary>
        /// <param name="inputBody">The body of the document which comes like a JSON</param>
        /// <param name="inputSing"> The signature that was received from the client</param>
        /// <param name="secretKey">Secret Key</param>
        /// <param name="timeDrift">Time gap concerning to current time</param>
        /// <returns>Return true if signature can be confirm, otherwise false</returns>
        public static bool TryConfirmSing(string inputBody, string inputSing, string secretKey, TimeSpan timeDrift)
        {
            var result = false;

            if (!string.IsNullOrEmpty(inputBody) && !string.IsNullOrEmpty(secretKey) && !string.IsNullOrEmpty(inputSing) && timeDrift != default)
            {
                result = ConfirmSing(inputBody, inputSing, secretKey, timeDrift, DateTime.Now);
            }

            return result;
        }

        /// <summary>
        /// This method checks if the document is authentic
        /// </summary>
        /// <param name="inputBody">The body of the document which comes like a JSON</param>
        /// <param name="inputSing"> The signature that was received from the client</param>
        /// <param name="secretKey">Secret Key</param>
        /// <param name="timeDrift">Time gap concerning to current time</param>
        /// <param name="targetTime">Time when signature was created</param>
        /// <exception cref="ArgumentException">Thrown when inputBody, inputSing or secretKey is null or empty and timeDrift or targetTime has default value .</exception>
        /// <returns>Return true if signature was confirm and false if signature was wrong</returns>
        public static bool ConfirmSing(string inputBody, string inputSing, string secretKey, TimeSpan timeDrift, DateTime targetTime)
        {
            if (!string.IsNullOrEmpty(inputBody) && !string.IsNullOrEmpty(secretKey) && !string.IsNullOrEmpty(inputSing) && timeDrift != default && targetTime != default)
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
            else if (targetTime == default)
            {
                throw new ArgumentException("DateTime targetTime is default");
            }
            else
            {
                throw new ArgumentException("Time span is default");
            }
        }

        /// <summary>
        /// This method checks if the document signature can be confirmed
        /// </summary>
        /// <param name="inputBody">The body of the document which comes like a JSON</param>
        /// <param name="inputSing"> The signature that was received from the client</param>
        /// <param name="secretKey">Secret Key</param>
        /// <param name="timeDrift">Time gap concerning to current time</param>
        /// <param name="targetTime">Time when signature was created</param>
        /// <returns>Return true if signature can be confirm, otherwise false</returns>
        public static bool TryConfirmSing(string inputBody, string inputSing, string secretKey, TimeSpan timeDrift, DateTime targetTime)
        {
            var result = false;

            if (!string.IsNullOrEmpty(inputBody) && !string.IsNullOrEmpty(secretKey) && !string.IsNullOrEmpty(inputSing) && timeDrift != default && targetTime != default)
            {
                result = ConfirmSing(inputBody, inputSing, secretKey, timeDrift, targetTime);
            }

            return result;
        }
    }
}
