using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Counters
{
    class PerformanceCounterCategoriCreator
    {
       public const string NOME_CATEGORIA = "CountersCategory";

        internal void Create()
        {
            PerformanceCounterCategory.Delete(NOME_CATEGORIA);

            // 1. Verifica se a categoria já existe
            if (!PerformanceCounterCategory.Exists(NOME_CATEGORIA))
            {

                // 2. Cria a coleção de contadores e a preenche.
                var counterCollection = new CounterCreationDataCollection();

                // Contador - Média
                var media = GetNewCounterCreationData("Tempo médio por operação", PerformanceCounterType.AverageCount64);
                counterCollection.Add(media);

                var mediaBase = GetNewCounterCreationData("Tempo médio por operação BASE", PerformanceCounterType.AverageBase);
                counterCollection.Add(mediaBase);

                // Contador - Diferença/Variação 
                var diferenca = GetNewCounterCreationData("Diferença/Variação", PerformanceCounterType.CounterDelta32);
                counterCollection.Add(diferenca);

                // Contador - Instantânea
                var instantanea = GetNewCounterCreationData("Número de operações executadas", PerformanceCounterType.NumberOfItems32);
                counterCollection.Add(instantanea);

                // Contador - Porcentagem
                var porcentagem = GetNewCounterCreationData("Porcentagem", PerformanceCounterType.CounterTimer);
                counterCollection.Add(porcentagem);

                // Contador - Porcentagem
                var taxa = GetNewCounterCreationData("Número de operações por seg", PerformanceCounterType.RateOfCountsPerSecond32);
                counterCollection.Add(taxa);

                // 3. Cria a categoria do performance counter.
                PerformanceCounterCategory.Create(
                    NOME_CATEGORIA,
                    "categoria para exemplo de contadores",
                    PerformanceCounterCategoryType.SingleInstance,
                    counterCollection
                );
            }
        }

        private CounterCreationData GetNewCounterCreationData(string name, PerformanceCounterType performanceCounterType)
        {
            var counter = new CounterCreationData();
            counter.CounterName = name;
            counter.CounterHelp = "Contador  - " + name;
            counter.CounterType = performanceCounterType;

            return counter;
        }
    }
}
