using System;
using System.IO;
using DocDocos;


namespace DocDocosCLI
{
    class Program
    {
        static string monRepertoireSortie;
        static string monFichierXML;
        static string monGabarit;


        static void Main(string[] args)
        {
            try
            { 
              if (ArgumentInvalide(args))
            {
                Console.WriteLine("Usage:");
                Console.WriteLine(
                    "     DocFocos -f ficgierdix.xml " +
                    "-r repertoiresortie [-g gabarit.html]");
                Environment.Exit(99);  

            }

              if(!File.Exists(monFichierXML))
              {
                  Console.WriteLine("Il y a un problème avec le fichier xml " +
                      monFichierXML);
                  Environment.Exit(99);  
              }

              if ((!string.IsNullOrEmpty(monGabarit )) && 
                  (!File.Exists(monGabarit)))
            {
                Console.WriteLine("Il y a un problème avec le fichier de gabarit " +
                    monGabarit );
                Environment.Exit(99);
            }

              // Traiter
              DocDocosDA Generateur;
              if (string.IsNullOrEmpty(monGabarit))
                  Generateur = new DocDocosDA();
              else
                  Generateur = new DocDocosDA(monGabarit);

              Generateur.FichierXMLDoc = monFichierXML;
              Generateur.RepertoireSortie = 
                  monRepertoireSortie;
              Console.WriteLine("Génération du site dans : " +
                  Generateur.RepertoireSortie);
              Generateur.GenererHTML();
            } catch (Exception ex)
             {
                Console.WriteLine("Erreur technique" +Environment.NewLine +
                   ex.ToString() );
                Environment.Exit(99);  

             }
            }



        private  static bool ArgumentInvalide(
            string[] args)
        {
            if (args.Length < 4 )
                return false;
            for (int i=0; i < (args.Length - 1); i++)
            {
                if (args[i].ToUpper() == "-F")
                    monFichierXML = args[i+1];
                if (args[i].ToUpper() == "-G")
                    monGabarit  = args[i + 1];
                if (args[i].ToUpper() == "-R")
                    monRepertoireSortie  = args[i + 1];
            }
            if (string.IsNullOrEmpty(monFichierXML))
                return false ;
            if (string.IsNullOrEmpty(monRepertoireSortie ))
                return false;

            return true; 
        }
    }
}
