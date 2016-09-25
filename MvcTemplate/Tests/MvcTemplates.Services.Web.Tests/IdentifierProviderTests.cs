namespace MvcTemplates.Services.Web.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using MvcTemplate.Services.Web;

    [TestClass]
    public class IdentifierProviderTests
    {
        [TestMethod]
        public void EncodingAndDecodingDoesntChangeTheId()
        {
            const int Id = 1337;
            IIdentifierProvider provider = new IdentifierProvider();
            var encodet = provider.EncodeId(Id);
            var actual = provider.DecodeId(encodet);

            Assert.AreEqual(Id, actual);
        }
    }
}
