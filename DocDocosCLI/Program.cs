using System;
using System.IO;
using DocDocos;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;


[assembly: InternalsVisibleTo("DocDocosCLITest")]




namespace DocDocosCLI
{
    internal class Program
    {
        internal static  Boolean UnitTest = false;
        internal static string monRepertoireSortie;
        internal static string monFichierXML;
        internal static string monGabarit;
        


        public  static void Main(string[] args)
        {
            try
            { 
              if (! (args.Length < 4))  
                    ExtraireArgument(args);
                else 
                    Terminer(99,"Usage:" + Environment.NewLine + 
                        "     DocFocos -f ficgierdoc.xml " +
                        "-r repertoiresortie [-g gabarit.html]");
                ValiderArguments();
              

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
                Terminer(99,
                    "Erreur technique" +
                    Environment.NewLine +
                    ex.ToString() );
                  

            }
        }

        private static void Terminer(
            int CodeDeRetour,
            string Message)
        {
            Console.WriteLine(Message);
            if (!UnitTest)
                Environment.Exit(CodeDeRetour);
            else
                throw new Exception("Code retour : " +
                    CodeDeRetour.ToString ()+
                    "message de console : " +
                    Message); 
            
        }

        private  static void  ExtraireArgument(
            string[] args)
        {
            
            for (int i=0; i < (args.Length - 1); i++)
            {
                if (args[i].ToUpper() == "-F")
                    monFichierXML = args[i+1];
                if (args[i].ToUpper() == "-G")
                    monGabarit  = args[i + 1];
                if (args[i].ToUpper() == "-R")
                    monRepertoireSortie  = args[i + 1];
            }
            

            
        }

        private static void ValiderArguments()
        {
            if (string.IsNullOrEmpty(monFichierXML))
               Terminer(99,
                        "Le fichier xml n'a pas été spécifié"); 

              if (!File.Exists(monFichierXML))
                     Terminer(99,
                         "Il y a un problème avec le fichier xml : " +
                          monFichierXML);
              

              if ((!string.IsNullOrEmpty(monGabarit )) && 
                  (!File.Exists(monGabarit)))
                Terminer(99,
                   "Il y a un problème avec le fichier de gabarit : " +
                    monGabarit );
        }

    }
}
