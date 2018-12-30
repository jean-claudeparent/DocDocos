﻿///<License>
/// Copyright (C) 2018 Jean-Claude Parent
/// License MIT voir MokaDocos-License.txt
///</License>
///

using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Xml;


namespace DocDocos
{
    /// <summary>
    /// Classe de logique d'affaire de MokaDocos.
    /// </summary>
    public class DocDocosDA
    {
       
        IO monIO = new IO();
        HTMLHelper HHêlper = new HTMLHelper();
         
        public  DocDocosDA()
        {
             string Chemin = 
                monIO.RepertoireAssembly();
            Chemin = monIO.AjouterCheminFichier(Chemin,
                                                "MokaDocos-gabarit.html");
            Gabarit = File.ReadAllText(Chemin);

        }

        /// <summary>
        /// Ce nombre remplacera une partie du nom
        /// d'entiteDocument pour donner des noms de fichiers 
        /// pluscourts.
        /// </summary>
        public Int32 RaccourciFichier = 1;

        public string FichierXMLDoc;
        public string RepertoireSortie;
        public string Gabarit;
        private XDocument Doc = new XDocument();
        public string NomNamespace;
        private   Dictionary<string, EntiteDocument> Dictionnaire =
            new Dictionary<string, EntiteDocument>();


        /// <summary>
        /// Charge le document XML de
        /// documentation généré par Visual Studio
        /// et génère le site web de documentation.
        /// </summary>
        public void GenererHTML()
        {
            
            AjouterToutDict();
            TraiterDictionnaire();
            PublierTout();
        }

        /// <summary>
        /// Construit une pageweb
        /// en inséran le contenu et le titre depage
        /// dans le gabarit HTML et écrite le
        /// résultat dans le fichier  
        /// de sortie.
        /// </summary>
        /// <param name="Fichier">Chemin complet du fichierhtml à écrire</param>
        /// <param name="Titre">Titre de la page web</param>
        /// <param name="Contenu">Contenu HTML à insérer dans la page</param>
        public void Publier(
            string Fichier,
            string Titre,
            string Contenu)
        {
            if (!Directory.Exists(RepertoireSortie))
                Directory.CreateDirectory(RepertoireSortie); 
            string Page = Gabarit.Replace("{{Titre}}", Titre);
            Page = Page.Replace("{{Contenu}}", Contenu);
            String PathComplet = monIO.AjouterCheminFichier(
                RepertoireSortie, 
                Fichier.Replace(":","_"));

            File.WriteAllText(
                PathComplet, Page);

        }

        /// <summary>
        /// Construit une EntiteDocument à
        /// partir d'un noeud xml
        /// </summary>
        /// <param name="Noeud">Noeud contenant l'information</param>
        /// <returns>L'entité document construite</returns>
        public EntiteDocument TraiterNoeud(
            XElement   NoeudATraiter)
        {
            string Temp = "";
            EntiteDocument Resultat =
                new EntiteDocument();
            if (NoeudATraiter != null )
            {
                try
                {
                    if ( (string )NoeudATraiter.Attribute("name")  == null)
                        throw new Exception("L'attribut 'name' est absent du xml ");
                    Temp = (string)NoeudATraiter.Attribute("name");

                    if (Temp != null)
                        Resultat.Nom = Temp;

                    if (string.IsNullOrEmpty(Resultat.Nom))
                        throw new Exception   ("Le nom n'as pas été trouvé");
                    Resultat.Sommaire =
                        (string )NoeudATraiter.Element(
                            "summary");
                
                } catch (Exception ex)
                {
                    throw new Exception("Le xml semble ne pas avoir la bonne structure :" +
                        Environment.NewLine +
                        NoeudATraiter.ToString () +
                        Environment.NewLine +
                        "Exception originale " + ex.ToString() );
                }
            } // if
        
            
            return Resultat;
        } // methode

