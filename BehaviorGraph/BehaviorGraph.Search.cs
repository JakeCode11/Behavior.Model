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


namespace UserModeling
{
    using System.Linq;
    using MathCog;
    using System;
    using System.Diagnostics;
    using System.Collections.Generic;

    public partial class BehaviorGraph
    {
        public BehaviorGraphNode RetrieveInitInnerNode(int index = 0)
        {
            var startNode = Nodes[0];

            var initNode = startNode.OutEdges[index].Target;

            //var initNode = Nodes[1];
            object obj = SearchInnerLoopNode(initNode);

            var node = obj as BehaviorGraphNode;
            var lstNode = obj as List<BehaviorGraphNode>;

            if (node != null) return node;
            if (lstNode != null) return lstNode[0];

            return null;
        }

        public int PathFinding(BehaviorGraphNode currentNode)
        {
            Debug.Assert(currentNode != null);

            int count = 0;
            while (currentNode.OutEdges.Count != 0)
            {
                currentNode = currentNode.OutEdges[0].Target;
                count++;
            }

            var parentNode = SearchOuterLoopNode(currentNode);

            if (parentNode == null) return 0;

            while (parentNode.OutEdges.Count != 0)
            {
                parentNode = parentNode.OutEdges[0].Target;
                count += parentNode.SubGraph.Nodes.Count-1;
                //count += CurrentSubGraphPathFinding(parentNode);
            }
            
            return count;
        }

        //TODO
        public int CurrentSubGraphPathFinding(BehaviorGraphNode outerLoopNode)
        {
            var obj = SearchInnerLoopNode(outerLoopNode);

            var innerNode    = obj as BehaviorGraphNode;
            var innerNodeLst = obj as List<BehaviorGraphNode>;

            BehaviorGraphNode sourceNode = null;

            if (innerNode != null) sourceNode = innerNode;
            if (innerNodeLst != null) sourceNode = innerNodeLst[0];

            int parentIndex = SearchOuterLoopNodeIndex(sourceNode);

            int count = 0;
            while (sourceNode != null && sourceNode.OutEdges.Count != 0)
            {
                var nextObj = SearchNextInnerLoopNode(sourceNode);

                var nextNode = obj as BehaviorGraphNode;
                var nextNodeLst = obj as List<BehaviorGraphNode>;
                if (nextNodeLst != null) nextNode = nextNodeLst[0];
                int outerLoopIndex = SearchOuterLoopNodeIndex(nextNode);
                if (outerLoopIndex != parentIndex) break;
                count++;
            }
            return count;
        }

        private List<string> SearchAllOuterEdgeInfos(int index)
        {
            var lst = new List<string>();
            BehaviorGraphNode currentNode = Nodes[0];
            var edgeProp = currentNode.OutEdges[index].Property as OuterLoopEdgeProperty;
            Debug.Assert(edgeProp != null);
            lst.Add(edgeProp.Strategy);            
            currentNode = currentNode.OutEdges[index].Target;
            while (currentNode.OutEdges.Count != 0)
            {
                edgeProp = currentNode.OutEdges[0].Property as OuterLoopEdgeProperty;
                Debug.Assert(edgeProp != null);
                lst.Add(edgeProp.Strategy);
                currentNode = currentNode.OutEdges[0].Target;
            }
            return lst;
        }

        public List<string> SearchAllOuterEdgeInfos()
        {
           /* var lst = new List<string>();
            var currentNode = Nodes[0];
            while (currentNode.OutEdges.Count != 0)
            //while (currentNode != null)
            {
                var edgeProp = currentNode.OutEdges[0].Property as OuterLoopEdgeProperty;
                Debug.Assert(edgeProp != null);
                lst.Add(edgeProp.Strategy);
                currentNode = currentNode.OutEdges[0].Target;
            }*/

            int index = FindGoalIndex();
            return SearchAllOuterEdgeInfos(index);
        }

        #region Search Utils

