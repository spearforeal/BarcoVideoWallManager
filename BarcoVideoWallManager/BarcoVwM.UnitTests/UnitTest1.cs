
using BarcoVideoWallManager;

namespace BarcoVwM.UnitTests
{
    [TestFixture]
    public class Tests
    {
        private BarcoVideoWallManager.Barco _vwm;
        private const string Ip = "439d9c21-93d9-4665-8d82-6353c8a13cce.mock.pstmn.io";
        private const string Psk = "jrYRtDchsg1xudxH";

        [OneTimeSetUp]
        public async Task OneTimeSetup()
        {
            _vwm = new Barco(Ip, Psk, true);
            var authResult = await _vwm.AuthenticateAsync();
            Assert.IsTrue(authResult, "Authentication must succeed in OneTimeSetup");
        }
        [Test]
        public async Task AuthenticationAsync_ReturnsTrue_OnSuccess()
        {
            var authResult = await _vwm.AuthenticateAsync();
            Assert.That(authResult, Is.True, "Authentication should succeed against the postman mock server");
        }
        [Test]
        public async Task GetWallBrightnessAsync_ReturnsTrue_WhenResponseIsValid()
        {
            var result = await _vwm.GetWallBrightnessAsync();
            Assert.That(result, Is.True, "GetWallBrightnessAsync should return true for a valid brightness response");
        }
        [Test]
        public async Task AuthThenGetBrightness_Succeeds()
        {
            var authResult = await _vwm.AuthenticateAsync();
            Assert.IsTrue(authResult, "Auth should succeed");
            var brightnessResult = await _vwm.GetWallBrightnessAsync();
            Assert.IsTrue(brightnessResult, "Brightness call should succeed with a valid cookie");
        }

        [Test]
        public async Task GetVwMVersion_ReturnsTrue_WhenResponseIsValid()
        {
            var result = await _vwm.GetVwMVersionAsync();
            Assert.That(result, Is.True, "GetVwMVersionAsync should return true for a valid response");
            
        }
        [Test]
        public async Task GetApiVersion_ReturnsTrue_WhenResponseIsValid()
        {
            var result = await _vwm.GetApiVersionAsync();
            Assert.That(result, Is.True, "GetApiVersionAsync should return true for a valid response");
            
        }
    }
}