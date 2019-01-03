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
        public string GabaritItemMethode()
        {
            return "<tr>{{ContenuLigne}}</tr>";
        }

        public string GabaritCelluleMethode()
        {
            return "<td>{{ContenuCellule}}</td>";
        }

        public String GabaritMethodeSummary()
        {
            return "<p>{{Summary}}</p>";
        }

        public string GabaritTableau()
        {
            return "<table>{{Rangee}}</table>";
        }

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


} //class
}
