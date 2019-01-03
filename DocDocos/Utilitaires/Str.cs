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
        /// <summary>
        /// Retourne le début de la chaîne de 
        /// caractère qui sont acvant un certains caractère
        /// </summary>
        /// <returns></returns>
        public static string Debut(
            string Chaine,
            string Separateur)
        {
            if (string.IsNullOrEmpty(Chaine))
                return "";
            if (Chaine.IndexOf(Separateur) < 0)
                return Chaine;
            return Chaine.Substring(0,
               Chaine.IndexOf(Separateur)); 
        }



    } // class
}
