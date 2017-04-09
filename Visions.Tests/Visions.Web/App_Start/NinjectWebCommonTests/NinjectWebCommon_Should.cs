using Ninject;
using NUnit.Framework;
using Visions.Web.App_Start;

namespace Visions.Tests.Visions.Web.App_Start.NinjectWebCommonTests
{
    [TestFixture]
    public class NinjectWebCommon_Should
    {
        [Test]
        public void CreateAnInstanceOfIKernel()
        {
            // Arrange, Act, Assert
            Assert.IsInstanceOf<IKernel>(NinjectWebCommon.CreateKernel());
        }
    }
}
