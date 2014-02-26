using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Counters
{
    class PerformanceCounterIncrementer
    {
        [DllImport("Kernel32.dll")]
        public static extern void QueryPerformanceCounter(ref long ticks);

        internal void Start()
        {
            // Cria as instâncias dos contadores para atualizar seus valores.
            var media = GetPerfCounter("Tempo médio por operação");
            var mediaBase = GetPerfCounter("Tempo médio por operação BASE");
            var diferenca = GetPerfCounter("Diferença/Variação");
            var instantanea = GetPerfCounter("Número de operações executadas");
            var porcentagem = GetPerfCounter("Porcentagem");
            var taxa = GetPerfCounter("Número de operações por seg");

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    var tick = GetTickForPerfCounter();

                    // Se o tick for par, incrementa, caso contrário, decrementa
                    //if (tick % 2 == 0)
                    //{
                    media.IncrementBy(tick);
                    mediaBase.Increment();
                    diferenca.Increment();
                    instantanea.IncrementBy(5);
                    porcentagem.Increment();
                    taxa.Increment();
                    //}
                    //else
                    //{
                    //    media.IncrementBy(tick);
                    //    mediaBase.Decrement();
                    //    diferenca.Decrement();
                    //    instantanea.Decrement();
                    //    porcentagem.Decrement();
                    //    taxa.Decrement();
                    //}


                    Console.WriteLine("Atualizou o valor dos contadores.");
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(3));
                }
            });

        }

        private long GetTickForPerfCounter()
        {

            /// Este método é utilizado porque  a
            /// System.DateTime.Now.Ticks não é preciso o suficiente para o 
            /// calculo da média.
            var rand = new Random();
            var startTime = 0L;
            var endTime = 0L;

            // mede tempo inicial.
            QueryPerformanceCounter(ref startTime);

            System.Threading.Thread.Sleep(rand.Next(500));

            // mede tempo final.
            QueryPerformanceCounter(ref endTime);

            // Tick = variação de tempo.
            return endTime - startTime; ;
        }

        private PerformanceCounter GetPerfCounter(string name)
        {
            var _perfCounter = new PerformanceCounter();
            _perfCounter.CategoryName = PerformanceCounterCategoriCreator.NOME_CATEGORIA;
            _perfCounter.CounterName = name;
            _perfCounter.MachineName = ".";
            _perfCounter.ReadOnly = false;

            return _perfCounter;
        }
    }
}
