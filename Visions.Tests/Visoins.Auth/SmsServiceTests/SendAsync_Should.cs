using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Visions.Auth;

namespace Visions.Tests.Visoins.Auth.SmsServiceTests
{
    [TestFixture]
    public class SendAsync_Should
    {
        [Test]
        public void ReturnTaskFromResult()
        {
            // Arrange
            IIdentityMessageService smsService = new SmsService();

            // Act
            Task returnedTask = smsService.SendAsync(It.IsAny<IdentityMessage>());

            // Assert
            Assert.IsNotNull(returnedTask);
        }
    }
}
