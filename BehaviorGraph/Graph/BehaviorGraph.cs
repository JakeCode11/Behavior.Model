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

using System.Reflection;
using AlgebraGeometry;
using CSharpLogic;
using NUnit.Framework;



namespace UserModeling
{
    using System.Linq;
    using MathCog;
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;

    public partial class BehaviorGraph
    {
        #region Properties

        private List<BehaviorGraphNode> _nodes;
        public List<BehaviorGraphNode> Nodes
        {
            get { return _nodes; }
            set { _nodes = value; }
        }

        /// <summary>
        /// Key: Pattern Match Result
        /// Value: Predict Pattern Match Result (GraphNode)
        /// </summary>
        private Dictionary<BehaviorGraphNode, bool> _solvingCache;
        public Dictionary<BehaviorGraphNode, bool> SolvingCache
        {
            get { return _solvingCache; }
        }

        public SolvingStatus CurrentSolvingStatus { get; set; }

        public BehaviorGraph()
        {
            _nodes = new List<BehaviorGraphNode>();
            _solvingCache = new Dictionary<BehaviorGraphNode, bool>();
            CurrentSolvingStatus = SolvingStatus.None;
        }

        #endregion

        #region Behavior Graph API

        public void Insert(IEnumerable<Tuple<object, object>> kt)
        {
            //start node
            var startState =
                new BehaviorGraphNode(new OuterLoopBehaviorState());
            _nodes.Add(startState);

            Debug.Assert(kt != null);

            BehaviorGraphNode source = startState;
            foreach (var tuple in kt)
            {
                var strategy = tuple.Item1 as string;
                var tsExpr = tuple.Item2 as List<TraceStepExpr>;
                Debug.Assert(strategy != null);
                Debug.Assert(tsExpr != null);

                var traceSteps = tsExpr.Select(tse => tse.TraceStep).ToList();
                var outerEdgeProp = new OuterLoopEdgeProperty(strategy);
                var nodeState = new OuterLoopBehaviorState(traceSteps);
                var target = new BehaviorGraphNode(nodeState);
                _nodes.Add(target);

                var newEdge = new BehaviorGraphEdge(source, target, outerEdgeProp);
                source.OutEdges.Add(newEdge);
                target.InEdges.Add(newEdge);

                source = target;
            }
        }

        //multiple strategy
        public void Update(IEnumerable<Tuple<object, object>> traces)
        {
            Debug.Assert(Nodes[0] != null);
            BehaviorGraphNode source = Nodes[0];
            foreach (var tuple in traces)
            {
                var strategy = tuple.Item1 as string;
                var tsExpr = tuple.Item2 as List<TraceStepExpr>;
                Debug.Assert(strategy != null);
                Debug.Assert(tsExpr != null);

                var traceSteps = tsExpr.Select(tse => tse.TraceStep).ToList();

                var outerNode = SearchOuterLoopNode(strategy);
                if (outerNode == null)
                {
                    var outerEdgeProp = new OuterLoopEdgeProperty(strategy);
                    var nodeState = new OuterLoopBehaviorState(traceSteps);
                    outerNode = new BehaviorGraphNode(nodeState);
                    _nodes.Add(outerNode);
                    var newEdge = new BehaviorGraphEdge(source, outerNode, outerEdgeProp);
                    source.OutEdges.Add(newEdge);
                    outerNode.InEdges.Add(newEdge);
                    source = outerNode;
                }
                else
                {
                    Update(tsExpr, outerNode);
                }               
            }
        }

        private void Update(IEnumerable<TraceStepExpr> steps, BehaviorGraphNode outerLoopNode)
        {
            Debug.Assert(outerLoopNode != null);           
        
            foreach (var step in steps)
            {
                Update(step, outerLoopNode);
            }
        }

