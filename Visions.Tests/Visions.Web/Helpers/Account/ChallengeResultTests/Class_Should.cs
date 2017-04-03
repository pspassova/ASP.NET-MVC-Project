using NUnit.Framework;
using Visions.Web.Helpers.Account;

namespace Visions.Tests.Visions.Web.Helpers.Account.ChallengeResultTests
{
    [TestFixture]
    public class Class_Should
    {
        [Test]
        public void SetLoginProviderPropertyCorrectly_WhenItIsProvided()
        {
            // Arrange
            string provider = "test provider";
            string redirectUri = "test";

            // Act
            ChallengeResult challengeResult = new ChallengeResult(provider, redirectUri);

            // Assert
            Assert.AreSame(provider, challengeResult.LoginProvider);
        }

        [Test]
        public void SetRedirectUriPropertyCorrectly_WhenItIsProvided()
        {
            // Arrange
            string provider = "test";
            string redirectUri = "test redirectUri";

            // Act
            ChallengeResult challengeResult = new ChallengeResult(provider, redirectUri);

            // Assert
            Assert.AreSame(redirectUri, challengeResult.RedirectUri);
        }

        [Test]
        public void SetUserIdPropertyCorrectly_WhenItIsProvided()
        {
            // Arrange
            string provider = "test";
            string redirectUri = "test";
            string userId = "test";

            // Act
            ChallengeResult challengeResult = new ChallengeResult(provider, redirectUri, userId);

            // Assert
            Assert.AreSame(userId, challengeResult.UserId);
        }
    }
}
