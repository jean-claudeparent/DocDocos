using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocDocos;
using System.Xml;
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;


namespace MokaDocosTest
{
    [TestClass]
    public class UGenerer
    {
        [TestMethod]
        public void GenererHTMLPetit()
        {
            IO monIO = new IO();
            DocDocosDA monGenerateur =
                new DocDocosDA();

            monGenerateur.RepertoireSortie = monIO.RepertoireAssembly();
            monGenerateur.RepertoireSortie = monIO.AjouterCheminFichier(
                monGenerateur.RepertoireSortie,
                "Ressources");

            monGenerateur.RepertoireSortie = monIO.AjouterCheminFichier(
                monGenerateur.RepertoireSortie,
                "SiteWeb");
            monGenerateur.RepertoireSortie = monIO.AjouterCheminFichier(
                monGenerateur.RepertoireSortie,
                "JCUtilitaires");
            string Temp = monIO.AjouterCheminFichier(
                monIO.RepertoireAssembly(),
                "Ressources");
            monGenerateur.FichierXMLDoc = monIO.AjouterCheminFichier(
            Temp, "JCUtilitaires.xml");
            monGenerateur.GenererHTML();

            string FR = monIO.AjouterCheminFichier( 
                    monGenerateur.RepertoireSortie,
                    "JCUtilitaires.IO.AjouterCheminFichier1.html");
                

            Assert.IsTrue(
                File.Exists(FR),
                "fichier inexistant " +
                FR);

            String Resultat = 
                File.ReadAllText(FR);
            Assert.IsFalse(Resultat.Contains(
                "{{Rangee}}"),
                "{{Rangee}} toujours visible"); 

            Assert.IsTrue(Resultat.Contains(
                "Ajoute un autre niveau à un path"),
                "erreur Ajoute un autre niveau à un path Valeur=" +
                Resultat);
            
            }

        [TestMethod]
        public void GenererHTMLGros()
        {
            IO monIO = new IO();
            string GabaritVersionne = monIO.AjouterCheminFichier(
                monIO.RepertoireAssembly(),
                "GabaritUnitTest.htm");  
            DocDocosDA monGenerateur = 
                new DocDocosDA(GabaritVersionne);
            monGenerateur.RepertoireSortie = monIO.RepertoireAssembly();
            monGenerateur.RepertoireSortie = monIO.AjouterCheminFichier(
                monGenerateur.RepertoireSortie,
                "Ressources");

            monGenerateur.RepertoireSortie = monIO.AjouterCheminFichier(
                monGenerateur.RepertoireSortie,
                "SiteWeb");
            monGenerateur.RepertoireSortie = monIO.AjouterCheminFichier(
                monGenerateur.RepertoireSortie,
                "JCAssertionnCore");
            if (Directory.Exists(monGenerateur.RepertoireSortie) )
                Directory.Delete(
                monGenerateur.RepertoireSortie,true); 
            string Temp = monIO.AjouterCheminFichier(
                monIO.RepertoireAssembly(),
                "Ressources");
            string repres =
                monGenerateur.RepertoireSortie + Path.DirectorySeparatorChar.ToString(); 
            monGenerateur.FichierXMLDoc = monIO.AjouterCheminFichier(
            Temp, "JCAssertionnCore.xml");
            monGenerateur.GenererHTML();
            string FichierCree = repres +
                "JCAssertionCore.htm";

            Assert.IsTrue(File.Exists(FichierCree),
                "Il manque le fichier " +
                FichierCree);
            Assert.IsFalse(File.ReadAllText(FichierCree).Contains(
                "Gabarit spécifié par le unit test"),
                "Le commentaire de version de gabarit est absent "); 


        }


        [TestMethod]
        public void ComvertirNoeud()
        {
            DocDocosDA monGenerateur = new DocDocosDA();
            
            // cas qui marche
            XElement monXML = new XElement
                ("member",
                new XAttribute("name",
                "namespace.testniveau1.niveau2.niveau3.niveau4.niveau5(System.String,System.String)"),
                new XElement("summary", "Ceci est le sommaire."));

            EntiteDocument monED = 
                monGenerateur.TraiterNoeud(monXML);
            Assert.AreEqual("namespace",
                monED.Namespace);
            Assert.AreEqual("namespace",
                monED.Niveau(0) );

            Assert.AreEqual("testniveau1",
                            monED.Niveau(1));
            Assert.AreEqual("niveau2",
                 monED.Niveau(2)) ;
            Assert.AreEqual("niveau3",
                 monED.Niveau (3));
            Assert.AreEqual("niveau4",
                 monED.Niveau(4));
            Assert.AreEqual("niveau5",
                 monED.Niveau(5));
            Assert.AreEqual("",
                monED.Niveau(6));
            Assert.AreEqual("",
                monED.Niveau(7));

            Assert.AreEqual(6,
                monED.NombreNiveaux());
            Assert.AreEqual("Ceci est le sommaire.",
                monED.Sommaire );

        }



    } // class
}
