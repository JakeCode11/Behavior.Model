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
    public class TestGraphAuthoring
    {
        /*
         * Solving example : solve 1+1
         * 
         * Trace count: 1
         * Trace : 1+1 -> 2 (apply additional rule)
         * 
         * Authoring: 1+1-> 3 (Incorrect path) 
         */
        [Test]
        public void Test_OneStrategy_OneTraceStep()
        {
            //1+1->2
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

            //4-1->2
            var expr2 = new Term(Expression.Add, new List<object> { 4, 1 });
            // User Input, //wrong step
            var ts1 = new TraceStep(expr2, 2); 
            var ts1Expr = new TraceStepExpr(ts1);
            var tsLst = new List<TraceStepExpr>() {ts1Expr};
            var tuple2 = new Tuple<object, object>("strategy1", tsLst);
            var lst44 = new List<Tuple<object, object>>() { tuple2 };

            bool matchResult = graph.Match(lst44);
            Assert.True(matchResult);
            graph.Update(lst44);

            Assert.True(graph.Nodes.Count == 2);
            Assert.True(node1.SubGraph.Nodes.Count == 3);

            /////////////////////////////////////////////
            
            //test search
            var initNode = graph.RetrieveInitInnerNode();
            Assert.NotNull(initNode);

            var innerState = initNode.State as InnerLoopBehaviorState;
            Assert.NotNull(innerState);
            Assert.True(innerState.UserKnowledge.ToString().Equals("(1+1)"));

            Assert.True(initNode.InEdges.Count == 0);
            Assert.True(initNode.OutEdges.Count == 1);

            int count = graph.PathFinding(initNode);
            Assert.True(count == 1);

            var nextObj = graph.SearchNextInnerLoopNode(initNode);
            var tuple22 = nextObj as Tuple<object, object>;
            Assert.NotNull(tuple22);
            var nextNode = tuple22.Item2 as BehaviorGraphNode;
            Assert.NotNull(nextNode);
            count = graph.PathFinding(nextNode);
            Assert.True(count == 0);
        }

        /*
         * Solving example : solve 1+1+1
         * 
         * Trace count: 2
         * Trace : 1+1+1 -> 2+1 -> 3 (apply additional rule)
         * 
         * User Trace: 1+1+1-> 1+2 -> 3
         */
        [Test]
        public void Test_OneStrategy_MultiTraceStep()
        {
            var expr1 = new Term(Expression.Add, new List<object> { 1, 1, 1 });
            var expr2 = new Term(Expression.Add, new List<object> { 2, 1 });
            var ts1 = new TraceStep(expr1, expr2, "meta-rule todo", "rule todo");
            var ts1Expr = new TraceStepExpr(ts1);
            var ts2 = new TraceStep(expr2, 2, "meta-rule todo", "rule todo");
            var ts2Expr = new TraceStepExpr(ts2);
            var lst = new List<TraceStepExpr>() { ts1Expr, ts2Expr };
            var tuple = new Tuple<object, object>("strategy2", lst);
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

            // User Input, //2 steps trace 
            var expr3 = new Term(Expression.Add, new List<object> { 1, 2 });
            var ts3 = new TraceStep(expr1, expr3, "meta-rule todo", "rule todo");
            var ts3Expr = new TraceStepExpr(ts3);
            var ts4 = new TraceStep(expr3, 2, "meta-rule todo", "rule todo");
            var ts4Expr = new TraceStepExpr(ts4);
            var lst2 = new List<TraceStepExpr>() { ts3Expr, ts4Expr };

            var tuple2 = new Tuple<object, object>("strategy1", lst2);
            var lstStrategy2 = new List<Tuple<object, object>>();
            lstStrategy2.Add(tuple2);

            //Under the same strategy
            graph.Update(lstStrategy2);
            Assert.True(graph.Nodes.Count == 3);
            Assert.True(node1.SubGraph.Nodes.Count == 3);
        }
    }
}
