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
    /// <summary>
    /// Math Problem Tutor Interpreter
    /// </summary>
    public partial class MpTutorInterpreter : IVerify, IQuery, IInput
    {
        public MpTutor CurrentProblemTutor { get; set; }

        public MpTutorInterpreter(MpTutor tutor)
        {
            CurrentProblemTutor = tutor;
        }

        private BehaviorGraphNode _currentStateNode;
        public BehaviorGraphNode CurrentStateNode
        {
            get { return _currentStateNode; }
            set { _currentStateNode = value; }
        }

        public bool ReasoningOn { get; set; }
    }
}