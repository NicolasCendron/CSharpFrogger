using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpFrogger.Controle
{
    class Jogo : IEtapasLoop
    {
        #region Atributos
        private LoopPrincipal loop = new LoopPrincipal(this, 60); //TODO:

        private long previous = ControleTempo.CurrentTimeMillis();
        private Sapo Sapo { get; set; }
        private Veiculo[] veiculos;
        private Boolean[] veiculoNoMapa;
        private Boolean[] pista1;
        private Boolean[] pista2;
        private Boolean[] pista3;
        private const int TAM_PISTA = 13;
        private const int NRO_VEICULOS = 10;
        private Mapa Mapa { get; set; }
        #endregion

        #region Construtor

        public Jogo()  // : base("Frogger")
        {
            /* this.addKeyListener(this);
             setDefaultCloseOperation(EXIT_ON_CLOSE);
             setBounds(100, 100, 378, 357);
             setResizable(false);
             getContentPane().setLayout(null);
             setIgnoreRepaint(true);
             setFocusable(true);
             */
            Sapo = new Sapo();

            //addWindowListener(new WindowAdapter()

            //    {
            //    @Override
            //    public void windowClosing(WindowEvent e)
            //    {
            //        //Se apertar o x, paramos o loop.
            //        loop.stop();
            //    }
            //});

        }

        #endregion

        #region Metodos
        //public void iniciarLoop()
        //{
        //    new Thread(loop, "Main loop").start();
        //}

        #endregion

        #region Setup
        
        public void Setup(int velocidade)
            {
                //Criamos a estratégia de double buffering
           
                //TODO: createBufferStrategy(2);
                Random random = new Random();
                veiculos = new Veiculo[NRO_VEICULOS];
                for (int i = 0; i < NRO_VEICULOS; i++)
                {
                    if (i < NRO_VEICULOS / 4)
                        veiculos[i] = new Onibus(velocidade);
                    else if (i >= NRO_VEICULOS / 4 && i < NRO_VEICULOS / 2)
                        veiculos[i] = new Carro(velocidade);
                    else if (i >= NRO_VEICULOS / 2 && i < NRO_VEICULOS)
                        veiculos[i] = new Moto(velocidade);
                }
                for (int i = 0; i < NRO_VEICULOS; i++)
                {
                    int j = random.Next(veiculos.Length);

                    Veiculo temp = veiculos[i];
                    veiculos[i] = veiculos[j];
                    veiculos[j] = temp;
                }
                pista1 = new Boolean[TAM_PISTA];
                pista2 = new Boolean[TAM_PISTA];
                pista3 = new Boolean[TAM_PISTA];
                veiculoNoMapa = new Boolean[NRO_VEICULOS];
                for (int i = 0; i < TAM_PISTA; i++)
                {
                    pista1[i] = false;
                    pista2[i] = false;
                    pista3[i] = false;
                }
                for (int i = 0; i < NRO_VEICULOS; i++)
                {
                    veiculoNoMapa[i] = false;
                }
                Mapa = new Mapa();
                Sapo.ResetaPosicao();
            }
        #endregion

        #region Process Logics
        public void processLogics()
        {
            // TODO Auto-generated method stub
            //Calcula o tempo entre dois updates
            long time = ControleTempo.CurrentTimeMillis() - previous;
            if (Sapo.Vidas == 0)
                Environment.Exit(0);
            if (Sapo.Ymatriz == 0)
                loop.nextLevel();
            for (int i = 0; i < TAM_PISTA; i++)
            {
                pista1[i] = false;
                pista2[i] = false;
                pista3[i] = false;
            }
            for (int i = 0; i < NRO_VEICULOS; i++)
            {
                if (veiculoNoMapa[i])
                {
                    if (veiculos[i].Visible)
                    {
                        for (int j = veiculos[i].Xmatriz; j < veiculos[i].NroBlocosOcupa + veiculos[i].Xmatriz; j++)
                        {
                            if (Sapo.Ymatriz == veiculos[i].Ymatriz && Sapo.Xmatriz == j)
                            {
                                Sapo.ResetaPosicao();
                                Sapo.PerdeVida();
                            }
                        }
                        veiculos[i].update(time);
                        switch (veiculos[i].Pista)
                        {
                            case 1:
                                for (int j = veiculos[i].Xmatriz; j < veiculos[i].Xmatriz + veiculos[i].NroBlocosOcupa; j++)
                                    pista1[j + 3] = true;
                                break;
                            case 2:
                                for (int j = veiculos[i].Xmatriz; j < veiculos[i].Xmatriz + veiculos[i].NroBlocosOcupa; j++)
                                    pista2[j + 3] = true;
                                break;
                            case 3:
                                for (int j = veiculos[i].Xmatriz; j < veiculos[i].Xmatriz + veiculos[i].NroBlocosOcupa; j++)
                                    pista3[j + 3] = true;
                                break;
                        }
                    }
                    else
                    {
                        veiculoNoMapa[i] = false;
                        veiculos[i].resetPosicao();
                    }
                }
            }
            int soma1 = 0;
            int soma2 = 0;
            int soma3 = 0;
            for (int i = 0; i < TAM_PISTA; i++)
            {
                if (pista1[i])
                    soma1++;
                if (pista2[i])
                    soma2++;
                if (pista3[i])
                    soma3++;
            }
            if (soma1 < 0.3 * TAM_PISTA)
                tentaColocarVeiculo(pista1, 1);
            if (soma2 < 0.3 * TAM_PISTA)
                tentaColocarVeiculo(pista2, 2);
            if (soma3 < 0.3 * TAM_PISTA)
                tentaColocarVeiculo(pista3, 3);
            //Grava o tempo na saída do método
            previous = ControleTempo.CurrentTimeMillis();

        }
        #endregion

        #region Coloca Veiculos
        private void tentaColocarVeiculo(Boolean pista[], int nroPista)
        {
            int i = 0;
            Boolean VeiculoFoiColocado = false;
            while (i < NRO_VEICULOS && !VeiculoFoiColocado)
            {
                if (!veiculoNoMapa[i])
                {
                    Boolean CabeNaPista = true;
                    for (int j = 0; j <= veiculos[i].NroBlocosOcupa); j++)
                    {
                        if (pista[j])
                            CabeNaPista = false;
                    }
                    if (CabeNaPista)
                    {
                        for (int j = 0; j < veiculos[i].NroBlocosOcupa; j++)
                        pista[j] = true;
                        veiculoNoMapa[i] = true;
                        veiculos[i].Visible = true;
                        veiculos[i].Pista = nroPista;
                        veiculos[i].Visible = true;
                        VeiculoFoiColocado = true;
                    }

                }
                i++;
            }

        }
        #endregion

        #region Renderizar Gráficos
        /*
       public void RenderGraphics()
       {

           // TODO Auto-generated method stub
           Graphics g = getBufferStrategy().getDrawGraphics();

           Graphics g2 = g.create(getInsets().left,
                      getInsets().top,
                      getWidth() - getInsets().right,
                      getHeight() - getInsets().bottom);
           //Limpamos a tela
           g2.setColor(Color.GREEN);
           g2.fillRect(0, 0, getWidth(), getHeight());
           if (Mapa != null)
               mapa.draw((Graphics2D)g2);
           if (Sapo != null)
               sapo.draw((Graphics2D)g2);
           for (int i = 0; i < NRO_VEICULOS; i++)
           {
               if (veiculoNoMapa[i])
               {
                   veiculos[i].draw((Graphics2D)g2);
               }
           }

           g.dispose(); //Liberamos o contexto criado.
           g2.dispose();

       }

       public void paintScreen()
           {
               if (!getBufferStrategy().contentsLost())
                   getBufferStrategy().show();
           }

       public void tearDown()
           {
               //Não é realmente necessário, pois o jogo acaba.
               //Mas se fosse um fim de fase, seria.
               sapo = null;
           }
           */
        #endregion


        #region Handle KeyPresses
        /*
       public void keyPressed(KeyEvent e)
       {
           // TODO Auto-generated method stub
           switch (e.getKeyCode())
           {
               case KeyEvent.VK_W:
                   sapo.andaParaFrente();
                   break;
               case KeyEvent.VK_S:
                   sapo.andaParaTras();
                   break;
               case KeyEvent.VK_A:
                   sapo.andaParaEsquerda();
                   break;
               case KeyEvent.VK_D:
                   sapo.andaParaDireita();
                   break;
           }

       }

   public void keyReleased(KeyEvent e)
       {
           // TODO Auto-generated method stub

       }

   public void keyTyped(KeyEvent e)
       {
           // TODO Auto-generated method stub

       }
   */
        #endregion

    }
}
