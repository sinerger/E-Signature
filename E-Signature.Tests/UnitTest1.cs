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

        [TestCaseSource(typeof(SingSources), nameof(SingSources.InvalidCaseWhenBodyIsNullOrEmptyForGetSingMethod))]
        public void GetSing_WhenTestIsNotValid_ShouldGenerateArgumentNullExceptionForBody(string actualJSON, string secretKey)
        {
            Action act =()=> Signature.GetSing(actualJSON, secretKey);

            act.Should().Throw<ArgumentNullException>();
        }

        [TestCaseSource(typeof(SingSources), nameof(SingSources.InvalidCaseWhenSecretKeyIsNullOrEmptyForForGetSingMethod))]
        public void GetSing_WhenTestIsNotValid_ShouldGenerateArgumentNullExceptionForSecretKey(string actualJSON, string secretKey)
        {
            Action act = () => Signature.GetSing(actualJSON, secretKey);

            act.Should().Throw<ArgumentNullException>();
        }

        [TestCaseSource(typeof(SingSources), nameof(SingSources.ValidCasesForConfirmSingMethod))]
        public void ConfirmSing_WhenValidTestPassed_ShouldReturnBool(string inputJson, string secretKey, TimeSpan timeDrift, bool expected)
        {
            Signature.Configure(TimeSpan.FromMilliseconds(10));
            var inputSing = Signature.GetSing(inputJson, secretKey);
            
            var actual = Signature.ConfirmSing(inputJson, inputSing, secretKey, timeDrift);

            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(typeof(SingSources), nameof(SingSources.InvalidCasesWhenBodyIsNullOrEmptyForConfirmSingMethod))]
        public void ConfirmSing_WhenTestIsNotValid_ShouldGenerateArgumentExceptionForBody(string inputJson, string secretKey, TimeSpan timeDrift)
        {
            Signature.Configure(TimeSpan.FromMilliseconds(10));
            var inputSing = Signature.GetSing("{\"name\":\"john\",\"age\":22,\"class\":\"mca\"}", secretKey);

            Action act = () => Signature.ConfirmSing(inputJson, inputSing, secretKey, timeDrift);

            act.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(typeof(SingSources), nameof(SingSources.InvalidCasesWhenSecretKeyIsNullOrEmptyForConfirmSingMethod))]
        public void ConfirmSing_WhenTestIsNotValid_ShouldGenerateArgumentExceptionForSecretKey(string inputJson, string secretKey, TimeSpan timeDrift)
        {
            Signature.Configure(TimeSpan.FromMilliseconds(10));
            var inputSing = Signature.GetSing(inputJson, "Very secret super string");

            Action act = () => Signature.ConfirmSing(inputJson, inputSing, secretKey, timeDrift);

            act.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(typeof(SingSources), nameof(SingSources.InvalidCasesWhenInputSingIsNullOrEmptyForConfirmSingMethod))]
        public void ConfirmSing_WhenTestIsNotValid_ShouldGenerateArgumentExceptionForInputSing(string inputJson, string secretKey, TimeSpan timeDrift, string inputString)
        {
            Signature.Configure(TimeSpan.FromMilliseconds(10));

            Action act = () => Signature.ConfirmSing(inputJson, inputString, secretKey, timeDrift);

            act.Should().Throw<ArgumentException>();
        }

        [TestCaseSource(typeof(SingSources), nameof(SingSources.InvalidCasesWhenTimeDriftHasDefaultValueForConfirmSingMethod))]
        public void ConfirmSing_WhenTestIsNotValid_ShouldGenerateArgumentExceptionForTimeDrift(string inputJson, string secretKey, TimeSpan timeDrift)
        {
            Signature.Configure(TimeSpan.FromMilliseconds(10));
            var inputSing = Signature.GetSing(inputJson, "Very secret super string");

            Action act = () => Signature.ConfirmSing(inputJson, inputSing, secretKey, timeDrift);

            act.Should().Throw<ArgumentException>();
        }
    }
}