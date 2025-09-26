using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libCptBq
{
    public class Type_
    {
        private string code;
        private string libelle;
        private char sens;

        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Libelle
        {
            get { return libelle; }
            set { libelle = value; }
        }

        public char Sens
        {
            get { return sens; }
            set { sens = value; }
        }

        public Type_(string code, string libelle, char sens)
        {
            this.code = code;
            this.libelle = libelle;
            this.sens = sens;
        }

        public override string ToString()
        {
            return $"Code: {code}, Libelle: {libelle}, Sens: {sens}";
        }


    }
}
