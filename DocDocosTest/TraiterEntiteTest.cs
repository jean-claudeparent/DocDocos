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
        public void ParametresTest()
        {
            DocDocosDA Generateur = new DocDocosDA();
            EntiteDocument monEntite = new EntiteDocument();

            string innerxmlTest = "<member name=\"M:Unittestnamespace.IO.AjouterCheminFichier(System.String, System.String)\">" +
                Environment.NewLine +
              "<summary>" +
              Environment.NewLine +
              "Ajoute un autre niveau à un path" +
              Environment.NewLine +
            "de fichier" + Environment.NewLine +
            "</summary>" + Environment.NewLine +
            "<param name=\"Chemin\">Chemin du répertoire auquel on ajoute un niveau.</param>" + Environment.NewLine +
            "<param name=\"Ajout\">Niveau à ajouter</param>" + Environment.NewLine +
            "<returns>Le chemin avec l'ajout avec le séparateur correct pour le système d'exploitation</returns>" + Environment.NewLine +
            "</member> ";

            XElement  XMLTest = 
                 XElement.Parse(innerxmlTest);
            monEntite = 
                Generateur.TraiterNoeud(XMLTest);

            Assert.AreEqual("erreur",
                monEntite.Sommaire );
            Assert.AreEqual("Le chemin avec l'ajout avec le séparateur correct pour le système d'exploitation",
                monEntite.Retour);
        }


    }
}
