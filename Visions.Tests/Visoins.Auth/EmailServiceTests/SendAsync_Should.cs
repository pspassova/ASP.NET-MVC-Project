using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Visions.Auth;

namespace Visions.Tests.Visoins.Auth.EmailServiceTests
{
    [TestFixture]
    public class SendAsync_Should
    {
        [Test]
        public void ReturnTaskFromResult()
        {
            // Arrange
            IIdentityMessageService emailService = new EmailService();

            // Act
            Task returnedTask = emailService.SendAsync(It.IsAny<IdentityMessage>());

            // Assert
            Assert.IsNotNull(returnedTask);
        }
    }
}
