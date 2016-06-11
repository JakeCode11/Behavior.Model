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
using System.Text.RegularExpressions;
using CSharpLogic;
using NUnit.Framework;
using UserModeling;

namespace MathCog.UserModeling
{
    using System;
    using System.Diagnostics;
    using MathCog;

    public partial class HCIReasoner
    {
        #region Properties

        //Worked by Example Property

        private IKnowledge _queriedKnowledge; 
        public IKnowledge QueriedKnowledge
        {
            get { return _queriedKnowledge; }
            set
            {
                _queriedKnowledge = value;
                OnPropertyChanged("QueriedKnowledge");
            }
        }

        private bool _currentStepHintRequired = true;
        public bool CurrentStepHintRequired
        {
            set { _currentStepHintRequired = value; }
            get { return _currentStepHintRequired; }
        }

        private BehaviorGraphNode _currentStateNode;
        public BehaviorGraphNode CurrentStateNode
        {
            get { return _currentStateNode; }
            set { _currentStateNode = value; }
        }
        
        //private bool _queryProcessed;
        public int? TraceLeftCount
        {
            get { return ObjectGraph.PathFinding(CurrentStateNode); }
        }


        public bool ProblemSolved
        {
            get
            {
                if (UserGraph == null) return false;
                return UserGraph.CurrentSolvingStatus == 
                    BehaviorGraph.SolvingStatus.Complete;
            }
        }


        #endregion

        #region Reset

        public new void Reset()
        {
            UserGraph = null;
            ObjectGraph = null;
            _hasBehaviorModel = false;
            _problemIndex = -1;          
            CurrentStateNode = null;
            _currentStepHintRequired = true;
            //_queryProcessed = false;
        }

        public void Reset(object obj)
        {
            if (obj == null)
            {
                _currentStepHintRequired = true;
                //_currentStepAnswerRequired = true;
                CurrentStateNode = null;
            }
            else
            {
                if (TutorMode)
                {
                    var node = UserGraph.SearchInnerLoopNode(obj);
                    object obj1 = UserGraph.SearchPrevInnerLoopNode(node);
                    var obj1Node = obj1 as BehaviorGraphNode;
                    var obj1Lst = obj1 as List<BehaviorGraphNode>;
                    if (obj1Node != null)
                    {
                        CurrentStateNode = obj1Node;
                    }
                    if (obj1Lst != null)
                    {
                        CurrentStateNode = obj1Lst[0];
                    }
                    _currentStepHintRequired = true;
                    //_currentStepAnswerRequired = true;
                }
                else
                {
                    var node = ObjectGraph.SearchInnerLoopNode(obj);
                    object obj1 = ObjectGraph.SearchPrevInnerLoopNode(node);
                    var obj1Node = obj1 as BehaviorGraphNode;
                    var obj1Lst = obj1 as List<BehaviorGraphNode>;
                    if (obj1Node != null)
                    {
                        CurrentStateNode = obj1Node;
                    }
                    if (obj1Lst != null)
                    {
                        CurrentStateNode = obj1Lst[0];
                    }
                }
            }
        }

        #endregion

        #region public Query Api

        public void UnQuery(object obj, bool isUpdate = false)
        {
            Debug.Assert(obj != null);

            if (isUpdate) return;

            if (TutorMode)
            {
                UnqueryBehaviorGraph(obj);
            }
            else
            {
                UnqueryObjectiveGraph(obj);
            }
        }

        public void UnqueryBehaviorGraph(object obj)
        {
            if (UserGraph == null) return;
            var knowledge = obj as IKnowledge;
            Debug.Assert(knowledge != null);
            var node = UserGraph.UpdateSolvingCache(knowledge, false);
            if (node != null)
            {
                object obj1 = UserGraph.SearchPrevInnerLoopNode(node);
                var obj1Node = obj1 as BehaviorGraphNode;
                var obj1Lst = obj1 as List<BehaviorGraphNode>;
                if (obj1Node != null)
                {
                    CurrentStateNode = obj1Node;
                }
                if (obj1Lst != null && obj1Lst.Count != 0)
                {
                    CurrentStateNode = obj1Lst[0];
                }
            }
        }

