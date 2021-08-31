using System;

namespace E_Signature.Tests.Sources
{
    public class SingSources
    {
        private static string _actualJSON = "{\"name\":\"john\",\"age\":22,\"class\":\"mca\"}";
        private static string _secretKey = "Very secret super string";
        private static string _expectedJson = _actualJSON;

        public static object[] ValidCasesForGetSingMethod =
        {
            new object[]
            {
                _actualJSON,
                _secretKey,
                _expectedJson
            }
        };

        public static object[] ValidCasesForIsValidSingMethod =
        {
            new object[]
            {
                _actualJSON,
                _secretKey,
                TimeSpan.FromMinutes(1),
                true
            }
        };
    }
}
