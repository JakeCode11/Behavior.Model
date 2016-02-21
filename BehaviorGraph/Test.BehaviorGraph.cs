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
    public class TestGraphInput
    {

        #region Test One Strategy and One Trace Step
        /*
         * Solving example : solve 1+1
         * 
         * Trace count: 1
         * Trace : 1+1 -> 2 (apply additional rule)
         */ 
        [Test]
        public void Test_OneStrategy_OneTraceStep()
        {
            //strategy1 : 1+1->2
            var expr1 = new Term(Expression.Add, new List<object>{1,1});
            var ts = new TraceStep(expr1, 2, "null", "meta-rule todo", "rule todo");
            var tsExpr = new TraceStepExpr(ts);
            var lst = new List<TraceStepExpr>() { tsExpr};
            var tuple = new Tuple<object, object>("strategy1", lst);
            var lstStrategy = new List<Tuple<object, object>>();
            lstStrategy.Add(tuple);

            var graph = new BehaviorGraph();
            graph.Insert(lstStrategy);            
            Assert.True(graph.Nodes.Count == 2);

            var node0 = graph.Nodes[0];
            Assert.Null(node0.SubGraph);
            var node1 = graph.Nodes[1];

            Assert.True(node0.OutEdges.Count == 1);
            var edgeInfo = node0.OutEdges[0].Property as OuterLoopEdgeProperty;
            Assert.NotNull(edgeInfo);
            Assert.True(edgeInfo.Strategy.Equals("strategy1"));

            Assert.NotNull(node1.SubGraph);
            Assert.True(node1.InEdges.Count == 1);
            Assert.True(node1.OutEdges.Count == 0);

            Assert.True(node1.SubGraph.Nodes.Count == 2);

            /////////////////////////////////////////////////////
            //Search Test

            var initNode = graph.RetrieveInitInnerNode();
            Assert.NotNull(initNode);
            int count = graph.PathFinding(initNode);
            Assert.True(count == 1);

            var nextObj = graph.SearchNextInnerLoopNode(initNode);
            var tuple2 = nextObj as Tuple<object,object>;
            Assert.NotNull(tuple2);
            var nextNode = tuple2.Item2 as BehaviorGraphNode;
            Assert.NotNull(nextNode);
            count = graph.PathFinding(nextNode);
            Assert.True(count == 0);

            var prevObj = graph.SearchPrevInnerLoopNode(nextNode) as BehaviorGraphNode;
            Assert.NotNull(prevObj);
            count = graph.PathFinding(prevObj);
            Assert.True(count == 1);
        }

        /*
       * Solving example : solve 1+1
       * 
       * Trace count: 1
       * Trace : 1+1 -> 2 (apply additional rule)
       * 
       * Authoring: 1+1-> 3 (Incorrect path) 
       */
        [Test]
        public void Test_OneStrategy_OneTraceStep_Author()
        {
            //1+1->2
            var expr1 = new Term(Expression.Add, new List<object> { 1, 1 });
            var ts = new TraceStep(expr1, 2, "null", "meta-rule todo", "rule todo");
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
            var tsLst = new List<TraceStepExpr>() { ts1Expr };
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
            Assert.True(innerState.UserKnowledge.ToString().Equals("1+1"));

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
        * Solving example : solve 1+1
        * 
        * Trace count: 1
        * Trace : 1+1 -> 2 (apply additional rule)
        */
        [Test]
        public void Test_OneStrategy_OneTraceStep_UserInput()
        {
            var expr1 = new Term(Expression.Add, new List<object> { 1, 1 });
            var ts = new TraceStep(expr1, 2, null, "meta-rule todo", "rule todo");
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

            // User Input
            var userExpr1 = new Term(Expression.Add, new List<object> { 1, 1 });
            BehaviorGraphNode matchedNode;
            var node = graph.SearchInnerLoopNode(userExpr1);
            Assert.NotNull(node);

            var userExpr2 = new Term(Expression.Add, new List<object> { 1, 2 });
            node = graph.SearchInnerLoopNode(userExpr2);
            Assert.Null(node);
        }

        #endregion

        #region One Strategy and Multiple Trace Step

        /*
         * Solving example : solve 1+1+1
         * 
         * Trace count: 2
         * Trace : 1+1+1 -> 2+1 -> 3 (apply additional rule)
         */ 
        [Test]
        public void Test_OneStrategy_MultiTraceStep()
        {
            var expr1 = new Term(Expression.Add, new List<object> { 1, 1 , 1});
            var expr2 = new Term(Expression.Add, new List<object> {2, 1} );
            var ts1 = new TraceStep(expr1, expr2, "null", "meta-rule todo", "rule todo");
            var ts1Expr = new TraceStepExpr(ts1);
            var ts2 = new TraceStep(expr2, 2, "null", "meta-rule todo", "rule todo");
            var ts2Expr = new TraceStepExpr(ts2);
            var lst = new List<TraceStepExpr>() { ts1Expr,ts2Expr};
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
            Assert.True(node1.OutEdges.Count == 0);

            /////////////////////////////////////////////////////
            //Search Test

            var initNode = graph.RetrieveInitInnerNode();
            Assert.NotNull(initNode);
            int count = graph.PathFinding(initNode);
            Assert.True(count == 2);

            var nextObj = graph.SearchNextInnerLoopNode(initNode);
            var tuple2 = nextObj as Tuple<object, object>;
            Assert.NotNull(tuple2);
            var nextNode = tuple2.Item2 as BehaviorGraphNode;
            Assert.NotNull(nextNode);
            count = graph.PathFinding(nextNode);
            Assert.True(count == 1);

            var nextObj2 = graph.SearchNextInnerLoopNode(nextNode);
            var tuple3 = nextObj2 as Tuple<object, object>;
            Assert.NotNull(tuple3);
            var nextNode2 = tuple3.Item2 as BehaviorGraphNode;
            Assert.NotNull(nextNode2);
            count = graph.PathFinding(nextNode2);
            Assert.True(count == 0);
        }

        [Test]
        public void Test_MultiStrategy_1()
        {
            //strategy1: 1->2, 2->3
            var ts1 = new TraceStep(1, 2, "null", "TODO1", "TODO2");
            var ts1Expr = new TraceStepExpr(ts1);
            var ts2 = new TraceStep(2, 3, "null", "TODO2", "TODO3");
            var ts2Expr = new TraceStepExpr(ts2);
            var lst1 = new List<TraceStepExpr>() { ts1Expr, ts2Expr };
            var tuple1 = new Tuple<object, object>("strategy1", lst1);

            //strategy2: 2->3, 3->5, 5->7
            var ts3 = new TraceStep(2, 3, null, "TODO", "TODO1");
            var ts3Expr = new TraceStepExpr(ts3);
            var ts4 = new TraceStep(3, 5, null, "TODO2", "TODO5");
            var ts4Expr = new TraceStepExpr(ts4);
            var ts5 = new TraceStep(5, 7, null,"Test1", "test23");
            var ts5Expr = new TraceStepExpr(ts5);
            var lst2 = new List<TraceStepExpr>() {ts3Expr, ts4Expr, ts5Expr};
            var tuple2 = new Tuple<object, object>("strategy2", lst2);

            var lstStrategy = new List<Tuple<object, object>>();
            lstStrategy.Add(tuple1);
            lstStrategy.Add(tuple2);

            var graph = new BehaviorGraph();
            graph.Insert(lstStrategy);
            Assert.True(graph.Nodes.Count == 3);

            var node1 = graph.Nodes[1];
            Assert.True(node1.OutEdges.Count == 1);
            var node2 = graph.Nodes[2];
            Assert.True(node2.InEdges.Count == 1);
            Assert.True(node2.OutEdges.Count == 0);

            Assert.NotNull(node1.SubGraph);
            Assert.True(node1.SubGraph.Nodes.Count == 3);

            Assert.NotNull(node2.SubGraph);
            Assert.True(node2.SubGraph.Nodes.Count == 4);

            /////////////////////////////////////////////////////
            //Search Test

            var initNode = graph.RetrieveInitInnerNode();
            Assert.NotNull(initNode);
            int count = graph.PathFinding(initNode);
            Assert.True(count == 5);

            var nextObj = graph.SearchNextInnerLoopNode(initNode);
            var tuple5 = nextObj as Tuple<object, object>;
            Assert.NotNull(tuple5);
            var nextNode = tuple5.Item2 as BehaviorGraphNode;
            Assert.NotNull(nextNode);
            count = graph.PathFinding(nextNode);
            Assert.True(count == 4);

            nextObj = graph.SearchNextInnerLoopNode(nextNode);
            tuple5 = nextObj as Tuple<object, object>;
            Assert.NotNull(tuple5);
            nextNode = tuple5.Item2 as BehaviorGraphNode;
            Assert.NotNull(nextNode);
            count = graph.PathFinding(nextNode);
            Assert.True(count == 3);

            var index = graph.SearchOuterLoopNodeIndex(nextNode);
            Assert.True(index == 1);

            nextObj = graph.SearchNextInnerLoopNode(nextNode);
            tuple5 = nextObj as Tuple<object, object>;
            Assert.NotNull(tuple5);
            nextNode = tuple5.Item2 as BehaviorGraphNode;
            Assert.NotNull(nextNode);
            count = graph.PathFinding(nextNode);
            Assert.True(count == 2);

            index = graph.SearchOuterLoopNodeIndex(nextNode);
            Assert.True(index == 2);

            nextObj = graph.SearchNextInnerLoopNode(nextNode);
            tuple5 = nextObj as Tuple<object, object>;
            Assert.NotNull(tuple5);
            nextNode = tuple5.Item2 as BehaviorGraphNode;
            Assert.NotNull(nextNode);
            count = graph.PathFinding(nextNode);
            Assert.True(count == 1);

            index = graph.SearchOuterLoopNodeIndex(nextNode);
            Assert.True(index == 2);

            nextObj = graph.SearchNextInnerLoopNode(nextNode);
            tuple5 = nextObj as Tuple<object, object>;
            Assert.NotNull(tuple5);
            nextNode = tuple5.Item2 as BehaviorGraphNode;
            Assert.NotNull(nextNode);
            count = graph.PathFinding(nextNode);
            Assert.True(count == 0);

            index = graph.SearchOuterLoopNodeIndex(nextNode);
            Assert.True(index == 2);

            var prevNode = graph.SearchPrevInnerLoopNode(nextNode) as BehaviorGraphNode;
            Assert.NotNull(prevNode);
            count = graph.PathFinding(prevNode);
            Assert.True(count == 1);

            var strateties = graph.SearchAllOuterEdgeInfos();
            Assert.True(strateties.Count == 2);
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
        public void Test_OneStrategy_MultiTraceStep_Author()
        {
            var expr1 = new Term(Expression.Add, new List<object> { 1, 1, 1 });
            var expr2 = new Term(Expression.Add, new List<object> { 2, 1 });
            var ts1 = new TraceStep(expr1, expr2, null, "meta-rule todo", "rule todo");
            var ts1Expr = new TraceStepExpr(ts1);
            var ts2 = new TraceStep(expr2, 2, null, "meta-rule todo", "rule todo");
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
            var ts3 = new TraceStep(expr1, expr3, null, "meta-rule todo", "rule todo");
            var ts3Expr = new TraceStepExpr(ts3);
            var ts4 = new TraceStep(expr3, 2, null, "meta-rule todo", "rule todo");
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

        /*
      * Solving example : solve 1+1+1
      * 
      * Trace count: 2
      * Trace : 1+1+1 -> 2+1 -> 3 (apply additional rule)
      */
        [Test]
        public void Test_OneStrategy_MultiTraceStep_UserInput()
        {
            var expr1 = new Term(Expression.Add, new List<object> { 1, 1, 1 });
            var eq1 = new Equation(expr1, 20);
            var expr2 = new Term(Expression.Add, new List<object> { 2, 1 });
            var ts1 = new TraceStep(eq1, expr2, null, "meta-rule todo", "rule todo");
            var ts1Expr = new TraceStepExpr(ts1);
            var ts2 = new TraceStep(expr2, 2, null, "meta-rule todo", "rule todo");
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



        #endregion


    }
}