using System;
using E_Signature.Tests.Sources;
using System.Collections;
using NUnit.Framework;
using FluentAssertions;

namespace E_Signature.Tests
{
    public class Tests
    {
        [TestCaseSource(typeof(SingSources), nameof(SingSources.ValidCasesForGetSingMethod))]
        public void GetSing_WhenValidTestPassed_ShouldReturnSing(string actualJSON, string secretKey, string expectedJson)
        {
            Signature.Configure(TimeSpan.FromSeconds(1));

            var actualSing = Signature.GetSing(actualJSON,secretKey);

            var expectedSing = Signature.GetSing(expectedJson, secretKey);

            actualSing.Should().Be(expectedSing);
        }

        [TestCaseSource(typeof(SingSources), nameof(SingSources.ValidCasesForIsValidSingMethod))]
        public void IsValidSing_WhenValidTestPassed_ShouldReturnBool(string inputJson, string secretKey, TimeSpan timeDrift, bool expected)
        {
            Signature.Configure(TimeSpan.FromMilliseconds(10));
            var inputSing = Signature.GetSing(inputJson, secretKey);
            
            var actual = Signature.ConfirmSing(inputJson, inputSing, secretKey, timeDrift);

            Assert.AreEqual(expected, actual);
        }
    }
}