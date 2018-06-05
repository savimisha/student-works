using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReactDiff
{
    public class ExplicitReactDiff: ReactDiffSolving
    {
        public ExplicitReactDiff(Parameters p) : base(p) { }
        public override void solve()
        {
            for (int j = 0; j < maxStepsT; j++)
            {
                for (int i = 1; i < n; i++)
                {
                    v1[i, j + 1] = k * (lambda_1 * (v1[i - 1, j] - 2 * v1[i, j] + v1[i + 1, j]) / (h * h) + alpha + betta * Math.Pow(v1[i, j], 2) / v2[i, j] - gamma * v1[i, j]) + v1[i, j];
                    v2[i, j + 1] = k * (lambda_2 * (v2[i - 1, j] - 2 * v2[i, j] + v2[i + 1, j]) / (h * h) + delta * Math.Pow(v1[i, j], 2) - nu * v2[i, j]) + v2[i, j];
                }

                v1[0, j + 1] = v1[1, j + 1];
                v1[n, j + 1] = v1[n - 1, j + 1];
                v2[0, j + 1] = v2[1, j + 1];
                v2[n, j + 1] = v2[n - 1, j + 1]; 

                /*v1[0, j + 1] = (4 * v1[1, j] - v1[2, j]) / 3;
                v1[n, j + 1] = (4 * v1[n - 1, j] - v1[n - 2, j]) / 3;
                v2[0, j + 1] = (4 * v2[1, j] - v2[2, j]) / 3;
                v2[n, j + 1] = (4 * v2[n - 1, j] - v2[n - 2, j]) / 3;*/
            }
            inAllComputedLayers += maxStepsT;
            maxStepsTCopy = maxStepsT;
        }
        public override void ComputeNextLayer()
        {
            int j = computedLayers + 1;
            for (int i = 1; i < n; i++)
            {
                v1[i, j + 1] = k * (lambda_1 * (v1[i - 1, j] - 2 * v1[i, j] + v1[i + 1, j]) / (h * h) + alpha + betta * Math.Pow(v1[i, j], 2) / v2[i, j] - gamma * v1[i, j]) + v1[i, j];
                v2[i, j + 1] = k * (lambda_2 * (v2[i - 1, j] - 2 * v2[i, j] + v2[i + 1, j]) / (h * h) + delta * Math.Pow(v1[i, j], 2) - nu * v2[i, j]) + v2[i, j];
            }

            v1[0, j + 1] = v1[1, j + 1];
            v1[n, j + 1] = v1[n - 1, j + 1];
            v2[0, j + 1] = v2[1, j + 1];
            v2[n, j + 1] = v2[n - 1, j + 1];
            computedLayers++;
        }
    }
}
