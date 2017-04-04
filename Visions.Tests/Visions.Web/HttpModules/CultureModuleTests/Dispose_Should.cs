using NUnit.Framework;
using Visions.Web.HttpModules;

namespace Visions.Tests.Visions.Web.HttpModules.CultureModuleTests
{
    [TestFixture]
    public class Dispose_Should
    {
        [Test]
        public void BeAMethodInCultureModuleClass()
        {
            bool isDisposed = false;
            using (new CultureModule())
            {
                isDisposed = true;
            }

            Assert.IsTrue(isDisposed);
        }
    }
}
