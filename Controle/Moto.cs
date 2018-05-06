using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFrogger.Controle
{
    class Moto : Veiculo
    {
        #region atributos
        private const String IMAGE_ADRESS = "/img/moto.png";
	    private const int LARGURA = 54;
        private const int ALTURA = 50;
        private const int NRO_BLOCOS_OCUPA = 1;

        #endregion

        #region construtor
        public Moto(int vel) : base(vel)
        {
           // super.setImage(IMAGE_ADRESS);
            Altura = ALTURA;
            Largura = LARGURA;
            NroBlocosOcupa = NRO_BLOCOS_OCUPA;
        }
        #endregion
    }
}
