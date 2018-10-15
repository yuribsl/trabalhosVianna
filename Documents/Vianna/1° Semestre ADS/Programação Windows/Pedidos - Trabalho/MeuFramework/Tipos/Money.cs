using System;
using System.Globalization;

namespace MeuFramework.Tipos
{
    public struct Money
    {
        #region Variaveis

        private decimal quantidade;
        private static decimal[] centavos = new decimal[] { 1, 10, 100, 1000 };
        private CultureInfo cultura;

        #endregion

        #region Construtores

        public Money(decimal valor) : this(valor, false) { }

        public Money(decimal valor, bool arredondar)
        {
            cultura = CultureInfo.CurrentCulture;
            Arredondar = arredondar;

            this.quantidade = 0;

            if (Arredondar)
                this.quantidade = Decimal.Round(Convert.ToDecimal(valor * FatorCentavos));
            else
                this.quantidade = Decimal.Truncate(Convert.ToDecimal(valor * FatorCentavos));
        }

        #endregion

        #region Propriedades

        public CultureInfo Cultura { get => cultura; }

        public bool Arredondar { get; set; }

        public int CasasDecimais
        {
            get { return Cultura.NumberFormat.CurrencyDecimalDigits; }
        }

        public decimal Valor
        {
            get { return Convert.ToDecimal(quantidade / FatorCentavos); }
        }

        private decimal FatorCentavos
        {
            get { return centavos[CasasDecimais]; }
        }

        #endregion

        #region Metodos

        public override bool Equals(object obj)
        {
            return (obj is Money) && Equals((Money)obj);
        }

        public bool Equals(Money outro)
        {
            return (outro.quantidade == this.quantidade) && (outro.Cultura != null) && (outro.Cultura == this.Cultura);
        }

        public override int GetHashCode()
        {
            return quantidade.GetHashCode() ^ Cultura.GetHashCode();
        }

        public override string ToString()
        {
            return Valor.ToString("N2");
        }

        public string ToString(string format)
        {
            return Valor.ToString(format, this.Cultura);
        }

        public Money[] Alocar(int n)
        {
            Money resultadoBaixo = NewMoney((quantidade / n), Arredondar);
            Money resultadoAlto = NewMoney(resultadoBaixo.quantidade + 1, Arredondar);
            Money[] resultados = new Money[n];

            int intResto = (int)quantidade % n;
            for (int i = 0; i < intResto; i++) resultados[i] = resultadoAlto;
            for (int i = intResto; i < n; i++) resultados[i] = resultadoBaixo;

            return resultados;
        }

        private static Money NewMoney(decimal quantidade, bool arredondar)
        {
            Money money = new Money();
            money.Arredondar = arredondar;

            if (arredondar)
                money.quantidade = Decimal.Round(quantidade);
            else
                money.quantidade = Decimal.Truncate(quantidade);

            return money;
        }

        private static void ConferirMoedasIguais(Money esq, Money dir)
        {
            if (esq.Cultura.LCID != dir.Cultura.LCID)
                throw new InvalidOperationException("Tipo de Moeda Diferente. Impossível efetuar a operação.");
        }

        public static Money Adicionar(Money esq, Money dir)
        {
            ConferirMoedasIguais(esq, dir);

            return NewMoney(esq.quantidade + dir.quantidade, dir.Arredondar || esq.Arredondar);
        }

        public static Money Subtrair(Money esq, Money dir)
        {
            ConferirMoedasIguais(esq, dir);
            return NewMoney(esq.quantidade - dir.quantidade, dir.Arredondar || esq.Arredondar);
        }

        public static Money Multiplicar(Money esq, decimal valor)
        {
            return NewMoney(Decimal.Round(esq.quantidade * valor), esq.Arredondar);
        }

        public static Money Multiplicar(Money esq, double valor)
        {
            return Multiplicar(esq, Convert.ToDecimal(valor));
        }

        public static Money Dividir(Money esq, decimal valor)
        {
            return NewMoney(Decimal.Floor(esq.quantidade / valor), esq.Arredondar);
        }

        public static Money Dividir(Money esq, double valor)
        {
            return Dividir(esq, Convert.ToDecimal(valor));
        }

        public static long Comparar(Money esq, Money dir)
        {
            ConferirMoedasIguais(esq, dir);
            return decimal.Compare(esq.quantidade, dir.quantidade);
        }

        public static Money operator +(Money esq, Money dir)
        {
            return Adicionar(esq, dir);
        }

        public static Money operator -(Money esq, Money dir)
        {
            return Subtrair(esq, dir);
        }

        public static Money operator *(Money esq, decimal valor)
        {
            return Multiplicar(esq, valor);
        }

        public static Money operator *(decimal valor, Money dir)
        {
            return Multiplicar(dir, valor);
        }

        public static Money operator *(Money esq, double valor)
        {
            return Multiplicar(esq, valor);
        }

        public static Money operator *(double valor, Money dir)
        {
            return Multiplicar(dir, valor);
        }

        public static Money operator /(Money esq, decimal valor)
        {
            return Dividir(esq, valor);
        }

        public static Money operator /(Money esq, double valor)
        {
            return Dividir(esq, valor);
        }

        public static bool operator >(Money esq, Money dir)
        {
            return Comparar(esq, dir) > 0;
        }

        public static bool operator >=(Money esq, Money dir)
        {
            return Comparar(esq, dir) >= 0;
        }

        public static bool operator <(Money esq, Money dir)
        {
            return Comparar(esq, dir) < 0;
        }

        public static bool operator <=(Money esq, Money dir)
        {
            return Comparar(esq, dir) <= 0;
        }

        public static bool operator ==(Money esq, Money dir)
        {
            return esq.Equals(dir);
        }

        public static bool operator !=(Money esq, Money dir)
        {
            return !esq.Equals(dir);
        }

        #endregion
    }
}