        public void UnqueryObjectiveGraph(object obj)
        {
            if (ObjectGraph == null) return;
            var node = ObjectGraph.SearchInnerLoopNode(obj);
            if (node != null)
            {
                object obj1 = ObjectGraph.SearchPrevInnerLoopNode(node);
                var obj1Node = obj1 as BehaviorGraphNode;
                var obj1Lst = obj1 as List<BehaviorGraphNode>;
                if (obj1Node != null)
                {
                    CurrentStateNode = obj1Node;
                }
                if (obj1Lst != null)
                {
                    CurrentStateNode = obj1Lst[0];
                }
            }
        }

        public void AdjustQueryParameter()
        {
            if (_currentStepHintRequired)
            {
                _currentStepHintRequired = false;
            }
        }

        public QueryFeedbackState Query(object obj, out string msg, out object output)
        {
            output = null;

            if (!TutorMode && QueriedKnowledge == null)
            {
                msg = AGWarning.NoSelection;
                return QueryFeedbackState.QueryFailed;
            }

            if (TutorMode)
            {
                return QueryBehaviorGraph(obj, out msg, out output);
            }
            else
            {
                return QueryObjectiveGraph(obj, out msg, out output);
            }
        }

        private QueryFeedbackState QueryBehaviorGraph(object obj, out string msg, out object output)
        {
            msg = null;
            output = null;
            if (UserGraph == null) return QueryFeedbackState.QueryFailed;
            if ( UserGraph.Nodes.Count == 0 || UserGraph.Nodes.Count == 1)
            {
                msg = "TODO";
                return QueryFeedbackState.QueryFailed;
            }

/*            if (!_queryProcessed)
            {
                _queryProcessed = true;
                output = UserGraph.SearchAllOuterEdgeInfos();
                msg = AGTutorMessage.InputQuery;
                return QueryFeedbackState.TutorQueryStarted;
            }*/

            if (obj != null)
            {
                return VerifyBehaviorGraph(obj, out msg, out output);
            }
            Debug.Assert(obj == null);
            Debug.Assert(_currentStateNode != null);

            object edgeInfo = null;
            BehaviorGraphNode nextNode = null;
            var nextTuple1 = UserGraph.SearchNextInnerLoopNode(_currentStateNode);
            if (nextTuple1 == null) // query-end
            {
                //partial checking
                BehaviorGraph.SolvingStatus ss = UserGraph.CheckSolvingStatus();
                if (ss == BehaviorGraph.SolvingStatus.Complete)
                {
                    msg = AGDemonstrationMessage.QueryEnd;
                    return QueryFeedbackState.TutorQueryEnded;
                }
                else if (ss == BehaviorGraph.SolvingStatus.Partial)
                {
                    int nextGoalIndex = UserGraph.FindGoalIndex();
                    _currentStateNode = UserGraph.RetrieveInitInnerNode(nextGoalIndex);
                    nextTuple1 = UserGraph.SearchNextInnerLoopNode(_currentStateNode);
                    if (nextTuple1 == null)
                    {
                        msg = AGDemonstrationMessage.QueryEnd;
                        return QueryFeedbackState.TutorQueryEnded;
                    }
                    CurrentStepHintRequired = true;
                }
                else
                {
                    msg = AGDemonstrationMessage.QueryEnd;
                    return QueryFeedbackState.TutorQueryEnded;
                }
            }

            #region Path Selection

            var tuple11 = nextTuple1 as Tuple<object, object>;
            var tuple11Lst = nextTuple1 as List<Tuple<object, object>>;

            if (tuple11 != null)
            {
                edgeInfo = tuple11.Item1;
                nextNode = tuple11.Item2 as BehaviorGraphNode;
                //_currentStateNode = nextNode;
            }
            if (tuple11Lst != null)
            {
                var tupleTemp = tuple11Lst[0];
                edgeInfo = tupleTemp.Item1;
                nextNode = tupleTemp.Item2 as BehaviorGraphNode;
                //_currentStateNode = nextNode;
            }

            #endregion

            if (_currentStepHintRequired)
            {
                if (edgeInfo != null)
                {
                    var innerEdgeProp = edgeInfo as InnerLoopEdgeProperty;
                    Debug.Assert(innerEdgeProp != null);
                    msg = AGTutorMessage.QueryIntermediate;
                    _currentStepHintRequired = false;
                    //int parentIndex = UserGraph.SearchOuterLoopNodeIndex(_currentStateNode);
                    int parentIndex = UserGraph.SearchOuterLoopNodeIndex(nextNode);
                    var lst1 = UserGraph.SearchAllOuterEdgeInfos();
                    var tuple = new Tuple<object, object, object>(innerEdgeProp.MetaRule, lst1, parentIndex);
                    output = tuple;
                }
                return QueryFeedbackState.TutorQueryProcessedHint;
            }
            Debug.Assert(nextNode != null);
            _currentStepHintRequired = true;
            var nodeState = nextNode.State as InnerLoopBehaviorState;
            Debug.Assert(nodeState != null);
            var expr = IKnowledgeGenerator.Generate(nodeState.UserKnowledge);
            var innerEdgeProp1 = edgeInfo as InnerLoopEdgeProperty;
            Debug.Assert(innerEdgeProp1 != null);
            var appliedRule = innerEdgeProp1.AppliedRule;
            //int parentIndex1 = UserGraph.SearchOuterLoopNodeIndex(_currentStateNode);
            int parentIndex1 = UserGraph.SearchOuterLoopNodeIndex(nextNode);
            var lst2 = UserGraph.SearchAllOuterEdgeInfos();
            output = new Tuple<object, object, object, object>(appliedRule, expr, lst2, parentIndex1);
            CurrentStateNode = nextNode;
            return QueryFeedbackState.TutorQueryProcessedAnswer;
        }

