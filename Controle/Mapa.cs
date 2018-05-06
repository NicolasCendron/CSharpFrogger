using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFrogger.Controle
{
    class Mapa : SceneObject
    {
        #region atributos
        private const String IMAGE_ADRESS = "/img/mapa.png";
	    private const int X_MAPA = -16;
        private const int Y_MAPA = 26;
        private const int ALTURA_MAPA = 302;
        private const int LARGURA_MAPA = 388;

        #endregion

        #region Construtor

        public Mapa()
        {
            //setImage(IMAGE_ADRESS);
            PosicaoX = X_MAPA;
            PosicaoY = Y_MAPA;
            Altura = ALTURA_MAPA;
            Largura = LARGURA_MAPA;
        }

        #endregion

        #region Metodos
        /*
        private void setImage(String image_adress)
        {
            try
            {
                super.setImage(ImageIO.read(Personagem.class.getResource(image_adress)));
			} 
            catch (IOException e) {
				System.out.println("Erro ao carregar imagem " + image_adress);
        }
        */
        #endregion
    }
}

	