        /// <summary>
        /// It can be in the same sub-graph, or the next sub-graph
        /// or null
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="wrongPathFilter"></param>
        /// <returns>Tuple<InnerLoopEdgeProperty, BehaviorGraphNode>
        ///         List<Tuple<InnerLoopEdgeProperty, BehaviorGraphNode>>
        /// </returns>
        public object SearchNextInnerLoopNode(BehaviorGraphNode currentNode)
        {
            if (currentNode == null) return null;
            var innerState = currentNode.State as InnerLoopBehaviorState;
            Debug.Assert(innerState != null);
            //must be leaf node
            Debug.Assert(currentNode.SubGraph == null);

            if (currentNode.OutEdges.Count != 0)
            {
                var lst = new List<Tuple<object, object>>();
                foreach (var tempEdge in currentNode.OutEdges)
                {
                    var tempEdgeInfo = tempEdge.Property as InnerLoopEdgeProperty;
                    Debug.Assert(tempEdgeInfo != null);
                    if (!tempEdgeInfo.CorrectPath) continue;
                    lst.Add(new Tuple<object, object>(tempEdgeInfo, tempEdge.Target));
                }
                if(lst.Count == 0) throw new Exception("Cannot reach here");
                if (lst.Count == 1) return lst[0];
                return lst;
            }

            Debug.Assert(currentNode.OutEdges.Count == 0);

            //find parent node of current node
            var parentNode = SearchOuterLoopNode(currentNode);
            Debug.Assert(parentNode != null);

