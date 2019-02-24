using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DocDocos;
using System.Xml;
using System.Xml.Linq;


namespace DocDocosTest
{
    /// <summary>
    /// Tester ;a transformation du
    /// fragment xml en entite
    /// de document
    /// </summary>
    [TestClass]
    public class TraiterEntiteTest
    {
        [TestMethod]
        public void ParametresParXMLTest()
        {
            #region Initialisations
            DocDocosDA Generateur = new DocDocosDA();
            EntiteDocument monEntite = new EntiteDocument();

            string innerxmlTest = "<member name=\"M:Unittestnamespace.IO.AjouterCheminFichier(System.String, System.String)\">" +
                Environment.NewLine +
              "<summary>" +
             
              "Ajoute un autre niveau à un path" +
              " de fichier" +
            "</summary>" + Environment.NewLine +
            "<param name=\"Chemin\">Chemin du répertoire auquel on ajoute un niveau.</param>" + Environment.NewLine +
            "<param name=\"Ajout\">Niveau à ajouter</param>" + Environment.NewLine +
            "<returns>Le chemin avec l'ajout avec le séparateur correct pour le système d'exploitation</returns>" + Environment.NewLine +
            "</member> "; 

            XElement  XMLTest = 
                 XElement.Parse(innerxmlTest);
            #endregion Initialisations

            monEntite = 
                Generateur.TraiterNoeud(XMLTest);

            Assert.AreEqual(
                "Ajoute un autre niveau à un path de fichier",
                monEntite.Sommaire );
            Assert.AreEqual("Le chemin avec l'ajout avec le séparateur correct pour le système d'exploitation",
                monEntite.Retour);

            Assert.AreEqual(2,
                monEntite.Parametres.Count);
            string Dbug = "";

            // extraire toutes les clé  du dictionnaire
            // pour les afficher dans le premier est
            // en cas d'échec
            foreach (var k in monEntite.Parametres.Keys)
            {
                Dbug += k.ToString() + ": ";
            }

            Assert.IsTrue(monEntite.Parametres
                .ContainsKey("Chemin"),
               Dbug);

            Assert.IsTrue(monEntite.Parametres
               .ContainsKey("Ajout"),
              Dbug);

            Assert.AreEqual("Chemin du répertoire auquel on ajoute un niveau.",
                monEntite.Parametres["Chemin"]);

            Assert.AreEqual("Niveau à ajouter",
                monEntite.Parametres["Ajout"]);


        }


    }
}
