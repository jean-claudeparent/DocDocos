using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocDocos;
using System;

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

            string Reponse = Environment.NewLine +
                "<tr>" + Environment.NewLine +
                "   <td>Une colonne 3&lt;&gt;5 ne pas enlever:{{ContenuCellule}}</td>" +
            Environment.NewLine +
            "</tr>" + Environment.NewLine   ;   

            Assert.AreEqual(
                Reponse,
                H.CreerRangee("Une colonne 3<>5 ne pas enlever:" +
                HTMLHelper.Variables()[1])); 
        }


    }
}