        /// <summary>
        /// Demonstration Mode 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="msg"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        private QueryFeedbackState QueryObjectiveGraph(object obj, out string msg, out object output)
        {
            output = null;
            msg = null;

            if (ObjectGraph == null)
            {
                msg = "You do not select the answer successfully.";
                QueriedKnowledge = null;
                return QueryFeedbackState.QueryFailed;
            }

            if (ObjectGraph.Nodes.Count == 0 || ObjectGraph.Nodes.Count == 1)
            {
                msg = "No internal procedural knowledge to present.";
                QueriedKnowledge = null;
                return QueryFeedbackState.QueryFailed;
            }

            if (obj != null)
            {
                //verify user's own step
                var matchedNode = ObjectGraph.SearchInnerLoopNode(obj);

                msg = matchedNode == null ?
                    AGTutorMessage.VerifyWrong : AGTutorMessage.VerifyCorrect;

                if (matchedNode == null) return QueryFeedbackState.DemonQueryVerify;

                Debug.Assert(matchedNode != null);

                var nextTuple1 = ObjectGraph.SearchNextInnerLoopNode(matchedNode);
                if (nextTuple1 == null)
                {
                    msg = AGTutorMessage.SolvedProblem;
                }
                CurrentStateNode = matchedNode;
                return QueryFeedbackState.DemonQueryVerify;
            }

            if (CurrentStateNode == null) // QueryNotStart
            {
                CurrentStateNode = ObjectGraph.RetrieveInitInnerNode();
                /*                object obj2 = ObjectGraph.SearchInnerLoopNode(parentNode);
                                var behaviorNode = obj2 as BehaviorGraphNode;
                                var lstNode = obj2 as List<BehaviorGraphNode>;
                                if (behaviorNode != null)
                                {
                                    _currentStateNode = behaviorNode;
                                }
                                if (lstNode != null)
                                {
                                    //TODO
                                    _currentStateNode = lstNode[0];
                                }*/

                Debug.Assert(_currentStateNode != null);

                msg = AGDemonstrationMessage.QueryAnswer;

                var lst = ObjectGraph.SearchAllOuterEdgeInfos();
                int number = ObjectGraph.PathFinding(CurrentStateNode);

                output = new Tuple<object, object>(lst, number);

                return QueryFeedbackState.DemonQueryStarted;
            }

            Debug.Assert(_currentStateNode != null);

            //direct query without verification
            object edgeInfo = null;
            BehaviorGraphNode nextNode = null;
            var nextTuple2 = ObjectGraph.SearchNextInnerLoopNode(_currentStateNode);
            if (nextTuple2 == null) // query-end
            {
                msg = AGDemonstrationMessage.QueryEnd;
                return QueryFeedbackState.DemonQueryEnded;
            }
            var tuple22 = nextTuple2 as Tuple<object, object>;
            var tuple22Lst = nextTuple2 as List<Tuple<object, object>>;

            if (tuple22 != null)
            {
                edgeInfo = tuple22.Item1;
                nextNode = tuple22.Item2 as BehaviorGraphNode;
            }
            if (tuple22Lst != null)
            {
                var tupleTemp = tuple22Lst[0];
                edgeInfo = tupleTemp.Item1;
                nextNode = tupleTemp.Item2 as BehaviorGraphNode;
            }

            Debug.Assert(nextNode != null);
            var nodeState = nextNode.State as InnerLoopBehaviorState;
            Debug.Assert(nodeState != null);
            var expr = IKnowledgeGenerator.Generate(nodeState.UserKnowledge);
            var innerEdgeProp = edgeInfo as InnerLoopEdgeProperty;
            var outerEdgeProp = edgeInfo as OuterLoopEdgeProperty;
            string appliedRule = null;
            if (innerEdgeProp != null)
            {
                appliedRule = innerEdgeProp.AppliedRule;
            }
            if (outerEdgeProp != null)
            {
                appliedRule = outerEdgeProp.Strategy;
            }
            int leftTraceCount = ObjectGraph.PathFinding(nextNode);

            int parentIndex = ObjectGraph.SearchOuterLoopNodeIndex(nextNode);
            var lst1 = ObjectGraph.SearchAllOuterEdgeInfos();
            var tuple1 = new Tuple<object, object>(lst1, parentIndex);

            output = new Tuple<object, object, object, object>(appliedRule, expr, leftTraceCount, tuple1);

            CurrentStateNode = nextNode;
            return QueryFeedbackState.DemonQueryProcessed;
        }

