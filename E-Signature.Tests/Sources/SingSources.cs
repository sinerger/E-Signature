using System;
using System.Collections.Generic;

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

        public static object[] ValidCasesForConfirmSingMethod =
        {
            new object[]
            {
                _actualJSON,
                _secretKey,
                TimeSpan.FromMinutes(1),
                true
            }
        };

        public static object[] InvalidCaseWhenBodyIsNullOrEmptyForGetSingMethod =
        {
            new object[]
            {
                null,
                _secretKey
            },
            new object[]
            {
                String.Empty,
                _secretKey
            }
        };

        public static object[] InvalidCaseWhenSecretKeyIsNullOrEmptyForForGetSingMethod =
        {
            new object[]
            {
                _actualJSON,
                null
            },
            new object[]
            {
                _actualJSON,
                String.Empty

            }
        };

        public static object[] InvalidCasesWhenBodyIsNullOrEmptyForConfirmSingMethod =
        {
            new object[]
            {
                null,
                _secretKey,
                TimeSpan.FromMinutes(1)
            },
            new object[]
            {
                String.Empty,
                _secretKey,
                TimeSpan.FromMinutes(1)
            }
        };

        public static object[] InvalidCasesWhenSecretKeyIsNullOrEmptyForConfirmSingMethod =
        {
            new object[]
            {
                _actualJSON,
                null,
                TimeSpan.FromMinutes(1)
            },
            new object[]
            {
                _actualJSON,
                String.Empty,
                TimeSpan.FromMinutes(1)
            }
        };

        public static object[] InvalidCasesWhenInputSingIsNullOrEmptyForConfirmSingMethod =
        {
            new object[]
            {
                _actualJSON,
                _secretKey,
                TimeSpan.FromMinutes(1),
                null
            },
            new object[]
            {
                _actualJSON,
                _secretKey,
                TimeSpan.FromMinutes(1),
                String.Empty
            }
        };

        public static object[] InvalidCasesWhenTimeDriftHasDefaultValueForConfirmSingMethod =
        {
            new object[]
            {
                _actualJSON,
                _secretKey,
                default
            }
        };
    }
}
