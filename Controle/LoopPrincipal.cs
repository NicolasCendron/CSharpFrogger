using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
namespace CSharpFrogger.Controle
{ 
    class LoopPrincipal
    {
        #region static
        public static int DEFAULT_UPS = 80;
        public static int DEFAULT_NO_DELAYS_PER_YIELD = 16;
        public static int DEFAULT_MAX_FRAME_SKIPS = 5;

        private static readonly DateTime Jan1st1970 = new DateTime
           (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        private static long NanoTime()
        {
            Stopwatch stopwatch = new Stopwatch();
            long nano = 10000L * Stopwatch.GetTimestamp();
            nano /= TimeSpan.TicksPerMillisecond;
            nano *= 100L;
            return nano;
        }

        #endregion

        #region atributos

        private IEtapasLoop game;
        private long desiredUpdateTime;
        private Boolean running;
        private Boolean CurrentLevel;

        private long afterTime;
        private long beforeTime = CurrentTimeMillis();

        private long overSleepTime = 0;
        private long excessTime = 0;

        private int noDelaysPerYield = DEFAULT_NO_DELAYS_PER_YIELD;
        private int maxFrameSkips = DEFAULT_MAX_FRAME_SKIPS;
        private int velocidade;
        private const int VEL_INICIAL = 60;

        int noDelays = 0;
        #endregion
        #region Propriedades

        #endregion

        #region Construtores

        public LoopPrincipal(IEtapasLoop loopSteps, int ups, int maxFrameSkips, int noDelaysPerYield) : base()
        {
            if (ups < 1)
                throw new ArgumentException("You must display at least one frame per second!");

            if (ups > 1000)
                ups = 1000;

            this.game = loopSteps;
            this.desiredUpdateTime = 1000000000L / ups;
            this.running = true;
            this.maxFrameSkips = maxFrameSkips;
            this.noDelaysPerYield = noDelaysPerYield;
            this.velocidade = VEL_INICIAL;
        }

        public LoopPrincipal(IEtapasLoop loopSteps, int ups) 
            : this(loopSteps, ups, DEFAULT_MAX_FRAME_SKIPS, DEFAULT_NO_DELAYS_PER_YIELD)
        {   
        }

        public LoopPrincipal(IEtapasLoop loopSteps)
            : this(loopSteps, DEFAULT_UPS)
        {
        }

        #endregion

        #region Metodos
        private void Sleep(long nanos)
        {
            try
            {
                noDelays = 0;
                long beforeSleep = NanoTime();
                Thread.Sleep(Convert.ToInt32(nanos / 1000000L));
                overSleepTime = NanoTime() - beforeSleep - nanos;
            }
            catch (Exception e) {
                //TODO TRATAR EXCEPTION
            }
        }

        private void yieldIfNeed()
        {
            if (++noDelays == noDelaysPerYield)
            {
                Thread.Yield();
                noDelays = 0;
            }
        }

        private long CalculateSleepTime()
        {
            return desiredUpdateTime - (afterTime - beforeTime) - overSleepTime;
        }

        public void Run()
        {
            running = true;

            try
            {
                while (running)
                {
                    game.Setup(velocidade);
                    CurrentLevel = true;
                    while (CurrentLevel)
                    {
                        beforeTime = NanoTime();
                        skipFramesInExcessTime();

                        game.ProcessLogics();
                        game.RenderGraphics();
                        game.PaintScreen();

                        afterTime = NanoTime();
                        long sleepTime = CalculateSleepTime();
                        if (sleepTime >= 0)
                            Sleep(sleepTime);
                        else
                        {
                            excessTime -= sleepTime; // Sleep time is negative
                            overSleepTime = 0L;
                            yieldIfNeed();
                        }
                    }
                }
            }
            catch (Exception e)
            {
               //TODO implementar tratamento
            }
            finally
            {
                running = false;
                game.TearDown();
                Environment.Exit(0);
            }
        }

        private void skipFramesInExcessTime()
        {
            int skips = 0;
            while ((excessTime > desiredUpdateTime) && (skips < maxFrameSkips))
            {
                excessTime -= desiredUpdateTime;
                game.ProcessLogics();
                skips++;
            }
        }

        public void stop()
        {
            running = false;
        }
        public void nextLevel()
        {
            CurrentLevel = false;
            velocidade += 30;
        }
        #endregion

    }
}