            if (parentNode.OutEdges.Count == 0) return null;
            if (parentNode.OutEdges.Count == 1)
            {
                var edge = parentNode.OutEdges[0];
                BehaviorGraphEdgeProperty edgeInfo = edge.Property;
                var nextParentNode = edge.Target;

                object obj = SearchInnerLoopNode(nextParentNode);
                var node = obj as BehaviorGraphNode;
                var lstNode = obj as List<BehaviorGraphNode>;
                if (node != null)
                {
                    return SearchNextInnerLoopNode(node);
                    //return new Tuple<object, object>(edgeInfo, node);
                }
                if (lstNode != null)
                {
                    var lst = new List<Tuple<object, object>>();
                    foreach (BehaviorGraphNode bgn in lstNode)
                    {
                        return SearchNextInnerLoopNode(bgn);
                        //var tuple = new Tuple<object, object>(edgeInfo, bgn)
                    }
                }                
            }
            if (parentNode.OutEdges.Count > 1)
            {
                throw new Exception("TODO");   
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="outerNode"></param>
        /// <returns>BehaviorGraphNode or List<BehaviorGraphNode></returns>
        public object SearchInnerLoopNode(BehaviorGraphNode outerNode, bool wrongPathFilter = true)
        {
            Debug.Assert(outerNode != null);
            if (outerNode.SubGraph == null || outerNode.SubGraph.Nodes.Count == 0) return null;

            if (outerNode.SubGraph.Nodes.Count == 1)
            {
                return Nodes[1].SubGraph.Nodes[0]; 
            }

            var lst = new List<BehaviorGraphNode>();
            foreach (BehaviorGraphNode node in outerNode.SubGraph.Nodes)
            {
                if (node.InEdges.Count == 0)
                {
                    if (wrongPathFilter && node.DeriveToWrongPath()) continue;
                    lst.Add(node);
                }
            }

            if (lst.Count == 1) return lst[0];
            if (lst.Count == 0)
            {
                if (outerNode.SubGraph.Nodes.Count != 0)
                {
                    return outerNode.SubGraph.Nodes[0];
                }
            }
            return lst;
        }

        /// <summary>
        /// It can be in the same outer loop(sub-graph), or come back to 
        /// the previous sub-graph, or even null 
        /// if it is the first inner-loop node
        /// </summary>
        /// <param name="gn"></param>
        /// <param name="wrongPathFilter"></param>
        /// <returns>BehaviorGraphNode or List<BehaviorGraphNode></returns>
        public object SearchPrevInnerLoopNode(BehaviorGraphNode gn, 
                                              bool wrongPathFilter = true)
        {
            var innerState = gn.State as InnerLoopBehaviorState;
            Debug.Assert(innerState != null);
            Debug.Assert(gn.SubGraph == null);

            if (gn.InEdges.Count != 0)
            {
                if (gn.InEdges.Count == 1) return gn.InEdges[0].Source;
                Debug.Assert(gn.InEdges.Count > 1);

                if (wrongPathFilter)
                {
                    var lst = new List<BehaviorGraphNode>();
                    foreach (var edge in gn.InEdges)
                    {
                        var edgeInfo = edge.Property as InnerLoopEdgeProperty;
                        Debug.Assert(edgeInfo != null);
                        if(edgeInfo.CorrectPath) lst.Add(edge.Source);
                    }
                    if (lst.Count == 1) return lst[0];
                    return lst;
                }
                else
                {
                    var lst = new List<BehaviorGraphNode>();
                    foreach (var edge in gn.InEdges)
                    {
                        var edgeInfo = edge.Property as InnerLoopEdgeProperty;
                        Debug.Assert(edgeInfo != null);
                       lst.Add(edge.Source);
                    }
                    return lst;
                } 
            }

            Debug.Assert(gn.InEdges.Count == 0);
            BehaviorGraphNode outerLoopNode = SearchOuterLoopNode(gn);
            if (outerLoopNode == null) return null;
            if (outerLoopNode.InEdges.Count == 0) return null;
            if (outerLoopNode.InEdges.Count == 1)
            {
                var edge = outerLoopNode.InEdges[0];
                var prevOuterLoopNode = edge.Source;
                return SearchInnerLoopNode(prevOuterLoopNode);
            }
            if (outerLoopNode.InEdges.Count > 1)
            {
                throw new Exception("TODO");
            }
            return null;
        }

        public BehaviorGraphNode SearchOuterLoopNode(BehaviorGraphNode inputInnerNode)
        {
            var innerState = inputInnerNode.State as InnerLoopBehaviorState;
            Debug.Assert(innerState != null);
            Debug.Assert(inputInnerNode.SubGraph == null);

            foreach (BehaviorGraphNode outerNode in Nodes)
            {
                if (outerNode.SubGraph == null) continue;
                var internalNodes = outerNode.SubGraph.Nodes;
                foreach (BehaviorGraphNode innerNode in internalNodes)
                {
                    bool result = innerNode.Equals(inputInnerNode);
                    if (result) return outerNode;
                }
            }
            return null;
        }

        public int SearchOuterLoopNodeIndex(BehaviorGraphNode gn)
        {
            Debug.Assert(gn.SubGraph == null);
            var parentNode = SearchOuterLoopNode(gn);

            BehaviorGraphNode currentNode = Nodes[0];
            int index = FindGoalIndex();

            var edgeProp = currentNode.OutEdges[index].Property as OuterLoopEdgeProperty;
            Debug.Assert(edgeProp != null);
            currentNode = currentNode.OutEdges[index].Target;

            int counter = 1;
            if (currentNode.Equals(parentNode)) return counter;
            while (currentNode.OutEdges.Count != 0)
            {
                counter++;
                edgeProp = currentNode.OutEdges[0].Property as OuterLoopEdgeProperty;
                Debug.Assert(edgeProp != null);               
                currentNode = currentNode.OutEdges[0].Target;
                if (currentNode.Equals(parentNode)) return counter;                
            }

            return counter;
/*            for (var i = 0; i < Nodes.Count; i++)
            {
                if (Nodes[i].Equals(parentNode))
                {
                    return i;
                }
            }
            return -1;*/
        }

        public BehaviorGraphNode SearchOuterLoopNode(string strategy)
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                for (int j = 0; j < Nodes[i].OutEdges.Count; j++)
                {
                    var edge = Nodes[i].OutEdges[j];
                    var outerEdge = edge.Property as OuterLoopEdgeProperty;
                    Debug.Assert(outerEdge != null);
                    if (outerEdge.Strategy.Equals(strategy))
                    {
                        return edge.Target;
                    }
                }
            }
            return null;
        }
        
        public BehaviorGraphNode SearchInnerLoopNode(object obj)
        {
            foreach (BehaviorGraphNode gn in Nodes)
            {
                if (gn.SubGraph != null)
                {
                    var node = gn.SubGraph.SearchInnerLoopNode(obj);
                    if (node == null) continue;
                    return node;
                }
                Debug.Assert(gn.SubGraph == null);
                var state = gn.State as InnerLoopBehaviorState;
                if (state == null) continue;
                bool result = state.MatchState(obj);
                if (result) return gn;
            }
            return null;
        }

        #endregion
    }
}