/*******************************************************************************
 * Copyright (c) 2015 Bo Kang
 *   
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *  
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/

using UserModeling;

namespace MathCogUserData
{
    using System;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using CSharpLogic;
    using System.Collections.Generic;
    using MathCog;
    using NUnit.Framework;

    /*
     * Simplify the expression x+1+x-4?
     * 
     */
    public static partial class ProblemAuthoringModel
    {
        public static BehaviorGraph Problem97()
        {
            const string fact1 = "x+1+x-4=";
            var queryExpr = Reasoner.Instance.Load(fact1) as AGQueryExpr;
            Assert.NotNull(queryExpr);
            queryExpr.RetrieveRenderKnowledge();
            Assert.True(queryExpr.RenderKnowledge.Count == 1);

            var agEquationExpr = queryExpr.RenderKnowledge[0] as AGEquationExpr;
            Assert.True(agEquationExpr != null);
            agEquationExpr.IsSelected = true;

            agEquationExpr.GenerateSolvingTrace();
            Assert.True(agEquationExpr.AutoTrace != null);
            Assert.True(agEquationExpr.AutoTrace.Count == 1);

            var graph = new BehaviorGraph();
            graph.Insert(agEquationExpr.AutoTrace);
            var answerNode1 = graph.SearchInnerLoopNode(agEquationExpr.Equation.Rhs);
            Assert.NotNull(answerNode1);
            graph.SolvingCache.Add(answerNode1, false);
            return graph;

         /*   ////////////////Authoring Below //////////////////////////////

            Debug.Assert(graph.Nodes.Count == 2);

            //add another strategy to solve the same problem
            //so it add onto the outer loop side
            //x+1+x-4 -> 1-4+x+x -> -3+2x -> 2x-3

            var x = new Var('x');
            var innerTerm = new Term(Expression.Multiply, new List<object>() {2, x});
            var rhs = new Term(Expression.Add, new List<object>() { innerTerm, -3 });
            var eq1Lhs = new Term(Expression.Add, new List<object>() {x, 1, x, -4});
            var eq1 = new Equation(eq1Lhs, rhs);
            var eq2Lhs = new Term(Expression.Add, new List<object>() {1, -4, x, x});
            var eq2 = new Equation(eq2Lhs, rhs);
            var eq3Lhs = new Term(Expression.Add, new List<object>() {-3, innerTerm});
            var eq3 = new Equation(eq3Lhs, rhs);
            var eq4 = new Equation(rhs, rhs);

            var ts1 = new TraceStep(eq1, eq2, "TODO", "TODO");
            var ts1Expr = new TraceStepExpr(ts1);

            var ts2 = new TraceStep(eq2, eq3, "TODO", "TODO");
            var ts2Expr = new TraceStepExpr(ts2);

            var ts3 = new TraceStep(eq3, eq4, "TODO", "TODO");
            var ts3Expr = new TraceStepExpr(ts3);

            var lst = new List<TraceStepExpr>() {ts1Expr, ts2Expr, ts3Expr};

            var tuple = new Tuple<object, object>(AlgebraRule.AlgebraicStrategy, lst);
            var lstTuple = new List<Tuple<object, object>>() { tuple };

            graph.Update(lstTuple);*/
           
        }
    }
}
