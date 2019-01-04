using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocDocos;

namespace DocDocosTest
{
    [TestClass]
    public class HTMLHelperTest
    {
        [TestMethod]
        public void RangeeTest()
        {
            HTMLHelper H =
                new HTMLHelper();
            Assert.AreEqual(
                "<tr><td>Une colonne 3&lt;&gt;5</td></tr>",
                H.CreerRangee("Une colonne 3<>5")); 
        }


    }
}
