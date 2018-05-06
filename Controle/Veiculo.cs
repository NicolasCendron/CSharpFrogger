using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFrogger.Controle
{
    class Veiculo : Personagem
    {
        #region atributos
        public int Velocidade { get; set; }
        public Boolean Visible { get; set; }
        public int NroBlocosOcupa { get; set; }
        private const float TAM_BLOCO = 54;
        protected const int POS_PISTA1_Y = 210;
        protected const int POS_PISTA2_Y = 150;
        protected const int POS_PISTA3_Y = 90;
        protected const int POS_PISTA1_X = -162;
        protected const int POS_PISTA2_X = -162;
        protected const int POS_PISTA3_X = -162;
        private int pista;
        #endregion

        #region Construtor
        public Veiculo(int vel)
        {
            Velocidade = vel;
        }
        #endregion

        #region Propriedades
        public int Pista
        {
            get
            {
                return pista;
            }
            set
            {
                switch (value)
                {
                    case 1:
                        PosicaoX = POS_PISTA1_X;
                        PosicaoY = POS_PISTA1_Y;
                        Ymatriz = 3;
                        break;
                    case 2:
                        PosicaoX = POS_PISTA2_X;
                        PosicaoY = POS_PISTA2_Y;
                        Ymatriz = 2;
                        break;
                    case 3:
                        PosicaoX = POS_PISTA3_X;
                        PosicaoY = POS_PISTA3_Y;
                        Ymatriz = 1;
                        break;
                }

            }
        }

        #endregion

        #region Metodos
        public void resetPosicao()
        {
            Pista = this.pista;
        }

        public void trocaPista(int direcao)
        {
            if (pista + direcao >= 3 && pista + direcao <= 1)
                pista += direcao;
        }

        public void AumentaVelocidade(int aumento)
        {
            Velocidade += aumento;
        }


        public void update(long time)
        {
            //O tempo é em milis. Para obter em segundos, precisamos dividi-lo por 1000.        
            PosicaoX = ((int)((time * Velocidade) / 1000 + PosicaoX));
            calculaPosicaoMatriz();
            if (Xmatriz >= 0)
            {
                if (colisaoParede())
                {
                    Visible = false;
                }
            }
        }

        private void calculaPosicaoMatriz()
        {
            Xmatriz = Convert.ToInt32(Math.Round(PosicaoX / TAM_BLOCO));
        }

        #endregion

    }
}
