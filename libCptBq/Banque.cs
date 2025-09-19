using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libCptBq
{
    public class Banque
    {
        private List<Compte> comptes;
        public Banque()
        {
            comptes = new List<Compte>();
        }

        public List<Compte> Comptes
        {
            get { return comptes; }
            set { comptes = value; }
        }

        public override string ToString()
        {
            string str = "";
            foreach (var Compte in comptes)
            {
                str += Compte.ToString();
            }
            return str;
        }   
        

        public void AjouteCompte(Compte compte)
        {
            comptes.Add(compte);
        }
        public void AjouteCompte(int numero, string nom, decimal solde, decimal decouvertAutorise)
        {
            Compte nouveauCompte = new Compte(numero, nom, solde, decouvertAutorise);
            comptes.Add(nouveauCompte);
        }
        public Compte compteMax()
        {
            if (comptes.Count == 0)
            {
                return null; // ou lancer une exception selon le besoin
            }
            Compte compteMax = comptes[0];
            foreach (var compte in comptes)
            {
                if (compte.Solde > compteMax.Solde)
                {
                    compteMax = compte;
                }
            }
            return compteMax;
        }

        
    }
}
