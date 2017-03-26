using Moq;
using NUnit.Framework;
using System.Web;
using Visions.Helpers;
using Visions.Helpers.Contracts;

namespace Visions.Tests.Visions.Helpers.PhotoHelperTests
{
    [TestFixture]
    public class UploadToFileSystem_Should
    {
        [Test]
        public void InvokeSaveAsMethod_Once_WhenBothParametersAreProvided()
        {
            // Arrange
            string physicalPath = @"c:\work\app_data";

            var fileMock = new Mock<HttpPostedFileBase>();

            IPhotoHelper photoHelper = new PhotoHelper();

            // Act
            photoHelper.UploadToFileSystem(fileMock.Object, physicalPath);

            // Assert
            fileMock.Verify(x => x.SaveAs(It.IsAny<string>()), Times.Once);
        }
    }
}
