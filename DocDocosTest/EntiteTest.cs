using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocDocos;


namespace MokaDocosTest
{
    [TestClass]
    public class EntiteTest
    {
        [TestMethod]
        public void TypeNomTest()
        {
            EntiteDocument E1 = new EntiteDocument();
            Assert.AreEqual(
                TypeEntiteDocument.Inconnu,
                E1.TypeEntite);
            E1.Nom = "";
            Assert.AreEqual(
                TypeEntiteDocument.Inconnu,
                E1.TypeEntite);
            E1.Nom = "LTest.montest.allo";
            Assert.AreEqual(
                TypeEntiteDocument.Inconnu,
                E1.TypeEntite);
            E1.Nom = "M:Test.montest.allo";
            Assert.AreEqual(
                TypeEntiteDocument.Classe,
                E1.TypeEntite);
            E1.Nom = "T:Test.montest.allo";
            Assert.AreEqual(
                TypeEntiteDocument.Propriete,
                E1.TypeEntite,
                "Test de propriete");
            Assert.AreEqual(
                "Test.montest.allo",
                E1.Nom );



        }

        [TestMethod]
        public void NiveauxTest()
        { 
            EntiteDocument E1 = 
                new EntiteDocument();
            E1.Nom = "M:MonNamespace.Niveau1.Niveau2.Niveau3.Niveau4";
            Assert.AreEqual(
                "MonNamespace.Niveau1.Niveau2.Niveau3.Niveau4",
                 E1.Nom);  
            Assert.AreEqual(
                "MonNamespace",
                E1.Namespace );
            Assert.AreEqual(
                "Niveau1",
                E1.Niveau (1));
            Assert.AreEqual(
                "Niveau2",
                E1.Niveau(2));
            Assert.AreEqual(
                "Niveau3",
                E1.Niveau(3));
            Assert.AreEqual(
                "Niveau4",
                E1.Niveau(4));
            Assert.AreEqual(
                "",
                E1.Niveau(5));
            Assert.AreEqual(
                "",
                E1.Niveau(6));
            
        }

        [TestMethod]
        public void NomParentTest()
        {
            EntiteDocument monTest =
                new EntiteDocument();
            monTest.Nom = 
                "M:Namesp.niv1.niv2.miv3.niv4";
            Assert.AreEqual(
                "Namesp.niv1.niv2.miv3.niv4",
                monTest.Nom);
            string Parent = monTest.NomParent();
            Assert.AreEqual(
                "Namesp.niv1.niv2.miv3",
                Parent);

            monTest.Nom = Parent;
            Parent = monTest.NomParent();
            Assert.AreEqual(
                "Namesp.niv1.niv2",
                Parent);

            monTest.Nom = Parent;
            Parent = monTest.NomParent();
            Assert.AreEqual(
                "Namesp.niv1",
                Parent);

            monTest.Nom = Parent;
            Parent = monTest.NomParent();
            Assert.AreEqual(
                "Namesp",
                Parent);

            monTest.Nom = Parent;
            Parent = monTest.NomParent();
            Assert.AreEqual(
                null,
                Parent);


        }

    } // class
}
