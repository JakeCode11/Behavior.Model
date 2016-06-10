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

using System.Collections.Generic;
using UserModeling;

namespace MathCog2.UserModeling
{
    public class MathTutorEngine : ISelect, IAnalyze
    {
        //Record a list of Tutored Problem
        public List<MpTutorInterpreter> TutoredProblems { get; set; }
        public MpTutorInterpreter CurrentTutorInterp { get; set; }

        private static MathTutorEngine _instance;

        public static MathTutorEngine Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MathTutorEngine();
                }
                return _instance;
            }
        }

        private MathTutorEngine()
        {
            TutoredProblems = new List<MpTutorInterpreter>();
        }

        #region Internal Load

        #endregion

        #region Problem Selection

        public void Select(int index)
        {
            var mpt = new MpTutor(index);
            CurrentTutorInterp = new MpTutorInterpreter(mpt);
            TutoredProblems.Add(CurrentTutorInterp);
        }

        public void Select()
        {
            
        }

        #endregion

        #region Student Performance Analysis

        public void Analyze()
        {

        }

        #endregion
    }
}
