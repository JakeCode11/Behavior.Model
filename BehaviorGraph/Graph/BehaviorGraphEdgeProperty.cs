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
    public abstract class BehaviorGraphEdgeProperty
    {
        private BehaviorLevel _level;
        protected BehaviorGraphEdgeProperty(BehaviorLevel level)
        {
            _level = level;
        }
    }

    public class OuterLoopEdgeProperty : BehaviorGraphEdgeProperty
    {
        public string Strategy { get; set; }
        public OuterLoopEdgeProperty(string strategy)
            :base(BehaviorLevel.Outer)
        {
            Strategy = strategy;
        }
    }

    public class InnerLoopEdgeProperty : BehaviorGraphEdgeProperty
    {
        public InnerLoopEdgeProperty(string metaRule, string appliedRule)
            : base(BehaviorLevel.Inner)
        {
            MetaRule = metaRule;
            AppliedRule = appliedRule;
            CorrectPath = true;
        }

        public InnerLoopEdgeProperty()
            : base(BehaviorLevel.Inner)
        {
            CorrectPath = false;
        }

        public string MetaRule { get; set; }
        public string AppliedRule { get; set; }

        public bool CorrectPath { get; set; }
    }
}
