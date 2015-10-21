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

namespace MathCogUserData
{
    using System;
    using System.Diagnostics;
    using System.Linq.Expressions;
    using CSharpLogic;
    using System.Collections.Generic;
    using MathCog;
    using NUnit.Framework;
    using UserModeling;
    /*
     * Simplify the expression 1+2+3?
     */

    public static partial class ProblemAuthoringModel
    {
        /// <summary>
        /// Refer to MathCog -> Test/Problem/Test.Problem.99.cs
        /// </summary>
        /// <param name="knolwedgeTrace">back-end solving trace.</param>
        /// <returns></returns>
        public static BehaviorGraph Problem99()
        {
            const string fact1 = "1+2+3=";
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


/*
            //////////////////////////////////////////////////////////////

            Reasoner.Instance.Reset();
            
            //////////////////////////////////////////////////////////////

            var graph = new BehaviorGraph();
            graph.Insert(agEquationExpr.AutoTrace);

            ////////////////Authoring Below //////////////////////////////
            
           
            Debug.Assert(graph.Nodes.Count == 2);
            Debug.Assert(graph.Nodes[0].OutEdges.Count == 1);

            //add another strategy to solve the same problem
            //so it add onto the outer loop side

            var term1 = new Term(Expression.Add, new List<object> {1,2,3});
            var eq1 = new Equation(term1, 6);

            var term2 = new Term(Expression.Add, new List<object>() {1, 5});
            var eq2 = new Equation(term2, 6);

            var metaRule = AlgebraRule.Rule(AlgebraRule.AlgebraRuleType.Associative);
            var appliedRule = AlgebraRule.Rule(AlgebraRule.AlgebraRuleType.Associative, eq1, eq2);
            var ts1 = new TraceStep(eq1, eq2, metaRule, appliedRule);
            var ts1Expr = new TraceStepExpr(ts1);
            var eq3 = new Equation(6, 6);
            var ts2 = new TraceStep(eq2, eq3, "TODO", "TODO");
            var ts2Expr = new TraceStepExpr(ts2);
            var lst = new List<TraceStepExpr>() {ts1Expr, ts2Expr};
            var tuple = new Tuple<object, object>(ArithRule.ArithmeticStrategy, lst);
            var lstTuple = new List<Tuple<object, object>>() {tuple};

            graph.Update(lstTuple);*/
      
        }
    }
}