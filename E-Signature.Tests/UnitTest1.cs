using System;
using E_Signature.Tests.Sources;
using NUnit.Framework;

namespace E_Signature.Tests
{
    public class Tests
    {
        [TestCaseSource(typeof(SingSources), nameof(SingSources.ValidCasesForGetSingMethod))]
        public void GetSing_WhenValidTestPassed_ShouldReturnSing(string actualJSON, string secretKey, string expectedJson)
        {
            //var actualSing = Signature.GetSing(actualJSON,secretKey);

            //var expectedSing = Signature.GetSing(expectedJson, secretKey);

            Assert.Equals(expectedJson, actualJSON);
        }

        [Test]
        public void IsValidSing_WhenValidTestPassed_ShouldReturnBool(string inputJson, string secretKey, TimeSpan timeDrift, bool expected)
        {
            //var inputSing = Signature.GetSing(inputJson,secretKey);

            //var actual = Signature.IsValidSing(inputJson, inputSing, secretKey, timeDrift);

            //Assert.AreEqual(expected, actual);
        }
    }
}