        #endregion

        #region public Verify API

        private string UpdateVerifiedMessage()
        {
            var status = UserGraph.CheckSolvingStatus();
            
            if (status == BehaviorGraph.SolvingStatus.Partial &&
                UserGraph.CurrentSolvingStatus == BehaviorGraph.SolvingStatus.None)
            {
                UserGraph.CurrentSolvingStatus = status;
                return AGTutorMessage.SolvingPartialProblem;
            }

            if (status == BehaviorGraph.SolvingStatus.Complete &&
                UserGraph.CurrentSolvingStatus == BehaviorGraph.SolvingStatus.Partial)
            {
                UserGraph.CurrentSolvingStatus = status;
                return AGTutorMessage.SolvedProblem;
            }

            if (status == BehaviorGraph.SolvingStatus.Complete &&
                UserGraph.CurrentSolvingStatus == BehaviorGraph.SolvingStatus.None)
            {
                UserGraph.CurrentSolvingStatus = status;
                return AGTutorMessage.SolvedProblem;
            }

            return null;
        }

        private QueryFeedbackState VerifyBehaviorGraph(object obj, out string msg, out object output)
        {
            msg = null;
            output = null;
            Debug.Assert(obj != null);
            var iKnowledge = obj as IKnowledge;
            Debug.Assert(iKnowledge != null);
            //verify user's own step
            var matchedNode = UserGraph.UpdateSolvingCache(iKnowledge);
            msg = matchedNode == null ?
                AGTutorMessage.VerifyWrong : AGTutorMessage.VerifyCorrect;
            //TODO, wrong node derivation
            if (matchedNode == null)
            {
                //equation -> higher Level Semantics, such as goal or shape.
                var gObj = Reasoner.Instance.ExprValidate(iKnowledge.Expr);
                var gKnowledge = gObj as IKnowledge;
                if (gKnowledge != null)
                {
                    Verify(iKnowledge, gKnowledge, out msg, out output);
                    if (msg.Equals(AGTutorMessage.VerifyCorrect))
                    {
                        string str = UpdateVerifiedMessage();
                        if (str != null) msg = str;
                        return QueryFeedbackState.TutorQueryProcessedVerify;
                    }
                }
                var gKnowledgeLst = gObj as List<object>;
                if (gKnowledgeLst != null)
                {
                    foreach (var gKTemp in gKnowledgeLst)
                    {
                        gKnowledge = gKTemp as IKnowledge;
                        if (gKnowledge == null) continue;

                        Verify(iKnowledge, gKnowledge, out msg, out output);
                        if (!msg.Equals(AGTutorMessage.VerifyWrong))
                        {
                            return QueryFeedbackState.TutorQueryProcessedVerify;
                        }
                    }
                }
                return QueryFeedbackState.TutorQueryProcessedVerify;
            }
            Debug.Assert(matchedNode != null);
            CurrentStateNode = matchedNode;
            string str1 = UpdateVerifiedMessage();
            if (str1 != null) msg = str1;
            return QueryFeedbackState.TutorQueryProcessedVerify;
           /* var nextTuple = UserGraph.SearchNextInnerLoopNode(matchedNode);
            if (nextTuple == null)
            {
                if (UserGraph.IsPartialCorrect())
                {
                    msg = AGTutorMessage.SolvingPartialProblem;
                }
                else
                {
                    msg = AGTutorMessage.SolvedProblem;
                }
            }*/
        }

