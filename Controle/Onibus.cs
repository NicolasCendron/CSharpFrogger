using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFrogger.Controle
{
    class Onibus : Veiculo
    {
        #region Atributos
        private const String IMAGE_ADRESS = "/img/onibus.png";
	    private const int LARGURA = 162;
        private const int ALTURA = 50;
        private const int NRO_BLOCOS_OCUPA = 3;

        #endregion

        #region Construtor
        public Onibus(int vel) : base(vel)
        {
            //super.setImage(IMAGE_ADRESS);
            Altura = ALTURA;
            Largura = LARGURA;
            NroBlocosOcupa = NRO_BLOCOS_OCUPA;
        }
        #endregion
    }
}
