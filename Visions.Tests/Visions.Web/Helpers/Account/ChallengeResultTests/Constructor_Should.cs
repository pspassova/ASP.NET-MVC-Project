using NUnit.Framework;
using System;
using Visions.Web.Helpers.Account;

namespace Visions.Tests.Visions.Web.Helpers.Account.ChallengeResultTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenProviderIsNotPassed()
        {
            // Arrange
            string redirectUri = "test";
            string userId = "test";

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ChallengeResult(null, redirectUri, userId));
        }

        [Test]
        public void ThrowArgumentNullException_WhenRedirectUriIsNotPassed()
        {
            // Arrange
            string provider = "test";
            string userId = "test";

            // Act, Assert
            Assert.Throws<ArgumentNullException>(() => new ChallengeResult(provider, null, userId));
        }

        [Test]
        public void CreateAnInstanceOfChallengeResult_WhenProviderAndRedirectUriArePassed()
        {
            // Arrange
            string provider = "test";
            string redirectUri = "test";

            // Act, Assert
            Assert.IsInstanceOf<ChallengeResult>(new ChallengeResult(provider, redirectUri));
        }

        [Test]
        public void CreateAnInstanceOfChallengeResult_WhenAllOfTheParametersArePassed()
        {
            // Arrange
            string provider = "test";
            string redirectUri = "test";
            string userId = "test";

            // Act, Assert
            Assert.IsInstanceOf<ChallengeResult>(new ChallengeResult(provider, redirectUri, userId));
        }
    }
}