        private void Update(TraceStepExpr tsExpr, BehaviorGraphNode outerLoopNode = null)
        {
            var ts = tsExpr.TraceStep;
            Debug.Assert(ts != null);

            BehaviorGraphNode gn1 = SearchInnerLoopNode(ts.Source);
            BehaviorGraphNode gn2 = SearchInnerLoopNode(ts.Target);

            if (gn1 != null)
            {
                if (gn2 != null)
                {
                    ConnectTwoNodes(tsExpr, gn1, gn2);
                }
                else
                {
                    ConnectPrevNode(tsExpr, gn1);
                }
            }
            else
            {
                Debug.Assert(gn1 == null);
                if (gn2 != null)
                {
                    ConnectNextNode(tsExpr, gn2);
                }
                else
                {
                    if (outerLoopNode == null) return;
                    ConnectRootNode(tsExpr, outerLoopNode);
                }
            }
        }

        private void ConnectTwoNodes(TraceStepExpr tsExpr, BehaviorGraphNode gn1, BehaviorGraphNode gn2)
        {
            bool findTarget = false;
            foreach (var edge in gn1.OutEdges)
            {
                if (edge.Target == null) continue;

                if (edge.Target.Equals(gn2))
                {
                    findTarget = true;
                }
            }

            if (!findTarget)
            {
                InnerLoopEdgeProperty newEdgeProp1;
                if (tsExpr.TraceStep.WrongStep)
                {
                    newEdgeProp1 = new InnerLoopEdgeProperty();
                }
                else
                {
                    newEdgeProp1 = new InnerLoopEdgeProperty((string)tsExpr.TraceStep.Rule, (string)tsExpr.TraceStep.AppliedRule);
                }
                var newEdge1 = new BehaviorGraphEdge(gn1, gn2, newEdgeProp1);

                gn1.OutEdges.Add(newEdge1);
                gn2.InEdges.Add(newEdge1);
            }
        }

        private void ConnectPrevNode(TraceStepExpr tsExpr, BehaviorGraphNode gn1)
        {
            var newNodeProp = new InnerLoopBehaviorState(tsExpr.TraceStep.Target);
            var newNode = new BehaviorGraphNode(newNodeProp);

            InnerLoopEdgeProperty newEdgeProp;
            if (tsExpr.TraceStep.WrongStep)
            {
                newEdgeProp = new InnerLoopEdgeProperty();
            }
            else
            {
                newEdgeProp = new InnerLoopEdgeProperty((string)tsExpr.TraceStep.Rule, (string)tsExpr.TraceStep.AppliedRule);
            }
            var newEdge = new BehaviorGraphEdge(gn1, newNode, newEdgeProp);
            /*  foreach (var edge in gn1.OutEdges)
            {
                var nextNode = edge.Target;
                nextNode.InEdges.Add(newEdge);
            }*/
            gn1.OutEdges.Add(newEdge);
            newNode.InEdges.Add(newEdge);
            BehaviorGraphNode parentNode = SearchOuterLoopNode(gn1);
            parentNode.SubGraph.Nodes.Add(newNode);
        }

        private void ConnectNextNode(TraceStepExpr tsExpr, BehaviorGraphNode gn2)
        {
            var newNodeProp = new InnerLoopBehaviorState(tsExpr.TraceStep.Source);
            var newNode = new BehaviorGraphNode(newNodeProp);
            InnerLoopEdgeProperty newEdgeProp;
            if (tsExpr.TraceStep.WrongStep)
            {
                newEdgeProp = new InnerLoopEdgeProperty();
            }
            else
            {
                newEdgeProp = new InnerLoopEdgeProperty((string)tsExpr.TraceStep.Rule, (string)tsExpr.TraceStep.AppliedRule);
            }
            var newEdge = new BehaviorGraphEdge(newNode, gn2, newEdgeProp);
            /*foreach (var edge in gn2.InEdges)
            {
                var prevNode = edge.Source;
                prevNode.OutEdges.Add(newEdge);
            }*/
            gn2.InEdges.Add(newEdge);
            newNode.OutEdges.Add(newEdge);
            BehaviorGraphNode parentNode = SearchOuterLoopNode(gn2);
            parentNode.SubGraph.Nodes.Add(newNode);
        }