        public void Verify(IKnowledge source, IKnowledge obj, out string msg, out object output)
        {
            output = null;
            object outobj;
            SurfaceVerify(source, obj, out msg, out outobj); //Equation Eval Trace
            if (msg != null && msg.Equals(AGTutorMessage.VerifyCorrect)) return;
            DeepVerify(source, obj, out msg, out outobj); //Relation Eval
        }

        private void DeepVerify(IKnowledge source, IKnowledge obj, out string msg, out object output)
        {
            msg = AGTutorMessage.VerifyWrong;

            object result;
            var shapeExpr = obj as AGShapeExpr;
            if (shapeExpr != null)
            {
                result = Reasoner.Instance.RelationValidate(shapeExpr.ShapeSymbol, out output);
            }
            else
            {
                result = Reasoner.Instance.RelationValidate(obj.Expr, out output);                
            }

            if (result == null) return;

            var inputApproximateMatched = result as bool?;
            if (inputApproximateMatched != null)
            {
                if (inputApproximateMatched.Value)
                {
                    msg = AGTutorMessage.VerifyCorrect;
                }
                return;
            }

            var trace = result as List<Tuple<object, object>>;
            if (trace != null)
            {
                bool matchResult = UserGraph.Match(trace); //match and update
                if (matchResult)
                {
                    //insert nodes
                    UserGraph.Insert(trace);
                    CurrentStateNode = UserGraph.SearchInnerLoopNode(source); //update _currentStateNode;
                    //Debug.Assert(_currentStateNode != null);
                    msg = AGTutorMessage.VerifyCorrect;
                }
            }
        }

