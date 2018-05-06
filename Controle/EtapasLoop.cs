using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFrogger.Controle
{
    interface IEtapasLoop
    {
        void Setup(int valor);
        void ProcessLogics();
        void RenderGraphics();
        void PaintScreen();
        void TearDown();
    }
}
