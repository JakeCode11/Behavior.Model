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

using AlgebraGeometry;
using CSharpLogic;
using starPadSDK.MathExpr;

namespace UserModeling
{
    using MathCog;
    using MathCog.UserModeling;
    using NUnit.Framework;

    [TestFixture]
    public partial class TestProblems
    {
        /*
         * Problem 1: Find the distance betweeen A(2,0) and B(5,4)?
         */

        #region Tutoring

        public void Init1_Tutoring()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 1;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            bool tutorMode = true;
            HCIReasoner.Instance.InitMode(tutorMode);
            //Not this one
            Assert.True(HCIReasoner.Instance.RelationGraph.Nodes.Count == 0);
            //Verifier
            Assert.True(Reasoner.Instance.RelationGraph.Nodes.Count == 3);
        }

        #region Behavior Test

        [Test]
        public void Test_Problem_01_Tutor_0()
        {
            Init1_Tutoring();

            ///////////////////////////////////////////////////////
            /*
             * User Input: (2,0)
             * Expect result: Correct Track
             */ 
            string userFact0 = "(2,0)";
            var userShapeExpr1 = HCIReasoner.Instance.HCILoad(userFact0) as AGShapeExpr;
            Assert.NotNull(userShapeExpr1);
            string message;
            object output;
            QueryFeedbackState state = HCIReasoner.Instance.Query(userShapeExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));
            /*
             * User Input: A(2,0)
             * Expect result: Correct Track
             */ 
            const string userFact1 = "A(2,0)";
            userShapeExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as AGShapeExpr;
            Assert.NotNull(userShapeExpr1);
            state = HCIReasoner.Instance.Query(userShapeExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /*
             * User Input: d^2 = 9 + 16
             * 
             * Expect result: Correct Track
             */
            Expr userFact2 = ExprMock.Mock1();
            var userEqExpr2 = HCIReasoner.Instance.HCILoad(userFact2) as AGEquationExpr;
            Assert.NotNull(userEqExpr2);
            state = HCIReasoner.Instance.Query(userEqExpr2, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /*
             * User Input: d^2 = 16 + 9
             * 
             * Expect result: Correct Track
             */
            Expr userFact3 = ExprMock.Mock2();
            var userEqExpr3 = HCIReasoner.Instance.HCILoad(userFact3) as AGEquationExpr;
            Assert.NotNull(userEqExpr3);
            state = HCIReasoner.Instance.Query(userEqExpr3, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /*
             * User Input: d = Sqrt(16+9)
             * 
             * Expect result: Correct Track
             */
            Expr userFact4 = ExprMock.Mock3();
            var userEqExpr4 = HCIReasoner.Instance.HCILoad(userFact4) as AGEquationExpr;
            Assert.NotNull(userEqExpr4);
            state = HCIReasoner.Instance.Query(userEqExpr4, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));


            /*
             * User Input: d = Sqrt(4+3)
             * 
             * Expect result: Wrong track
             */
            Expr userFact4_1 = ExprMock.Mock3_1();
            var userEqExpr4_1 = HCIReasoner.Instance.HCILoad(userFact4_1) as AGEquationExpr;
            Assert.NotNull(userEqExpr4_1);
            state = HCIReasoner.Instance.Query(userEqExpr4_1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyWrong));


            /*
             * User Input: d = 5
             * 
             * Expect result: Correct Track
             */
            const string userFact5 = "d=5";
            var userEqExpr5 = HCIReasoner.Instance.HCILoad(userFact5) as AGPropertyExpr;
            Assert.NotNull(userEqExpr5);

            state = HCIReasoner.Instance.Query(userEqExpr5, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvedProblem));

        }

        [Test]
        public void Test_Problem_01_Tutor_1()
        {
            Init1_Tutoring();

            string message;
            object output;
            QueryFeedbackState state;

            /*
             * User Input: 5
             * 
             * Expect result: Correct Track TODO Wrong
             */
            const string userFact5 = "5";
            var userEqExpr5 = HCIReasoner.Instance.HCILoad(userFact5);
            Assert.NotNull(userEqExpr5);

            state = HCIReasoner.Instance.Query(userEqExpr5, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
//            Assert.True(message.Equals(AGTutorMessage.SolvedProblem));
        }

        [Test]
        public void Test_Problem_01_Tutor_2()
        {
            Init1_Tutoring();

            string message;
            object output;
            QueryFeedbackState state;

            /*
             * User Input: (2.0,0)
             * Expect result: Correct Track
             */
            string userFact0 = "(2.0,0)";
            var userShapeExpr1 = HCIReasoner.Instance.HCILoad(userFact0) as AGShapeExpr;
            Assert.NotNull(userShapeExpr1);
            state = HCIReasoner.Instance.Query(userShapeExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));
        }

        [Test]
        public void Test_Problem_01_Tutor_3()
        {
            Init1_Tutoring();

            string message;
            object output;
            QueryFeedbackState state;

            /*
             * User Input: AB
             * Expect result: Wrong Track
             */
            string userFact0 = "AB";
            var userShapeExpr1 = HCIReasoner.Instance.HCILoad(userFact0, ShapeType.LineSegment) as AGQueryExpr;
            Assert.NotNull(userShapeExpr1);

            state = HCIReasoner.Instance.Query(userShapeExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyWrong));

            /*
             * User Input: Line Segment from geometry
             * Expect result: Correct Track
       /*      #1#
            var lss = MockSegment();
            var userShapeExpr2 = HCIReasoner.Instance.HCILoad(lss, ShapeType.LineSegment) as AGShapeExpr;
            Assert.NotNull(userShapeExpr2);

            state = HCIReasoner.Instance.Query(userShapeExpr2, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));*/
        }

        private LineSegmentSymbol MockSegment()
        {
            var pt1 = new Point("A", 2, 0);
            var pt2 = new Point("B", 5, 4);
            var seg = new LineSegment("AB", pt1, pt2);
            var segSymbol = new LineSegmentSymbol(seg);
            return segSymbol;
        }

        #endregion

        #endregion

        #region Worked Example

        public void Init1_WorkedExample()
        {
            //Retrieve authoring behavior graph 
            bool result = HCIReasoner.Instance.InitProblem(1);
            //Has authoring behavior data
            Assert.True(result);
            Assert.True(HCIReasoner.Instance.UserGraph.Nodes.Count == 4);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            bool tutorMode = false;
            HCIReasoner.Instance.InitMode(tutorMode);
        }

        [Test]
        public void Test_Problem_01_WorkedExample_0()
        {
            Init1_WorkedExample();

            // general solving ability
            const string input1 = "A(2,0)";
            const string input2 = "B(5,4)";
            const string query = "d=";

            HCIReasoner.Instance.HCILoad(input1);
            HCIReasoner.Instance.HCILoad(input2);
            var obj = HCIReasoner.Instance.HCILoad(query);
            Assert.NotNull(obj);
            var agQueryExpr = obj as AGQueryExpr;
            Assert.NotNull(agQueryExpr);
            agQueryExpr.RetrieveRenderKnowledge();
            Assert.True(agQueryExpr.RenderKnowledge != null);
            Assert.True(agQueryExpr.RenderKnowledge.Count == 1);

            var agPropertyExpr = agQueryExpr.RenderKnowledge[0] as AGPropertyExpr;
            Assert.True(agPropertyExpr != null);
            agPropertyExpr.IsSelected = true; // user select knowledge answer

            //select query knowledge
            HCIReasoner.Instance.QueriedKnowledge = agQueryExpr;
            Assert.NotNull(HCIReasoner.Instance.ObjectGraph);
            Assert.True(HCIReasoner.Instance.ObjectGraph.Nodes.Count == 4);

            ////////////////////////////////////////////////////////////////////
            //query its internal procedures
            string message;
            object output;
            QueryFeedbackState state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.DemonQueryStarted);
            //Assert.True(HCIReasoner.Instance.TraceLeftCount == 9);

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.DemonQueryProcessed);

/*            Assert.True(HCIReasoner.Instance.TraceLeftCount == 8);*/

            state = HCIReasoner.Instance.Query(null, out message, out output);
            var currentNode = HCIReasoner.Instance.CurrentStateNode;
            int index = HCIReasoner.Instance.ObjectGraph.SearchOuterLoopNodeIndex(currentNode);
            Assert.True(index == 2);
            Assert.True(HCIReasoner.Instance.TraceLeftCount == 7);

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(HCIReasoner.Instance.TraceLeftCount == 6);

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(HCIReasoner.Instance.TraceLeftCount == 5);

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(HCIReasoner.Instance.TraceLeftCount == 4);

            /* ////////////////////////////////////////////////////////////////////

             HCIReasoner.Instance.Reset();
             Reasoner.Instance.Reset();*/
        }


        #endregion
    }
}
