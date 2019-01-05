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
                "   <td>Une colonne 3&amp;lt;&amp;gt;5 ne pas enlever:&#123;&#123;ContenuCellule&#125;&#125;</td>" +
            Environment.NewLine +
            "</tr>";   

            Assert.AreEqual(
                Reponse,
                H.CreerRangee("Une colonne 3<>5 ne pas enlever:" +
                HTMLHelper.Variables()[1]),
                Environment.NewLine + 
                "==>cas à une colonne");

            Reponse = Environment.NewLine +
                "<tr>" + Environment.NewLine +
                "   <td>Une colonne</td>" +
                Environment.NewLine  + 
                "   <td>Deuxième cellule</td>" +
                Environment.NewLine + 
                "   <td>Troisième cellule</td>" +
                Environment.NewLine +
                "</tr>";
            string Reel = H.CreerRangee("Une colonne",
                "Deuxième cellule",
                "Troisième cellule");

            Assert.AreEqual(
                Reponse,
                Reel,
                "==>cas à 3 colonneS=="+
                Reel );


        }


    }
}
