using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Celerik.NetCore.Util.Test
{
    [TestClass]
    public class BaseTestTest : BaseTest
    {
        [TestMethod]
        public void SetUserlaims()
        {
            var claimKey = "UserName";
            var claimValue = "René Higuita";
            SetUserClaims(claimKey, claimValue);

            var httpContextAccesor = GetService<IHttpContextAccessor>();
            var httpContext = httpContextAccesor.HttpContext;
            var claimObj = httpContext.User.Claims.FirstOrDefault(c => c.Type == claimKey);
            var claimStr = JsonConvert.DeserializeObject<string>(claimObj.Value);

            Assert.AreEqual(claimValue, claimStr);
        }
    }
}
