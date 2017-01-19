using System.Threading.Tasks;
using NUnit.Framework;
using TinCan.xAPIWrapper;

namespace xAPIWrapper.Tests.iOS
{
    [TestFixture]
    public class TestClass
    {
        /// <summary>
        /// The LRS
        /// </summary>
        private APIWrapper _xAPIWrapper;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [SetUp]
        public void Init()
        {
            _xAPIWrapper = new APIWrapper(string.Empty, string.Empty, string.Empty);
        }
        /// <summary>
        /// Tests the about.
        /// </summary>
        [Test]
        public async Task TestAbout()
        {
            var lrsRes = await _xAPIWrapper.About();
            Assert.IsTrue(lrsRes.Success);
        }

    }
}
