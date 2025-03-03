
namespace BarcoVwM.UnitTests
{
    [TestFixture]
    public class Tests
    {
        private BarcoVideoWallManager.Barco _vwm;
        private const string Ip = "439d9c21-93d9-4665-8d82-6353c8a13cce.mock.pstmn.io";
        private const string Psk = "jrYRtDchsg1xudxH";

        [SetUp]
        public void Setup()
        {
            _vwm = new BarcoVideoWallManager.Barco(Ip, Psk, true);

        }

        [Test]
        public async Task AuthenticationAsync_ReturnsTrue_OnSuccess()
        {
            bool authResult = await _vwm.AuthenticateAsync();
            Assert.IsTrue(authResult, "Authentication should succeed against the postman mock server");
        }
    }
}