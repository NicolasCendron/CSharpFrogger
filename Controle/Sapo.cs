using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFrogger.Controle
{
    class Sapo : Personagem
    {
        #region atributos

        public int Vidas { get; set; }
        public int Score { get; set; }
        private const int X_MATRIZ_INICIO = 3;
        private const int Y_MATRIZ_INICIO = 4;
        private const int VIDAS_INICIAL = 3;
        private const int X_INICIAL = 162;
        private const int Y_INICIAL = 270;
        private const int DESLOCAMENTO_X = 50;
        private const int DESLOCAMENTO_Y = 60;
        private const int LARGURA = 46;
        private const int ALTURA = 48;
        private const String IMAGEM_SAPO = "/img/sapinho.png";

        #endregion

        #region construtor
        public Sapo()
        {
            PosicaoX = X_INICIAL;
            PosicaoY = Y_INICIAL;
            //TODO: super.setImage(IMAGEM_SAPO);
            Altura = ALTURA;
            Largura = LARGURA;
            Vidas = VIDAS_INICIAL;
            Xmatriz =X_MATRIZ_INICIO;
            Ymatriz = Y_MATRIZ_INICIO;
        }

        #endregion

        #region Metodos
        private void update(int desloc_x, int desloc_y)
        {
            Xmatriz = Xmatriz + desloc_x;
            Ymatriz = Ymatriz + desloc_y;
            if (colisaoParede())
            {
                Xmatriz = Xmatriz - desloc_x;
                Ymatriz = Ymatriz - desloc_y;
            }
            else
            {
                PosicaoX = PosicaoX + DESLOCAMENTO_X * desloc_x;
                PosicaoY = PosicaoY + DESLOCAMENTO_Y * desloc_y;
            }

        }

        public void andaParaFrente()
        {
            update(0, -1);
        }
        public void andaParaTras()
        {
            update(0, 1);
        }
        public void andaParaEsquerda()
        {
            update(-1, 0);
        }
        public void andaParaDireita()
        {
            update(1, 0);
        }

        public void PerdeVida()
        {
            Vidas = Vidas - 1;
        }
        public void IncrementaScore(int valor)
        {
            Score = Score + valor;
        }
        public void ResetaPosicao()
        {
            PosicaoX = X_INICIAL;
            PosicaoY = Y_INICIAL;
            Xmatriz = X_MATRIZ_INICIO;
            Ymatriz = Y_MATRIZ_INICIO;
        }
        #endregion
    }
}
