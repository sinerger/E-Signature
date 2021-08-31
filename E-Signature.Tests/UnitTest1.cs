using System;
using System.Collections;
using NUnit.Framework;
using FluentAssertions;

namespace E_Signature.Tests
{
    public class Tests
    {
        [TestCaseSource(typeof(GetDataForTestGetSing))]
        public void GetSing_WhenValidTestPassed_ShouldNotHaveSameSing(string actualJSON, string secretKey, string expectedJson)
        {
            Signature.Configure(TimeSpan.FromMilliseconds(50));

            var actualSing = Signature.GetSing(actualJSON,secretKey);

            var expectedSing = Signature.GetSing(expectedJson, secretKey);

            actualSing.Should().NotBe(expectedSing);
        }

        public class GetDataForTestGetSing : IEnumerable
        {
            private string actualJSON = "{\"name\":\"john\",\"age\":22,\"class\":\"mca\"}";
            private string secretKey = "Very secrete key";
            private string expectedJson = "{\"name\":\"john\",\"age\":22,\"class\":\"mca\"}";

            public IEnumerator GetEnumerator()
            {


                yield return new object[]
                {
                    actualJSON,
                    secretKey,
                    expectedJson
                };


            }
        }
        public class GetDataForTestGetSing2 : IEnumerable
        {
            private string actualJSON = "{\"name\":\"john\",\"age\":22,\"class\":\"mca\"}";
            private string secretKey = "Very secrete key";
            private string expectedJson = "{\"name\":\"john\",\"age\":22,\"class\":\"mca\"}";

            public IEnumerator GetEnumerator()
            {


                yield return new object[]
                {
                    actualJSON,
                    secretKey,
                    TimeSpan.FromMilliseconds(1),
                    true
                };


            }
        }
        [TestCaseSource(typeof(GetDataForTestGetSing2))]
        public void IsValidSing_WhenValidTestPassed_ShouldReturnBool(string inputJson, string secretKey, TimeSpan timeDrift, bool expected)
        {
            Signature.Configure(TimeSpan.FromMilliseconds(10));
            var inputSing = Signature.GetSing(inputJson, secretKey);
            
            var actual = Signature.IsValidSing(inputJson, inputSing, secretKey, timeDrift);

            Assert.AreEqual(expected, actual);
        }
    }
}