        private void SurfaceVerify(IKnowledge source, IKnowledge obj, out string msg, out object output)
        {
            msg = AGTutorMessage.VerifyWrong;
            output = null;

            var eqGoalExpr = obj as AGPropertyExpr;
            if (eqGoalExpr != null)
            {
                GoalVerify(source, eqGoalExpr.Goal, out msg, out output);
            }
            var shapeExpr = obj as AGShapeExpr;
            if (shapeExpr != null)
            {
                ShapeVerify(source, shapeExpr.ShapeSymbol, out msg, out output);
            }
       /*     var eqGoalLst = obj as List<object>;
            if (eqGoalLst != null)
            {
                foreach (var tempObj in eqGoalLst)
                {
                    var gGoal = tempObj as AG;
                    bool verified = GoalVerify(obj, gGoal, out msg, out output);
                    if (verified) return;
                }
            }*/
        }

        private bool ShapeVerify(IKnowledge obj, ShapeSymbol shape, out string msg, out object output)
        {
            msg = AGTutorMessage.VerifyWrong;
            output = null;

            List<Tuple<object, object>> trace = null;

            var agShapeExpr = new AGShapeExpr(obj.Expr, shape);
            agShapeExpr.IsSelected = true;
            agShapeExpr.GenerateSolvingTrace();
            trace = agShapeExpr.AutoTrace;

            if (trace == null || trace.Count == 0) return false;

         /*   var lastTuple = trace[trace.Count - 1] as Tuple<object, object>;
            var lastLst = lastTuple.Item2 as List<object>;
            Debug.Assert(lastLst != null);
            Debug.Assert(lastLst.Count != 0);
            var lastTs = lastLst[lastLst.Count - 1] as TraceStepExpr;*/
            bool matchResult = UserGraph.Match(trace); //match and update
            if (!matchResult) return false;

            //insert nodes
            UserGraph.Insert(trace);
            CurrentStateNode = UserGraph.SearchInnerLoopNode(obj); //update _currentStateNode;
            //var nextTuple1 = UserGraph.SearchNextInnerLoopNode(CurrentStateNode);
          /*  if (nextTuple1 == null) // query-end
            {
                msg = AGTutorMessage.SolvedProblem;
                return true;
            }*/
            msg = AGTutorMessage.VerifyCorrect;
            return true;
        }

        private bool GoalVerify(IKnowledge obj, EqGoal eqGoal, out string msg, out object output)
        {
            msg = AGTutorMessage.VerifyWrong;
            output = null;

            List<Tuple<object, object>> trace = null;

            var agPropExpr = new AGPropertyExpr(obj.Expr, eqGoal);
            agPropExpr.IsSelected = true;
            agPropExpr.GenerateSolvingTrace();
            trace = agPropExpr.AutoTrace;

            BehaviorGraphNode node;
            if (trace == null || trace.Count == 0)
            {
                node = UserGraph.UpdateSolvingCache(agPropExpr);
                //node = UserGraph.SearchInnerLoopNode(eqGoal);

                if (node == null)
                    return false;
            }
            if (trace != null)
            {
                bool matchResult = UserGraph.Match(trace);
                if (!matchResult) return false;
                //insert nodes
                UserGraph.Insert(trace);
                CurrentStateNode = UserGraph.SearchInnerLoopNode(obj); //update _currentStateNode;
            }
            else
            {
                CurrentStateNode = UserGraph.SearchInnerLoopNode(eqGoal); 
            }
           /* var nextTuple1 = UserGraph.SearchNextInnerLoopNode(CurrentStateNode);
            if (nextTuple1 == null) // query-end
            {
                msg = AGTutorMessage.SolvedProblem;
                return true;
            }
            */
            msg = AGTutorMessage.VerifyCorrect;
            return true;
        }
        #endregion
    }
}