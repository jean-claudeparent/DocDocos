using System;
namespace DocDocos
{
    public class Str
    {
        /// <summary>
        /// Retourne le dernier caractère d'une chaîne
        /// </summary>
        /// <param name="Chaine"></param>
        /// <returns></returns>
        public string DernierC(string Chaine)
        {
            if (Chaine == null) return "";
            if (Chaine == "") return "";
            return Chaine.Substring(Chaine.Length - 1, 1);

        }

        public string ValeurSQL(
            string ValeurTexte)
        {
            if (ValeurTexte == null)
                return "null";
            else
                return "'" + 
                    ValeurTexte.Replace("'", "''") +
                    "'"; 
        }


        public String ExceptionData(
            Exception ex)
        {
            String Resultat = "";

            foreach (string  key in ex.Data.Keys)
            {
                Resultat = Resultat +
                    " Clé : " + key +
                    " Valeur : " + ex.Data[key];
            }

            return Resultat;
            
        }



    } // class
}