        /// <summary>
        /// Insere l'information de contenu
        /// dans chaque entree du dictionnaire
        /// </summary>
        ///Répartit lMinformation entre les noeuds
        ///parent et enfants (selon les nibeaux)
        private void TraiterDictionnaire()
        {
            EntiteDocument Temp = 
                new EntiteDocument();

            List<String> Cles =
                new List<string>(Dictionnaire.Keys);
            Cles.Sort();
            foreach (var cle in Cles)
            {
                Dictionnaire.TryGetValue(cle, out Temp);
                TraiterItem(ref Temp);
                MAJDict(cle, Temp);




            }

        }


        private  void  TraiterItem(
                ref EntiteDocument ItemATraiter)
        {
                if (ItemATraiter == null)
                    throw new Exception("Le noeud à traiter n'existe pas ca il est à null");
                if (ItemATraiter.Information == "")
                    ItemATraiter.Information = 
                        HHêlper.GabaritNethode();
            if (string.IsNullOrEmpty(ItemATraiter.NomFichier))
                ItemATraiter.NomFichier = NormaliserNomFichier(
                    ItemATraiter.Nom);
            
        }

        

        private void PublierTout()
        {
            EntiteDocument Courant =
                new EntiteDocument();

            List<String> Cles =
                new List<string>(Dictionnaire.Keys);
            Cles.Sort();
            foreach (var cle in Cles)
            {
                Courant = GetEntiteDoc(cle);
                Publier(Courant.NomFichier, cle,
                    Courant.Information );

                
            }

        }



        /// <summary>
        /// Retourne le innertext d'un
        /// élément xml ou
        /// retourne la valeursinukk si
        /// l'élémenyesy à null
        /// </summary>
        /// <param name="Valeur">Élément XML à traiter</param>
        /// <param name="ValeurSiNull">String à retourner si l'élément est null</param>
        /// <returns></returns>
        private string nvlXML(
            XmlNode Valeur,
            string ValeurSiNull = "")
        {
            if (Valeur == null)
                return ValeurSiNull;
            else
                return Valeur.InnerText; 
        }

        private void AjouterDictionnaire(
            XElement  NoeudDict)
        {
            EntiteDocument Courant = 
                new EntiteDocument();
            Courant = TraiterNoeud(NoeudDict);
            string FullName = "";

            for (int i = 0;
                i < (Courant.NombreNiveaux() - 1);
                i++ )
            {
                if(i == 0)
                    FullName +=  Courant.Niveau(i);
                else
                    FullName += "." + Courant.Niveau(i);

                MAJDict(FullName, 
                    new EntiteDocument(FullName));
            }
            MAJDict(Courant.Nom, Courant);

            
        }

        private void  MAJDict(
            string Cle,
            EntiteDocument Doc)
        {
            if (!Dictionnaire.ContainsKey(Cle))
                Dictionnaire.Add(Cle, Doc); 
            else
            {
                Dictionnaire[Cle] = 
                    Doc;
            }
            


        }

        public void AjouterToutDict()
        {
            string Debug;
            Doc = XDocument.Load(FichierXMLDoc);

            IEnumerable<XElement> ListeNoeuds =
                Doc.Root.Descendants("member");

            ListeNoeuds.Count();




            foreach (XElement  NoeudCourant in ListeNoeuds)
            { 
                AjouterDictionnaire(NoeudCourant);
                //throw new NotImplementedException(Debug );

            }
            
        }


        public EntiteDocument GetEntiteDoc(
            String Cle)
        {
            EntiteDocument Resultat;
            Dictionnaire.TryGetValue(Cle,out Resultat);

            return Resultat ;
        }


        public string NormaliserNomFichier(
            String NomANormaliser,
            string Extension = ".html")
        {
            String Resultat =
                NomANormaliser.Replace(":","_").Replace("\\", "_");
            Resultat = Resultat.Replace("/","_"); 
            if(Resultat.Contains("(")  )
            {
                Resultat = Resultat.Substring(0, Resultat.IndexOf("("));
                Resultat += RaccourciFichier.ToString();
                RaccourciFichier = RaccourciFichier + 1;
            }

            Resultat += Extension;

            return Resultat;


        }

    } // class
}

