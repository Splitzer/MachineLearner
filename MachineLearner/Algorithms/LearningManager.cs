using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MachineLearner.Algorithms
{
    public class LearningManager
    {
        public int NumberOfExamples { get; set; }
        public float MinimunDistance { get; set; }
        public float Range { get; set; }
        public float[] Frequencies { get; set; }
        public float[] NumberOfAppearances { get; set; }
        public float[][] Probabilities { get; set; }
        public float[][] ProbabilitiesSqared { get; set; }

        public LearningManager(DataTable rawData)
        {
            float[] Y;
            float[][] X = DatasetBreakdown(rawData, out Y);

            NumberOfExamples = Y.Length;

            float maxValue = X.Cast<float>().Max();
            int lenghtOfTuple = X[0].Length;

            for (int i = 0; i < maxValue; i++)
            {
                for (int k = 0; k < lenghtOfTuple; k++)
                {

                }
            }
        }

        private float[][] DatasetBreakdown(DataTable rawData, out float[] Y)
        {
            throw new NotImplementedException();
        }

        private void Normalise(float[][] X)
        {
            float[] firstTuple = X[1];
            float[] distances = new float[100];

            for (int i = 0; i < NumberOfExamples; i++)
            {
                distances[i] = CalculateDistance(firstTuple, X[i]);
            }

            float maxDistance = distances.Max();

            MinimunDistance = distances.Min();
            Range = maxDistance - MinimunDistance;
        }

        private float[] LinearRegression(float[][] X, float[] Y)
        {
            float[] W;

            W = Y;

            return W;
        }

        private float CalculateDistance(float[] tuple1, float[] tuple2)
        {
            int sum = 0;
            float distance = 0;
            int lenghtOfTuple = tuple1.Length;

            for (int k = 0; k < lenghtOfTuple; k++)
            {
                if (tuple1[k] == tuple2[k])
                {
                    sum++;
                }
            }

            distance = (sum / lenghtOfTuple) - 1;

            return distance;
        }

        public float[] Prediction(float[] Y, float[][] weights, float[][] kernelWeights)
        {
            float[] Ypred;

            Ypred = Y;

            return Ypred;
        }

        public float[][] KernelWeightCompute(float[][] X, float[][] kernels)
        {
            int numberOfCenters= kernels[0].Length;
            float[][] kernelWeights = new float[numberOfCenters][];

            for (int i = 0; i < NumberOfExamples; i++)
            {
                for (int l = 0; l < numberOfCenters; l++)
                {
                    //kernelWeights[i,l] = CalculateDistance(X[i], kernels[l]);
                }
            }

            return kernelWeights;
        }

        public void KernelCompute(float[][] X, float[][] kernels, float threshold)
        {
            float[] D = new float[100];
            int lenghtOfKernels = kernels[0].Length;

            for (int i = 1; i < NumberOfExamples; i++)
            {
                for (int l = 0; l < lenghtOfKernels; l++)
                {
                    D[l] = CalculateDistance(X[i], kernels[l]);
                }

                if (D.Min() > threshold)
                {
                    kernels = ConcatenateArrays(kernels, X[i]);
                }

                lenghtOfKernels = kernels[0].Length;
            }
        }

        private float[][] ConcatenateArrays(float[][] twoDimentionalArray, float[] array)
        {
            var point = new int[array.Length + twoDimentionalArray[0].Length];
            array.CopyTo(point, 0);
            twoDimentionalArray.CopyTo(point, array.Length);

            return twoDimentionalArray;
        }
    }
}