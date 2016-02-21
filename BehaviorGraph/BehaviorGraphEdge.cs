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
 **********************************************************************/

namespace UserModeling
{
    public class BehaviorGraphEdge
    {
        private BehaviorGraphNode _node1;
        public BehaviorGraphNode Source
        {
            get { return _node1; }
            set { _node1 = value; }
        }

        private BehaviorGraphNode _node2;
        public BehaviorGraphNode Target
        {
            get { return _node2; }
            set { _node2 = value; }
        }

        private BehaviorGraphEdgeProperty _property;
        public BehaviorGraphEdgeProperty Property
        {
            get { return _property; }
            set { _property = value; }
        }

        public BehaviorGraphEdge(BehaviorGraphNode source, 
                BehaviorGraphNode target, BehaviorGraphEdgeProperty prop)
        {
            _node1 = source;
            _node2 = target;
            _property = prop;
        }
    }
}
