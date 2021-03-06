﻿using Accord;
using Accord.Genetic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Accord.Math;

namespace AccordGenetic.Wrap
{

    public class ApproximationWrap : IGeneticWrap
    {
        /// <summary>
        ///  True values.
        /// </summary>
        private readonly double[,] _data;

        public Population Population { get; set; }
        double[] constants = { 1, 2, 3, 5, 7 };
        readonly double[,] solution;
  
        public ApproximationWrap(double[,] data, int functionsSet, int populationSize, int geneticMethod, int selectionMethod, float minRange, float lengthRange)
        {
            _data = data;
            // create fitness function
            
            SymbolicRegressionFitness fitness = new SymbolicRegressionFitness(data, constants);
            // create gene function
            IGPGene gene = (functionsSet == 0) ? (IGPGene)new SimpleGeneFunction(6) : (IGPGene)new ExtendedGeneFunction(6);
            // create population
            Population = new Population(populationSize,
                 (geneticMethod == 0) ? (IChromosome)new GPTreeChromosome(gene) : (IChromosome)new GEPChromosome(gene, 15),
                 fitness,
                 (selectionMethod == 0) ? (ISelectionMethod)new EliteSelection() : (selectionMethod == 1) ? (ISelectionMethod)new RankSelection() : (ISelectionMethod)new RouletteWheelSelection());


            // solution array
            solution = new double[50, 2];


            // calculate X values to be used with solution function
            for (int j = 0; j < 50; j++)
            {
                solution[j, 0] = minRange + (double)j * lengthRange / 49;
            }
        }



        public Result Evaluate()
        {
            // get best solution
            string bestFunction = Population.BestChromosome.ToString();

            var inputs = new double[7];
            for (int i = 0; i < 5; i++)
            {
                inputs[i + 1] = constants[i];
            }
            // calculate best function
            for (int j = 0; j < 50; j++)
            {
                inputs[0] = solution[j, 0];
                var output = PolishExpression.Evaluate(bestFunction, inputs);
                solution[j, 1] = output;
            }

            return new Result(solution, bestFunction);
        }




        public Error EvaluateError()
        {
            // get best solution
            string bestFunction = Population.BestChromosome.ToString();

            // calculate error
            double error = 0.0;
            var inputs = new double[7];
            for (int i = 0; i < 5; i++)
            {
                inputs[i + 1] = constants[i];
            }

            for (int j = 0, k = _data.GetLength(0); j < k; j++)
            {
                inputs[0] = _data[j, 0];
                error += Math.Abs(_data[j, 1] - PolishExpression.Evaluate(bestFunction, inputs));
            }


            return new Error(learningError: error, predictionError: error);
        }


    }
}
