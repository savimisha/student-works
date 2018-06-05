using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReactDiff
{
    public class ImplicitReactDiff: ReactDiffSolving
    {
        public ImplicitReactDiff(Parameters p)
            : base(p)
        { }
        public override void solve()
        {
            double[] AlphaCoeff = new double[n + 1];
            double[] BettaCoeff = new double[n + 1];

            AlphaCoeff[0] = 1;
            BettaCoeff[0] = 0;
            double tmp1, tmp2;
            for (int j = 0; j < maxStepsT; j++)
            {
                for (int i = 1; i < n; i++)
                {
                    tmp1 = 2 * lambda_1 + h * h / k - AlphaCoeff[i - 1] * lambda_1;
                    AlphaCoeff[i] = lambda_1 / tmp1;
                    tmp2 = h * h * (v1[i, j] / k + alpha - gamma * v1[i, j] +
                           betta * Math.Pow(v1[i, j], 2) / v2[i, j]);
                    BettaCoeff[i] = (tmp2 + BettaCoeff[i - 1] * lambda_1) / tmp1;
                }
                v1[n, j + 1] = BettaCoeff[n - 1] / (-AlphaCoeff[n - 1] + 1);
                for (int i = n; i > 0; i--)
                    v1[i - 1, j + 1] = AlphaCoeff[i - 1] * v1[i, j + 1] + BettaCoeff[i - 1];
                for (int i = 1; i < n; i++)
                {
                    tmp1 = 2 * lambda_2 + h * h / k - AlphaCoeff[i - 1] * lambda_2;
                    AlphaCoeff[i] = lambda_2 / tmp1;
                    tmp2 = h * h * (v2[i, j] / k - nu * v2[i, j] +
                           delta * Math.Pow(v1[i, j], 2));
                    BettaCoeff[i] = (tmp2 + BettaCoeff[i - 1] * lambda_2) / tmp1;
                }
                v2[n, j + 1] = BettaCoeff[n - 1] / (-AlphaCoeff[n - 1] + 1);
                for (int i = n; i > 0; i--)
                    v2[i - 1, j + 1] = AlphaCoeff[i - 1] * v2[i, j + 1] + BettaCoeff[i - 1];
            }
            inAllComputedLayers += maxStepsT;
            maxStepsTCopy = maxStepsT;
        }
        public override void ComputeNextLayer()
        {
            throw new NotImplementedException();
        }
    }
}
