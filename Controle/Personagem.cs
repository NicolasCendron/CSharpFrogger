using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFrogger.Controle
{
    class Personagem : SceneObject
    {
        #region atributos
        private const int LIMITE_X_MATRIZ_MIN = 0;
        private const int LIMITE_Y_MATRIZ_MIN = 0;
        private const int LIMITE_X_MATRIZ_MAX = 6;
        private const int LIMITE_Y_MATRIZ_MAX = 4;
        
        public int Xmatriz { get; set; }

        public int Ymatriz { get; set; }

        #endregion

        #region Metodos

        protected Boolean colisaoParede()
        {
            if (Xmatriz < LIMITE_X_MATRIZ_MIN)
                return true;
            else if (Xmatriz > LIMITE_X_MATRIZ_MAX)
                return true;
            else if (Ymatriz > LIMITE_Y_MATRIZ_MAX)
                return true;
            else if (Ymatriz < LIMITE_Y_MATRIZ_MIN)
                return true;
            else
                return false;
        }

        #endregion

        //TODO setImage

    }

}
