using System;
using System.Collections.Generic;
using System.Text;

namespace DocDocos
{
    /// <summary>
    /// Contient le squelette
    /// du HTML 
    /// </summary>
    public  class HTMLHelper
    {
        /// <summary>
        /// Liste de toutes les bariables de 
        /// substitution utilisées
        /// </summary>
        /// <returns></returns>
        public  static List<string> Variables()
        {
             List<string> Resultat =
                   new List<string>();
            Resultat.Add("{{ContenuLigne}}");
            Resultat.Add("{{ContenuCellule}}");
            Resultat.Add("{{Rangee}}"); 
            Resultat.Add("{{Summary}}");
            
            return Resultat; 
        }


        /// <summary>
        /// Retourne le gabarit HTML
        /// du conenu d'une page de méthode
        /// </summary>
        /// <returns>Gabarit</returns>
        public string GabaritNethode()
        {
            return 
                GabaritMethodeSummary() +
                GabaritTableau();
        }
        
        /// <summary>
        /// Retourne legabatit pour
        /// le traitement d'une ligne de tableau
        /// </summary>
        /// <returns></returns>
        private  string GabaritRangeeMethode()
        {
            return "<tr>{{ContenuLigne}}" +
                Environment.NewLine   + "</tr>";
        }

        private  string GabaritCelluleMethode()
        {
            return Environment.NewLine + "   " +
                "<td>{{ContenuCellule}}</td>";
        }

        private  String GabaritMethodeSummary()
        {
            return "<p>{{Summary}}</p>";
        }

        /// <summary>
        /// Retourne le html définissant une
        /// table HTML avec une zone pour
        /// insérer l'information du tableau
        /// </summary>
        /// <returns>HTML représentant le début et la fin d'une table html</returns>
        private  string GabaritTableau()
        {
            return Environment.NewLine +
                "<table>{{Rangee}}</table>" +
                Environment.NewLine;  
        }

        /// <summary>
        /// Retourne un gabarit de démonstration
        /// utilisé au casoù le fichier de 
        /// gabatir serait introuvable
        /// </summary>
        /// <returns></returns>
        public static string GabaritDemo()
        {
            string Resultat = "";
            Resultat = "<!DOCTYPE html>" + Environment.NewLine;
            Resultat += "<html><head>" + Environment.NewLine  ;
            Resultat += "<meta charset=\"utf-8\" />" + Environment.NewLine;
            Resultat += "<title>{{Titre}}</title>" + Environment.NewLine;
            Resultat += "</head><body>" + Environment.NewLine;
            Resultat += "<h1>{{Titre}}</h1>" + Environment.NewLine;
            Resultat += "<p>{{Contenu}}</p></body></html>" + Environment.NewLine;


            return Resultat; 


        }

        /// <summary>
        /// Encode en équivalent html les caracttères
        /// probématiques dans l'affichage d'un document html
        /// </summary>
        /// <param name="HTNL"></param>
        /// <returns></returns>
        public String EncodeHTNL(
            string HTNL)
        {
            HTNL = HTNL.Replace("<", "&lt;");
            HTNL = HTNL.Replace(">", "&gt;");
            HTNL = HTNL.Replace("&", "&amp;");
            HTNL = HTNL.Replace("{", "&#123;");
            HTNL = HTNL.Replace("}", "&#125;");


            return HTNL;

        }

        /// <summary>
        /// Crée un bloc de HTML
        /// représentant une rangée de données
        /// d'un tableau
        /// </summary>
        /// <param name="Cellule1"></param>
        /// <param name="Cellule2"></param>
        /// <param name="Cellule2"></param>
        /// <returns>HTML créé</returns>
        public  string CreerRangee(
            string Cellule1,
            string Cellule2 = null,
            string Cellule3 = null)
        {
            string Resultat = 
                GabaritCelluleMethode().Replace(
                    "{{ContenuCellule}}",
                    EncodeHTNL(Cellule1));

            if(Cellule2 != null)
                Resultat +=
                  GabaritCelluleMethode().Replace(
                    "{{ContenuCellule}}",
                    EncodeHTNL(Cellule2));

            if (!string.IsNullOrEmpty(Cellule3))
                Resultat +=
                  GabaritCelluleMethode().Replace(
                    "{{ContenuCellule}}",
                    EncodeHTNL(Cellule3));


            return Environment.NewLine + 
                GabaritRangeeMethode().Replace(
                "{{ContenuLigne}}", Resultat); 
        } // methode

    } //class
}
