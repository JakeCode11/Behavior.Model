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

using MathCog;

namespace UserModeling
{
    using System.Linq.Expressions;
    using CSharpLogic;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class TestGraphUserInput
    {
        /*
        * Solving example : solve 1+1
        * 
        * Trace count: 1
        * Trace : 1+1 -> 2 (apply additional rule)
        */
        [Test]
        public void Test_OneStrategy_OneTraceStep()
        {
            var expr1 = new Term(Expression.Add, new List<object> { 1, 1 });
            var ts = new TraceStep(expr1, 2, "meta-rule todo", "rule todo");
            var tsExpr = new TraceStepExpr(ts);
            var lst = new List<TraceStepExpr>() { tsExpr };
            var tuple = new Tuple<object, object>("strategy1", lst);
            var lstStrategy = new List<Tuple<object, object>>();
            lstStrategy.Add(tuple);

            var graph = new BehaviorGraph();
            graph.Insert(lstStrategy);
            Assert.True(graph.Nodes.Count == 2);

            var node0 = graph.Nodes[0];
            Assert.Null(node0.SubGraph);
            var node1 = graph.Nodes[1];
            Assert.NotNull(node1.SubGraph);

            Assert.True(node1.SubGraph.Nodes.Count == 2);

            /////////////////////////////////////////////

            // Authoring Input, //wrong step
           /* var ts1 = new TraceStep(expr1, 3);
            var ts1Expr = new TraceStepExpr(ts1);
            graph.Insert(ts1Expr);*/
            
            /////////////////////////////////////////////

            // User Input
            var userExpr1 = new Term(Expression.Add, new List<object> { 1, 1 });
            BehaviorGraphNode matchedNode;
            var node = graph.SearchInnerLoopNode(userExpr1);
            Assert.NotNull(node);
           
            var userExpr2 = new Term(Expression.Add, new List<object> { 1, 2 });
            node = graph.SearchInnerLoopNode(userExpr2);
            Assert.Null(node);
        }

        /*
        * Solving example : solve 1+1+1
        * 
        * Trace count: 2
        * Trace : 1+1+1 -> 2+1 -> 3 (apply additional rule)
        */
        [Test]
        public void Test_OneStrategy_MultiTraceStep()
        {
            var expr1 = new Term(Expression.Add, new List<object> { 1, 1, 1 });
            var eq1 = new Equation(expr1, 20);
            var expr2 = new Term(Expression.Add, new List<object> { 2, 1 });
            var ts1 = new TraceStep(eq1, expr2, "meta-rule todo", "rule todo");
            var ts1Expr = new TraceStepExpr(ts1);
            var ts2 = new TraceStep(expr2, 2, "meta-rule todo", "rule todo");
            var ts2Expr = new TraceStepExpr(ts2);
            var lst = new List<TraceStepExpr>() { ts1Expr, ts2Expr };
            var tuple = new Tuple<object, object>("strategy1", lst);
            var lstStrategy = new List<Tuple<object, object>>();
            lstStrategy.Add(tuple);

            var graph = new BehaviorGraph();
            graph.Insert(lstStrategy);
            Assert.True(graph.Nodes.Count == 2);

            var node0 = graph.Nodes[0];
            Assert.Null(node0.SubGraph);
            var node1 = graph.Nodes[1];
            Assert.NotNull(node1.SubGraph);

            Assert.True(node1.SubGraph.Nodes.Count == 3);

            /////////////////////////////////////////////
            // User Input
            var userExpr1 = new Term(Expression.Add, new List<object> { 1, 1, 1 });
            var userEq1 = new Equation(userExpr1, 20);
            var node = graph.SearchInnerLoopNode(userEq1);
            Assert.NotNull(node);

            var userExpr2 = new Term(Expression.Add, new List<object> { 1, 3 });
            node = graph.SearchInnerLoopNode(userExpr2);
            Assert.Null(node);
        }
    }
}
