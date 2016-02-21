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
using MathCog;
using NUnit.Framework;

namespace UserModeling
{
    using CSharpLogic;
    using System.Collections.Generic;

    public enum BehaviorLevel
    {
        Inner, 
        Outer
    }

    public abstract class BehaviorGraphNodeState
    {
        private BehaviorLevel _level;
        public BehaviorLevel Level
        {
            get { return _level; }
            set { _level = value; }
        }
        protected BehaviorGraphNodeState(BehaviorLevel level)
        {
            _level = level;
        }
    }

    /// <summary>
    /// Two types of OuterLoop Node: Start, Intermediate.
    /// </summary>
    public class OuterLoopBehaviorState : BehaviorGraphNodeState
    {
        public List<TraceStep> Traces { get; set; }
       

        public OuterLoopBehaviorState(List<TraceStep> traces) // intermediate
            :base(BehaviorLevel.Outer)
        {
            Traces = traces;
        }

        public OuterLoopBehaviorState() //start
            :base(BehaviorLevel.Outer)
        {
        }
    }

    public class InnerLoopBehaviorState : BehaviorGraphNodeState
    {
        //Algebracally can be term, equation, shape or goal
        //Geometryically can be shape
        private object _userKnowledge;

        public object UserKnowledge { get { return _userKnowledge; }
            set { _userKnowledge = value; } } 
              
        public InnerLoopBehaviorState(object userInput)
            :base(BehaviorLevel.Inner)
        {
            UserKnowledge = userInput;
        }

        public bool MatchState(object obj)
        {
            if (obj == null) return false;
            if (UserKnowledge == null) return false;
            Debug.Assert(UserKnowledge != null);
            var iKnowledge = obj as IKnowledge;
            if (iKnowledge != null)
            {
                return MatchIKnowledgeHeuristic(iKnowledge);
            }
            else
            {
                bool cond1 = UserKnowledge.ToString().Equals(obj.ToString());
                if (cond1) return true;
                return UserKnowledge.Equals(obj);
            }
        }

        private bool MatchIKnowledgeHeuristic(IKnowledge iKnowledge)
        {
            var shapeExpr = iKnowledge as AGShapeExpr;
            var propExpr = iKnowledge as AGPropertyExpr;
            var equationExpr = iKnowledge as AGEquationExpr;
            var queryExpr = iKnowledge as AGQueryExpr;

            if (shapeExpr != null)
            {
                bool result = shapeExpr.Equals(UserKnowledge);
                if (result) return true;
                result = UserKnowledge.ToString().Equals(shapeExpr.ShapeSymbol.ToString());
                if (result) return true;
                return UserKnowledge.Equals(shapeExpr.ShapeSymbol);
            }
            if (propExpr != null)
            {
                return propExpr.Goal.ApproximateMatch(UserKnowledge);
            }
            if (equationExpr != null)
            {
                bool result = UserKnowledge.Equals(equationExpr.Equation);
                if (result) return true;
                result = equationExpr.Equation.ToString().Equals(UserKnowledge.ToString());
                return result;
            }
            if (queryExpr != null)
            {
                var labelConstraint = queryExpr.QueryTag.Constraint1 as string;
                if (labelConstraint == null) return false;

                bool result = UserKnowledge.Equals(queryExpr.QueryTag);
                if (result) return true;

                var shapeSymbol = UserKnowledge as ShapeSymbol;
                if (shapeSymbol == null) return false;

                return shapeSymbol.ApproximateMatch(labelConstraint);
            }

#region Partial Matching

            if (iKnowledge.Tag == null) return false;

            var term = UserKnowledge as Term;
            if (term != null)
            {
                return term.Equals(iKnowledge.Tag);
            }

            var equation = UserKnowledge as Equation;
            if (equation != null)
            {
                return equation.Lhs.Equals(iKnowledge.Tag);
            }

#endregion

            return false;
        }
    }
}
