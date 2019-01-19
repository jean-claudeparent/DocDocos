///<License>
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
       
        private  IO monIO = new IO();
        private HTMLHelper HHêlper = new HTMLHelper();
         
        /// <summary>
        /// Constructeur qui peut accepter
        /// le nom du fichier degabarit
        /// en argument
        /// </summary>
        /// <param name="FichierGabaritOptionnel">Chemin et nom de fichier pour utiliser un gabarit optionnel pour la génération du site</param>
        public  DocDocosDA(
            string FichierGabaritOptionnel = null)
        {
            if (string.IsNullOrEmpty(FichierGabaritOptionnel))
            {
                Gabarit = HTMLHelper.GabaritDemo();
            } else
            {
                if (File.Exists(FichierGabaritOptionnel))
                {
                    ExtensionFichier = Path.GetExtension(FichierGabaritOptionnel);

                    Gabarit = File.ReadAllText(FichierGabaritOptionnel);

                }
                else
                    throw new Exception("Le fichier de gabarit "+
                        FichierGabaritOptionnel +
                        " est inexistant."); 
            }

    }

        /// <summary>
        /// Ce nombre remplacera une partie du nom
        /// d'entiteDocument pour donner des noms de fichiers 
        /// pluscourts.
        /// </summary>
        public Int32 RaccourciFichier = 1;
        
        /// <summary>
        /// Extension des fichiers html gémérés,
        /// avec le point. Valeur par défaut ".html"
        /// </summary>
        public string ExtensionFichier = ".html";

        /// <summary>
        /// Chemin et nom du fichier de documentation xml à traiter
        /// </summary>
        public string FichierXMLDoc;

        /// <summary>
        /// Répertoire où sera créé le site web.
        /// </summary>
        public string RepertoireSortie;

        /// <summary>
        /// Contenu du gabarit qui sera utilisé pour générer les pages du site.
        /// </summary>
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
                if (Temp == null)
                    Temp = new EntiteDocument (cle);
                TraiterItem(ref Temp);
                MAJDict(cle, Temp);
                MAJDictParent(Temp);





            }

        }

        /// <summary>
        /// Crée le html et le mnom de fichier
        /// </summary>
        /// <param name="ItemATraiter"></param>
        private  void  TraiterItem(
                ref EntiteDocument ItemATraiter)
        {
            // Initialiser les propriétés vides
                if (ItemATraiter == null)
                    throw new Exception(
                        "Le noeud à traiter n'existe pas car il est à null");
                if (ItemATraiter.Information == "")
                    ItemATraiter.Information = 
                        HHêlper.GabaritInterne();
            if (string.IsNullOrEmpty(ItemATraiter.NomFichier))
                ItemATraiter.NomFichier = NormaliserNomFichier(
                    ItemATraiter.Nom);
            // Construire le html
            ItemATraiter.Information =
                ItemATraiter.Information.Replace(
                    "{{Summary}}",
                    ItemATraiter.Sommaire ); 
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
                Courant.Information = 
                    HHêlper.MenageHTNL(Courant.Information);  
                Publier(Courant.NomFichier, cle,
                    Courant.Information );

                
            }

        }


        
        private void AjouterDictionnaire(
            XElement  NoeudDict)
        {
            string monDebug = "";

            EntiteDocument Courant = 
                new EntiteDocument();
            Courant = TraiterNoeud(NoeudDict);
            string FullName = "";

            for (int i = 0;
                i < (Courant.NombreNiveaux() - 1);
                i++ )
            {
                if(i == 0)
                    FullName =  Courant.Niveau(i);
                else
                    FullName += "." + Courant.Niveau(i);

                MAJDict(FullName, null);
                
            }
            MAJDict(Courant.Nom  , Courant);

            
        }

        private void  MAJDict(
            string Cle,
            EntiteDocument Doc)
        {
            if (!Dictionnaire.ContainsKey(Cle))
                Dictionnaire.Add(Cle, Doc); 
            else
            {
                if(Doc != null)
                  Dictionnaire[Cle] = 
                      Doc;
            }
            string d = "";
            


        }

        /// <summary>
        /// Ajouter chaque membre du fichier
        /// xml dans le dictionnaire.
        /// </summary>
        public void AjouterToutDict()
        {
            if (!File.Exists(FichierXMLDoc))
                throw new FileNotFoundException(
                    "Le fichier XML de documentation <" +
                    FichierXMLDoc +
                    "> est introuvable ou innacessible.");
            
            Doc = XDocument.Load(FichierXMLDoc);

            IEnumerable<XElement> ListeNoeuds =
                Doc.Root.Descendants("member");

            ListeNoeuds.Count();




            foreach (XElement  NoeudCourant in ListeNoeuds)
            { 
                AjouterDictionnaire(NoeudCourant);
                
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

            Resultat += ExtensionFichier;

            return Resultat;


        }

        private void MAJDictParent(
            EntiteDocument  Enfant)
        {
            string CleParent = Enfant.NomParent();
            if (string.IsNullOrEmpty(CleParent))
                return;

            EntiteDocument Parent = Dictionnaire[CleParent];
            if (string.IsNullOrEmpty(
                Parent.Information))
                throw new Exception(
                    "Erreur de logicque dans la programmation, l'entrée de dictionnnaire " +
                    CleParent +
                    " n'a pas son information initialisée avec le gabarit interne html");
            // faire le travail
            string InfoEnfant = 
                HHêlper.CreerRangee(
                    HHêlper.ConstruireLien(
                        Enfant.NomFichier,
                      Enfant.Nom),
                    Enfant.Sommaire);
            Parent.Information =
                HHêlper.ConstruireHTML(
                   Parent.Information,
                   "{{Rangee}}",
                   InfoEnfant);
            MAJDict(CleParent, Parent );
            

             
        }



    } // class
}

