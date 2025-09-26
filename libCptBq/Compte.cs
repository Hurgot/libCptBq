using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace libCptBq
{
    /// <summary>
    /// Classe Compte
    /// </summary>
    public class Compte
    {
        private int numero;
        private string nom;
        private decimal solde;
        private decimal decouvertAutorise; // valeur négative
        private List<Mouvement> mesMouvements = new List<Mouvement>();

        /// <summary>
        /// Propriétés implémentées automatiquement
        /// </summary>
        public int Numero
        {
            get { return numero; }
            set { numero = value; }
        }
        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }
        public decimal Solde
        {
            get { return solde; }
            set { solde = value; }
        }
        public decimal DecouvertAutorise
        {
            get { return decouvertAutorise; }
            set { decouvertAutorise = value; }
        }

        public List<Mouvement> MesMouvements
        {
            get { return mesMouvements; }
            set { mesMouvements = value; }
        }


        /// <summary>
        /// Constructeur à 4 arguments
        /// </summary>
        /// <param name="numero">le numéro</param>
        /// <param name="nom">le nom</param>
        /// <param name="solde">le solde</param>
        /// <param name="decouvertAutorise">le découvert autorisé</param>
        public Compte(int numero, string nom, decimal solde, decimal decouvertAutorise)
        {
            this.numero = numero;
            this.nom = nom;
            this.solde = solde;
            this.decouvertAutorise = decouvertAutorise;
        }
        /// <summary>
        /// Constructeur de compte par défaut
        /// </summary>
        public Compte()
        {
            this.numero = 0;
            this.nom = "";
            this.solde = 0;
            this.decouvertAutorise = 0;
        }
        /// <summary>
        /// Réecriture de la méthode ToString
        public override string ToString()
        {
            return $"numero: {Numero} nom: {Nom} solde: {Solde:0.00} decouvert autorisé: {DecouvertAutorise:0.00}";
        }
        /// </summary>
        /// <returns></returns>


        /// <summary>
        /// Crédite le compte du montant spécifié
        /// </summary>
        /// <param name="montant">Le montant à créditer</param>
        public void Crediter(decimal montant)
        {
            if (montant > 0)
            {
                Solde += montant;
            }
        }

        /// <summary>
        /// Débite le compte du montant spécifié si le solde le permet
        /// </summary>
        /// <param name="montant">Le montant à débiter</param>
        /// <returns>True si le débit a été effectué, False sinon</returns>
        public bool Debiter(decimal montant)
        {
            if (this.Solde - montant >= this.DecouvertAutorise)
            {
                this.Solde -= montant;
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// Transférer un montant vers un autre compte
        /// </summary>
        /// <param name="montant"></param>
        /// <param name="compteDestination"></param>
        /// <returns></returns>
        public bool Transferer(decimal n, Compte c)
        {
            if (this.Solde - n >= this.DecouvertAutorise && n > 0)
            {
                this.Solde -= n;
                c.Solde += n;
                return true;
            }
            else
            {
                return false;
            }

        }







        /// <summary>
        /// Savoir si le solde est supérieur à celui d'un autre compte
        /// </summary>
        /// <param name="compteDestination"></param>
        /// <returns></returns>
        public bool Superieur(Compte c)
        {
            if (this.Solde > c.Solde)
            {
                return true;
            }
            else
            {
                return false;



            }

        }

        public void AjouterMouvement(Mouvement m)
        {
            mesMouvements.Add(m);
        }
    }
}
