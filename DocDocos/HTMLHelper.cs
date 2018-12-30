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
            return "<!-- PartieBariable --!>" +
                GabaritMethodeSummary() + 
                "<table><!-- Contenu --!>" +
                "</table>";
        }
        
        /// <summary>
        /// Retourne legabatit pour
        /// le traitement d'une ligne de tableau
        /// </summary>
        /// <returns></returns>
        public string GabaritItemMethode()
        {
            return "<tr></tr>";
        }

        public String GabaritMethodeSummary()
        {
            return "<p><!--Summary --!></p>";
        }

    } //class
}
