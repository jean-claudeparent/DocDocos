
using System;
using System.Collections.Generic;
using System.Text;

namespace DocDocos
{
    public class EntiteDocument
    {
        public  EntiteDocument(
            string leNom = null)
        {
            if (leNom != null)
                Nom = leNom;
            
        }


        private TypeEntiteDocument _TypeEntite = 
            TypeEntiteDocument.Inconnu;

        public  string NomFichier = "";

        

        public TypeEntiteDocument TypeEntite
        {
            get
            {
                return _TypeEntite; 
            }
        }

        public string Sommaire
        { get; set; }

        private string _Nom = "";

        public string Nom
        { get
            {
                return _Nom;
            }
          set
            {
                if ((value.Length < 2) ||
                    (value.Substring(1,1) != ":"))
                {
                    _Nom = value;
                    _TypeEntite = 
                        TypeEntiteDocument.Inconnu;
                } // endif
                else
                {
                    string Temp = value.Substring(
                        0, 1).ToUpper();
                    switch  (Temp)
                    {
                        case "M":
                            _TypeEntite =
                                TypeEntiteDocument.Classe;
                            break;
                        case "T":
                            _TypeEntite =
                                TypeEntiteDocument.Propriete;
                            break;
                        default:
                            _TypeEntite = TypeEntiteDocument.Inconnu;
                            break;

                    } //end case
                    _Nom = value.Substring(2, value.Length - 2) ;
                } // end else
                _Niveaux = Str.Debut(_Nom,"(").Split("."[0]);
                
            } // end set




        }


        public string Namespace
        {
            get
            {
                return Niveau(0);
                  
            }
        }

        private string[] _Niveaux = new string[0];

        public string Niveau(
            int QuelNiveau)
        {
            try
            {
                if (_Niveaux.Length <= (QuelNiveau))
                    return "";
                else
                return _Niveaux[QuelNiveau];
            } catch (Exception ex)
            {
                throw new Exception("Méthode Niveau(" +
                    QuelNiveau.ToString() + ")  " +
                    "_Niveaux[" +
                    _Niveaux.Length.ToString() + "]" +
                    ex.Message +
                    ex.Message,ex ); 
            }
        }
            

        public int NombreNiveaux()
        {
            return _Niveaux.Length;
        }

        public string Information = "";

        public  string  EncodeType()
        {
            switch (TypeEntite)
                {
                case TypeEntiteDocument.Classe:
                    return "M:";
                case TypeEntiteDocument.Propriete:
                    return "T:";
                default:
                    return "I:";
            }
        }

        public String NomParent()
        {
            string Resultat = "";

            if (NombreNiveaux() == 0)
                return null;
    if (NombreNiveaux() == 1)
                return null ;
    for (int i = 0;
         i < (NombreNiveaux() - 1);
         i++)
        {
                if (i == 0)
                    Resultat = Niveau(0);
                else
                    Resultat += "." + Niveau(i);
          
        } //for
       return Resultat; 
    } //methode



    } // class
} // namespace
