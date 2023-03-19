using System;
using System.ComponentModel;

namespace Konta
{
    public class PrzedZmianaHaslaArgs : CancelEventArgs
    {
        public PrzedZmianaHaslaArgs(string noweHaslo, string stareHaslo)
        {
            this.noweHaslo = noweHaslo;
            this.stareHaslo = stareHaslo;
        }
        private string noweHaslo;
        private string stareHaslo;
        public string NoweHaslo { get=>noweHaslo; }

        public string StareHaslo { get=>stareHaslo; }

    }

    public delegate void PrzedZmianaHaslaHandler(object sender, PrzedZmianaHaslaArgs e);

    public class Konto
    {
        private string haslo;

        public bool SprawdzHaslo(string haslo)
        {
            if (this.haslo == haslo)
            {
                return true;
            }   
            return false;
        }

        public string NazwaUzytkownika { get; set; }

        public static event PrzedZmianaHaslaHandler PrzedZmianaHasla;
        public static event EventHandler PoZmianieHasla;

        public bool ZmienHaslo(string stareHaslo, string noweHaslo)
        {
            
            if (!SprawdzHaslo(stareHaslo))
            {
                return false;
            }
            if (PrzedZmianaHasla != null)
            {
                PrzedZmianaHaslaArgs e = new PrzedZmianaHaslaArgs(noweHaslo, stareHaslo);
                
                PrzedZmianaHasla(this, e);
                if (e.Cancel)
                {
                    return false;
                }
            }

            haslo = noweHaslo;
            if (PoZmianieHasla!=null)
            {
                PoZmianieHasla(this, EventArgs.Empty);
            }
            return true;
        }

        public Konto(string nazwaUzytkownika, string haslo)
        {
            this.haslo = haslo;
            NazwaUzytkownika = nazwaUzytkownika;
        }
    }
}