        private void ConnectRootNode(TraceStepExpr tsExpr, BehaviorGraphNode parentNode)
        {
            Debug.Assert(parentNode != null);

            var newNodeProp1 = new InnerLoopBehaviorState(tsExpr.TraceStep.Source);
            var newNode1 = new BehaviorGraphNode(newNodeProp1);

            var newNodeProp2 = new InnerLoopBehaviorState(tsExpr.TraceStep.Target);
            var newNode2 = new BehaviorGraphNode(newNodeProp2);

            InnerLoopEdgeProperty newEdgeProp;
            if (tsExpr.TraceStep.WrongStep)
            {
                newEdgeProp = new InnerLoopEdgeProperty();
            }
            else
            {
                newEdgeProp = new InnerLoopEdgeProperty((string)tsExpr.TraceStep.Rule, (string)tsExpr.TraceStep.AppliedRule);
            }
            var newEdge = new BehaviorGraphEdge(newNode1, newNode2, newEdgeProp);

            newNode1.OutEdges.Add(newEdge);
            newNode2.InEdges.Add(newEdge);

            parentNode.SubGraph.Nodes.Add(newNode1);
            parentNode.SubGraph.Nodes.Add(newNode2);
        }

        #endregion

        #region Pattern Match API

        public bool Match(List<Tuple<object, object>> lst)
        {
            bool result = false;
            foreach (Tuple<object, object> tse in lst)
            {
                bool tempResult = Match(tse);
                if (tempResult) result = true;
            }
            return result;
        }

        public bool Match(Tuple<object, object> tuple)
        {
            var steps = tuple.Item2 as List<TraceStepExpr>;
            Debug.Assert(steps != null);
            return Match(steps);
        }

        public bool Match(List<TraceStepExpr> lst)
        {
            bool result = false;
            foreach (TraceStepExpr tse in lst)
            {
                bool tempResult = Match(tse);
                if (tempResult) result = true;
            }
            return result;
        }

        public bool Match(TraceStepExpr tsExpr)
        {
            var ts = tsExpr.TraceStep;
            Debug.Assert(ts != null);

            BehaviorGraphNode gn1 = SearchInnerLoopNode(ts.Source);
            BehaviorGraphNode gn2 = SearchInnerLoopNode(ts.Target);
            if (gn1 == null && gn2 == null) return false;
            return true;
        }

        #endregion

        #region User Pattern Match API

        public BehaviorGraphNode UpdateSolvingCache(IKnowledge userKnowledge, bool isAdd = true)
        {
            var node = SearchInnerLoopNode(userKnowledge);
            if (node == null) return null;
            Debug.Assert(node.State.Level == BehaviorLevel.Inner);
      /*      if (node.OutEdges.Count != 0) return node;
            Debug.Assert(node.OutEdges.Count == 0);*/
            bool contained = SolvingCache.ContainsKey(node);
            //Debug.Assert(contained);
            if (!contained) return node;
            if (isAdd)
            {
                SolvingCache[node] = true;                
            }
            else
            {
                SolvingCache[node] = false;
            }
            return node;
        }

        public bool IsPartialCorrect()
        {
            foreach (var hitone in SolvingCache.Values)
            {
                if (!hitone) return true;
            }
            return false;
        }

        public enum SolvingStatus
        {
            None,
            Partial,
            Complete
        }

        public int FindGoalIndex()
        {
            for (int i = 0; i < SolvingCache.Count; i++)
            {
                if (!SolvingCache.Values.ElementAt(i))
                {
                    return i;
                }
            }
            return 0;
        }

        public SolvingStatus CheckSolvingStatus()
        {
            var valueLst = SolvingCache.Values;
            int totalCount = valueLst.Count;
            int correctCount = (from val in valueLst where val select true).ToList().Count;

            if (correctCount == 0)
            {
                return SolvingStatus.None;
            }
            if (correctCount < totalCount)
            {
                return SolvingStatus.Partial;
            }
            if (correctCount == totalCount)
            {
                return SolvingStatus.Complete;
            }
            return SolvingStatus.None;
        }

        #endregion

    }
}