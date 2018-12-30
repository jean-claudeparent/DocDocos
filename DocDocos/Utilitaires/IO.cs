
using System;
using System.Reflection;
using System.IO;



namespace DocDocos
{
    public class IO
    {
        Str UStr = new Str();


        public string RepertoireAssembly()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ;

        }

        
        /// <summary>
        /// Ajoute un autre niveau à un path
        /// de fichier
        /// </summary>
        /// <param name="Chemin"></param>
        /// <param name="Ajout"></param>
        /// <returns></returns>
        public string AjouterCheminFichier(
           string Chemin,
           string Ajout)
        {
            if (Chemin == null)
                throw new Exception(
                    "Méthode AjouterCheminFichier n'accepte pas un chemin à null.");

            if (UStr.DernierC(Chemin) == Path.DirectorySeparatorChar.ToString())
                 return Chemin + Ajout;
            else
                return Chemin + Path.DirectorySeparatorChar.ToString() + Ajout;



            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";

        } // nethode

        public bool Windows()
        {
            if (Path.DirectorySeparatorChar.ToString() == "\\")
                return true;
            else
                return false;
        }


        public string RepertoireTravail()
        {
            if (RepertoireAssembly().Contains(
                "/PROD/"))
                return @"/Volumes/MacHD/users/jc/PROD/";
            if (RepertoireAssembly().Contains(
                    "\\PROD\\"))
                return @"d:\Utilisateur\PROD\";
            return AjouterCheminFichier(
                RepertoireAssembly(),
                "Ressources 1");

                    
                
        }



    } //class
} //namespace
