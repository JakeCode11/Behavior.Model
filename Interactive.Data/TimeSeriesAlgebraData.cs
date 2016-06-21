using System;
using CSharpLogic;
using starPadSDK.MathExpr;

namespace MathCog.Data
{
    [Serializable]
    public class TimeSeriesAlgebraData : TimeSeriesData
    {
        public Expr AlgebraExpr { get; set; }
        public TimeSeriesAlgebraData(Expr expr, DateTime dt)
        {
            AlgebraExpr = expr;
            InputTime = dt;
        }
    }
}