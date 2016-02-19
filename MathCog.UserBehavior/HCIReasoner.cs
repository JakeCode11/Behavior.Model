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
using System;
using System.ComponentModel;
using MathCog.UserModeling.Annotations;
using UserModeling;
namespace MathCog.UserModeling
{
    using MathCogUserData;
    using CSharpLogic;
    using System.Diagnostics;
    using MathCog;

    public partial class HCIReasoner : Reasoner, INotifyPropertyChanged
    {
        #region Properties and Constructors

        #region Internal Properties

        //subjective model
        internal BehaviorGraph UserGraph   { get; set; }

        //objective model
        internal BehaviorGraph ObjectGraph { get; set; }

        private bool _hasBehaviorModel;
        public bool HasBehaviorModel
        {
            get { return _hasBehaviorModel; }
            set { _hasBehaviorModel = value; }
        }
        
        #endregion

        #region Static Properties

        public static bool TutorMode = true;

        public static bool ReasoningOn = true;

        #endregion

        #region User Parameters

        private int _problemIndex;
        public int ProblemIndex
        {
            get { return _problemIndex; }
            set { _problemIndex = value; }
        }

        #endregion

        public HCIReasoner()
        {
            PropertyChanged += HCIReasoner_PropertyChanged;
        }

        #endregion

        #region public API

        public object HCILoad(object obj, ShapeType? st = null, 
            bool userInput = false, bool algebraSide = true)
        {
            if (TutorMode)
            {
                return Reasoner.Instance.Load(obj, st, true, algebraSide); 
            }

            // (demonstration) solving mode
            if (userInput)
            {
                return Reasoner.Instance.Load(obj, st, true);
            }
            else
            {
                return Reasoner.Instance.Load(obj, st, false);
            }
        }

        public bool HCIUnLoad(object obj)
        {
            if (!TutorMode)
            {
                return Reasoner.Instance.Unload(obj);                
            }
            return false;
        }
  
        #endregion

        #region Init Setup

        public void InitMode()
        {
            if (!TutorMode)
            {
                CurrentStateNode = null;
                Reasoner.Instance.Reset();
            }
        }

        public bool InitProblem(int problemIndex)
        {
            //Reset();
            Reasoner.Instance.Reset();
            ProblemIndex = problemIndex;
            return LoadUserModel(ProblemIndex);
        }

        private void LoadObjectGraph()
        {
            Debug.Assert(QueriedKnowledge !=null);
            IKnowledge selectRenderAnswer = QueriedKnowledge.FindSelectedKnowledge();
            if (selectRenderAnswer == null) return;
            selectRenderAnswer.GenerateSolvingTrace();
            //if (selectRenderAnswer.AutoTrace == null) return;
            var behaviorGraph = new BehaviorGraph();
            if (selectRenderAnswer.AutoTrace != null)
            {
                behaviorGraph.Insert(selectRenderAnswer.AutoTrace);
            }
            ObjectGraph = behaviorGraph;
        }

        private bool LoadUserModel(int problemIndex)
        {
            _problemIndex = problemIndex;

            var behaviorGraph = BehaviorUserModelLoader.Instance.LoadProblem(problemIndex);
            
            if (behaviorGraph != null)
            {
                UserGraph = behaviorGraph;
                CurrentStateNode = UserGraph.RetrieveInitInnerNode();
                HasBehaviorModel = true;
                return true;
            }
          
            Debug.Assert(UserGraph == null);
            HasBehaviorModel = false;
            //Capture User Input of the pre-programmed problems.
            return false;
        }

        #endregion

        #region Event 
        
        void HCIReasoner_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("QueriedKnowledge"))
            {
                if (TutorMode) return;
                if(QueriedKnowledge != null) LoadObjectGraph();
            }
        }

        #region Event Handler
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #endregion
    }
}
