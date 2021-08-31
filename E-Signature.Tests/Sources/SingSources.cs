using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace E_Signature.Tests.Sources
{
    public class SingSources
    {
        private static string _actualJSON;
        private static string _secretKey;
        private static string _expectedJson;

        public static object[] ValidCasesForGetSingMethod =
        {
            new object[]
            {
                _actualJSON,
                _secretKey,
                _expectedJson
            }
        };

        static SingSources()
        {
            _actualJSON =
                "{\"name\":\"john\",\"age\":22,\"class\":\"mca\"}";
            _expectedJson = _actualJSON;
            _secretKey = "Very secret super string";
        }
    }
}
