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

using Loader;
using UserModeling;

namespace MathCog2.UserModeling
{
    /// <summary>
    /// Math Problem Tutor
    /// </summary>
    public class MpTutor
    {
        private ProblemLoader _pl;

        public ProblemLoader PL
        {
            get { return _pl; }
            set { _pl = value; }
        }

        //subjective model
        private BehaviorGraph UserGraph { get; set; }

        //objective model
        private BehaviorGraph ObjectGraph { get; set; }

        public MpTutor(int problemIndex)
        {
            _pl = new ProblemLoader(problemIndex);
        }

        #region Load

        private void LoadObjectGraph()
        {

        }

        private void LoadUserModel(int problemIndex)
        {

        }

        #endregion

        #region Online Learning Analysis

        public void AnalyzeSolving()
        {
            
        }

        #endregion
    }
}