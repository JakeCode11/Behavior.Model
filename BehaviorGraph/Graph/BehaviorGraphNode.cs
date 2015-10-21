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

using System.Diagnostics;

namespace UserModeling
{
    using CSharpLogic;
    using System.Collections.Generic;

    public class BehaviorGraphNode
    {
        private List<BehaviorGraphEdge> _inEdges;
        public List<BehaviorGraphEdge> InEdges
        {
            get { return _inEdges; }
        }

        private List<BehaviorGraphEdge> _outEdges;
        public List<BehaviorGraphEdge> OutEdges
        {
            get { return _outEdges; }
            set { _outEdges = value; }
        }

        private BehaviorGraphNodeState _state;
        public BehaviorGraphNodeState State
        {
            get { return _state;  }
            set { _state = value; }
        }

        private BehaviorGraph _subGraph;
        public BehaviorGraph SubGraph
        {
            get { return _subGraph; }
            set { _subGraph = value; }
        }

        public BehaviorGraphNode(BehaviorGraphNodeState bs)
        {
            _inEdges = new List<BehaviorGraphEdge>();
            _outEdges = new List<BehaviorGraphEdge>();
            _state = bs;
            InitSubGraph();
        }

        private void InitSubGraph()
        {
            var outerState = _state as OuterLoopBehaviorState;
            if (outerState == null) return;

            var steps = outerState.Traces;
            if (steps == null || steps.Count == 0) return;

            _subGraph = new BehaviorGraph();          
            var ts1 = steps[0];
            var startState =
                new BehaviorGraphNode(new InnerLoopBehaviorState(ts1.Source));
            _subGraph.Nodes.Add(startState);

            BehaviorGraphNode source = startState;
            for(int i = 0; i < outerState.Traces.Count; i++)
            {
                var ts = outerState.Traces[i];
                var nodeState = new InnerLoopBehaviorState(ts.Target);
                var target = new BehaviorGraphNode(nodeState);
                _subGraph.Nodes.Add(target);

                var innerEdgeProp = new InnerLoopEdgeProperty((string)ts.Rule, (string)ts.AppliedRule);
                var newEdge = new BehaviorGraphEdge(source, target, innerEdgeProp);
                source.OutEdges.Add(newEdge);
                target.InEdges.Add(newEdge);

                source = target;
            }
        }
    }

    public static class BehaviorGraphNodeExtension
    {
        public static bool DeriveToWrongPath(this BehaviorGraphNode innerLoopNode)
        {
            foreach (var edge in innerLoopNode.OutEdges)
            {
                var edgeProp = edge.Property as InnerLoopEdgeProperty;
                Debug.Assert(edgeProp != null);
                if (!edgeProp.CorrectPath) continue;
                return false;
            }
            return true;
        }
    }

}