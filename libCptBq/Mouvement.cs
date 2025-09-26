using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libCptBq
{
    public class Mouvement
    {

        private DateTime dateMvt;
        private decimal montant;
        private Type_ leType;

        public DateTime DateMvt
        {
            get { return dateMvt; }
            set { dateMvt = value; }
        }
        public decimal Montant
        {
            get { return montant; }
            set { montant = value; }
        }
        public Type_ LeType
        {
            get { return leType; }
            set { leType = value; }
        }
        public Mouvement(DateTime dateMvt, decimal montant, Type_ leType)
        {
            this.dateMvt = dateMvt;
            this.montant = montant;
            this.leType = leType;
        }

    }
}
