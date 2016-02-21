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

namespace MathCog.UserModeling
{
    using NUnit.Framework;

    [TestFixture]
    public class TestProblems
    {
        /*
         * Problem 1: Find the distance betweeen A(2,0) and B(5,4)?
         * 
         * Problem 2: There exists two points A(2,4) and B(5,v), the distance between A and B is 5. 
         *  What is the value of v?
         *  
         * Problem 4:  There is a line, the slope of it is 3, the y-intercept of it is 2. 
         * What is the slope intercept form of this line? 
         * What is the general form of this line?
         * 
         * Problem 5: Given an equation 2y+2x-y+2x+4=0, graph this equation's corresponding shape?
         * What is the slope of this line? 
         * 
         * Problem 6: A line passes through points (2,3) and (4,y), the slope of this line is 5. 
         * What is the y-intercept of the line?
         * 
         * Problem 10: Line A contains the point (0,5) and is perpendicular to the line B 4y=x, what is the slope (use notation m) of line A? 
         * what is the general form of line A?
         * 
         * Problem 16: Find the midpoint of the line joining A(-1,2) and B(5,8).
         * 
         * Problem 28: There are two points A(2,y) and B(-1,4). 
         * The y-coordinate of point A is -1. 
         * What is the distance betweeen these two points?
         * (Use notation d to represent distance and round to 1 decimal place.)
         * 
         * 
         * Problem 29: Line A passes through two points (4,3) and (2,v). 
         * The line is perpendicular to line B in which the slope of line B is 1/2. 
         * What is the value of v? 
         * (Use notation m to represent line B's slope and m1 as line A's)
         * 
         * Problem 96: Simplify the expression x+2-1?
         * 
         * Problem 97: Simplify the expression x+1+x-4?
         * 
         * Problem 99: Simplify the expression 1+2+3?
         */

        #region Problem 1

        /*        #region Tutoring

        public void Init1_Tutoring()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 1;
            var reasoner = new HCIReasoner();
            bool result = reasoner.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(reasoner.ObjectGraph);

            HCIReasoner.TutorMode = true;
            reasoner.InitMode();
            //Not this one
            Assert.True(reasoner.RelationGraph.Nodes.Count == 0);
            //Verifier
            Assert.True(reasoner.RelationGraph.Nodes.Count == 3);
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
             #1# 
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
             #1# 
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
             #1#
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
             #1#
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
             #1#
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
             #1#
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
             #1#
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
             #1#
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
             #1#
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
             #1#
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
       /*      #2#
            var lss = MockSegment();
            var userShapeExpr2 = HCIReasoner.Instance.HCILoad(lss, ShapeType.LineSegment) as AGShapeExpr;
            Assert.NotNull(userShapeExpr2);

            state = HCIReasoner.Instance.Query(userShapeExpr2, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));#1#
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

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();
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

/*            Assert.True(HCIReasoner.Instance.TraceLeftCount == 8);#1#

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
             Reasoner.Instance.Reset();#1#
        }


        #endregion*/

        #endregion

        #region Problem 2

       /* #region Tutoring

        public void Init2_Tutor()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 2;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            //Assert.True(HCIReasoner.Instance.UserGraph.Nodes.Count == 3);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();

            //Not this one
            Assert.True(HCIReasoner.Instance.RelationGraph.Nodes.Count == 0);
            //Verifier
            Assert.True(Reasoner.Instance.RelationGraph.Nodes.Count == 6);
        }

        [Test]
        public void Test_Problem_02_Tutor_0()
        {
            Init2_Tutor();

            ///////////////////////////////////////////////////////
            /*
             * User Input: v = 8
             * Expect result: Correct Track
             #1#
            string userFact0 = "v=8";
            var userPropExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as AGPropertyExpr;
            Assert.NotNull(userPropExpr0);
            string message;
            object output;
            QueryFeedbackState state = HCIReasoner.Instance.Query(userPropExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvingPartialProblem));

            /*
            * User Input: v = 0
            * Expect result: Correct Track
            #1#
            string userFact1 = "v=0";
            var userPropExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as AGPropertyExpr;
            Assert.NotNull(userPropExpr1);
            state = HCIReasoner.Instance.Query(userPropExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvedProblem));

            /* /////////////////////////////////////////////////////
             HCIReasoner.Instance.Reset();
             Reasoner.Instance.Reset();#1#
        }

        #endregion

        #region Worked Example

        public void Init2_WorkedExample()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 2;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.True(HCIReasoner.Instance.UserGraph.Nodes.Count == 3);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();
        }

        [Test]
        public void Test_Problem_02_WorkedExample_0()
        {
            Init2_WorkedExample();

            const string input1 = "A(2,4)";
            const string input2 = "B(5,v)";
            const string input3 = "d=5";
            const string query = "v=";

            HCIReasoner.Instance.HCILoad(input1);
            HCIReasoner.Instance.HCILoad(input2);
            HCIReasoner.Instance.HCILoad(input3);
            HCIReasoner.Instance.HCILoad(query);

            var agShapeExpr = Reasoner.Instance.SearchKnowledge(input2) as AGShapeExpr;
            Assert.NotNull(agShapeExpr);

            agShapeExpr.RetrieveRenderKnowledge();
            Assert.True(agShapeExpr.RenderKnowledge != null);
            Assert.True(agShapeExpr.RenderKnowledge.Count == 2);

            var agShapeExpr1 = agShapeExpr.RenderKnowledge[1] as AGShapeExpr;
            Assert.True(agShapeExpr1 != null);
            agShapeExpr1.IsSelected = true; // user select knowledge answer

            //select query knowledge
            HCIReasoner.Instance.QueriedKnowledge = agShapeExpr;
            Assert.NotNull(HCIReasoner.Instance.ObjectGraph);
            Assert.True(HCIReasoner.Instance.ObjectGraph.Nodes.Count == 4);


            /* ////////////////////////////////////////////////////////////////////

             HCIReasoner.Instance.Reset();
             Reasoner.Instance.Reset();#1#
        }

        [Test]
        public void Test_Problem_02_WorkedExample_1()
        {
            #region Same as previous test case, init

            Init2_WorkedExample();

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();

            const string input1 = "A(2,4)";
            const string input2 = "B(5,v)";
            const string input3 = "d=5";
            const string query = "v=";

            HCIReasoner.Instance.HCILoad(input1);
            HCIReasoner.Instance.HCILoad(input2);
            HCIReasoner.Instance.HCILoad(input3);
            HCIReasoner.Instance.HCILoad(query);

            var agShapeExpr = Reasoner.Instance.SearchKnowledge(input2) as AGShapeExpr;
            Assert.NotNull(agShapeExpr);

            agShapeExpr.RetrieveRenderKnowledge();
            Assert.True(agShapeExpr.RenderKnowledge != null);
            Assert.True(agShapeExpr.RenderKnowledge.Count == 2);

            var agShapeExpr1 = agShapeExpr.RenderKnowledge[1] as AGShapeExpr;
            Assert.True(agShapeExpr1 != null);
            agShapeExpr1.IsSelected = true; // user select knowledge answer

            //select query knowledge
            HCIReasoner.Instance.QueriedKnowledge = agShapeExpr;
            Assert.NotNull(HCIReasoner.Instance.ObjectGraph);
            Assert.True(HCIReasoner.Instance.ObjectGraph.Nodes.Count == 4);

            #endregion

            //Problem trace navigation
            //TODO

            /*            //query its internal procedures
                        string message;
                        object output;
                        QueryFeedbackState state = HCIReasoner.Instance.Query(null, out message, out output);
                        Assert.True(state == QueryFeedbackState.DemonQueryStarted);
                        //Assert.True(HCIReasoner.Instance.TraceLeftCount == 1);

                        //TODO trace issue
                        var tuple4 = output as Tuple<object, object, object, object>;

                        state = HCIReasoner.Instance.Query(null, out message, out output);
                        Assert.True(HCIReasoner.Instance.TraceLeftCount == 0);

                        Assert.NotNull(tuple4);
                        var agExpr = tuple4.Item2 as AGShapeExpr;
                        Assert.NotNull(agExpr);
                        Assert.True(agExpr.ShapeSymbol.ToString().Equals("B(5,8)"));

                        ////////////////////////////////////////////////////////////////
                        agShapeExpr1.IsSelected = false;
                        HCIReasoner.Instance.CurrentStateNode = null;
                        ////////////////////////////////////////////////////////////////

                        var agShapeExpr0 = agShapeExpr.RenderKnowledge[0] as AGShapeExpr;
                        Assert.True(agShapeExpr0 != null);
                        agShapeExpr0.IsSelected = true; // user select knowledge answer

                        //select query knowledge
                        HCIReasoner.Instance.QueriedKnowledge = agShapeExpr;
                        Assert.NotNull(HCIReasoner.Instance.ObjectGraph);
                        Assert.True(HCIReasoner.Instance.ObjectGraph.Nodes.Count == 2);

                        //query its internal procedures
                        state = HCIReasoner.Instance.Query(null, out message, out output);
                        Assert.True(state == QueryFeedbackState.DemonQueryStarted);
                        Assert.True(HCIReasoner.Instance.TraceLeftCount == 1);

                        state = HCIReasoner.Instance.Query(null, out message, out output);
                        Assert.True(HCIReasoner.Instance.TraceLeftCount == 0);
                        tuple4 = output as Tuple<object, object, object, object>;
                        Assert.NotNull(tuple4);
                        agExpr = tuple4.Item2 as AGShapeExpr;
                        Assert.NotNull(agExpr);
                        Assert.True(agExpr.ShapeSymbol.ToString().Equals("B(5,0)"));#1#


            /*  ////////////////////////////////////////////////////////////////////

              HCIReasoner.Instance.Reset();
              Reasoner.Instance.Reset();#1#
        }*/

        //#endregion


        #endregion

        #region Problem 4

       /* #region Tutoring

        public void Init4_Tutor()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 4;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();
        }

        [Test]
        public void Test_Problem_04_Tutor_0()
        {
            Init4_Tutor();

            /////////////////////////////////////////////////////

            /*
             * User Input: y=3x+2
             * Expect result: Correct Track
             #1#
            string userFact0 = "y=3x+2";
            var userEqExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as AGEquationExpr;
            Assert.NotNull(userEqExpr0);
            string message;
            object output;
            QueryFeedbackState state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvingPartialProblem));

            /*
             * User Input: 3x-y+2=0
             * Expect result: Correct Track
             #1#
            string userFact1 = "3x-y+2=0";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as AGEquationExpr;
            Assert.NotNull(userEqExpr1);
            state = HCIReasoner.Instance.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvedProblem));

            /*  /////////////////////////////////////////////////////
              HCIReasoner.Instance.Reset();
              Reasoner.Instance.Reset();#1#
        }

        [Test]
        public void Test_Problem_04_Tutor_1()
        {
            Init4_Tutor();
            /////////////////////////////////////////////////////
            /*
             * User Input: k=2
             * Expect result: Correct Track
             #1#
            string userFact0 = "k=2";
            var userEqExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as AGPropertyExpr;
            Assert.NotNull(userEqExpr0);
            string message;
            object output;
            QueryFeedbackState state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /*
             * User Input: m=3
             * Expect result: Correct Track
             #1#

            string userFact1 = "m=3";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as AGPropertyExpr;
            Assert.NotNull(userEqExpr1);
            state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /*
             * User Input: y=mx+k
             * Expect result: Correct Track
             #1#
            string userFact2 = "y=mx+k";
            var userEqExpr2 = HCIReasoner.Instance.HCILoad(userFact2) as AGEquationExpr;
            Assert.NotNull(userEqExpr2);
            state = HCIReasoner.Instance.Query(userEqExpr2, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /*
             * User Input: y=3x+k
             * Expect result: Correct Track
             #1#

            string userFact3 = "y=3x+k";
            var userEqExpr3 = HCIReasoner.Instance.HCILoad(userFact3) as AGEquationExpr;
            Assert.NotNull(userEqExpr3);
            state = HCIReasoner.Instance.Query(userEqExpr3, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /*
             * User Input: y=mx+2
             * Expect result: Correct Track
             #1#
            string userFact4 = "y=mx+2";
            var userEqExpr4 = HCIReasoner.Instance.HCILoad(userFact4) as AGEquationExpr;
            Assert.NotNull(userEqExpr4);
            state = HCIReasoner.Instance.Query(userEqExpr4, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));
            /*
                        /////////////////////////////////////////////////////
                        HCIReasoner.Instance.Reset();
                        Reasoner.Instance.Reset();#1#
        }

        [Test]
        public void Test_Problem_04_Tutor_2()
        {
            Init4_Tutor();

            /////////////////////////////////////////////////////

            string message;
            object output;

            QueryFeedbackState state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedHint);

            var tuple3 = output as Tuple<object, object, object>;
            Assert.NotNull(tuple3);
            var strategyLst = tuple3.Item2 as List<string>;
            Assert.NotNull(strategyLst);
            Assert.True(strategyLst.Count == 1);

            ///////////////////////////////////////////////////////////
            /*
             * User Input: y=3x+2
             * Expect result: Correct Track
             #1#
            string userFact0 = "y=3x+2";
            var userEqExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as AGEquationExpr;
            Assert.NotNull(userEqExpr0);

            state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvingPartialProblem));

            /////////////////////////////////////////////////////////

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedHint);

            tuple3 = output as Tuple<object, object, object>;
            Assert.NotNull(tuple3);
            strategyLst = tuple3.Item2 as List<string>;
            Assert.NotNull(strategyLst);
            Assert.True(strategyLst.Count == 1);
        }

        #endregion

        #region Worked Example

        /*
         * There is a line, the slope of it is 3, the y-intercept of it is 2. 
         * What is the slope intercept form of this line? 
         * What is the general form of this line?
         #1#
        public void Init4_WorkedExample()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 4;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);
            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();
        }

        public void Test_Problem_04_WorkedExample_0()
        {
            Init4_WorkedExample();

            /*  ////////////////////////////////////////////////////////////////////

              HCIReasoner.Instance.Reset();
              Reasoner.Instance.Reset();#1#
        }


        #endregion*/


        #endregion

        #region Problem 5

/*        public void Init5_Tutor()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 5;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();
        }

        [Test]
        public void Test_Problem_05_Tutor_01()
        {
            Init5_Tutor();

            /////////////////////////////////////////////////////

            /*
             * User Input: 4x+y+4=0
             * Expect result: Correct Track
             #1#
            string userFact0 = "4x+y+4=0";
            var userEqExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as AGEquationExpr;
            Assert.NotNull(userEqExpr0);
            string message;
            object output;
            QueryFeedbackState state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /*
             * User Input: x=0
             * 
             * Expect result: Correct Track //TODO But it was wrong now,
             #1#
            string userFact1 = "x=0";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as AGPropertyExpr;
            Assert.NotNull(userEqExpr1);
            state = HCIReasoner.Instance.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyWrong));

            /*
             * User Input: y=-4
             * 
             * Expect result: Correct Track //TODO But it was wrong now,
             #1#

            /*
             * User Input: (0,-4)
             * 
             * Expect result: Correct Track 
             #1#
            string userFact2 = "(0,-4)";
            var userEqExpr2 = HCIReasoner.Instance.HCILoad(userFact2) as AGShapeExpr;
            Assert.NotNull(userEqExpr2);
            state = HCIReasoner.Instance.Query(userEqExpr2, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /*
             * User Input: (-1,0)
             * 
             * Expect result: Correct Track 
             #1#
            string userFact3 = "(-1,0)";
            var userEqExpr3 = HCIReasoner.Instance.HCILoad(userFact3) as AGShapeExpr;
            Assert.NotNull(userEqExpr3);
            state = HCIReasoner.Instance.Query(userEqExpr3, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /*
             * Plot line onto the geometry canvas
             * 4x+y+4=0
             * 
             * Expect result: partial problem-solved
             #1#

            /*     /////////////////////////////////////////////////////
                 HCIReasoner.Instance.Reset();
                 Reasoner.Instance.Reset();#1#
        }

        [Test]
        public void Test_Problem_05_Tutor_02()
        {
            Init5_Tutor();

            /////////////////////////////////////////////////////

            /*
              * User Input: y+4x+4=0
              * Expect result: Correct Track
              #1#
            string userFact0 = "y+4x+4=0";
            var userEqExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as AGEquationExpr;
            Assert.NotNull(userEqExpr0);
            string message;
            object output;
            QueryFeedbackState state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            string userFact1 = "2x+2x+y+4=0";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as AGEquationExpr;
            Assert.NotNull(userEqExpr1);
            state = HCIReasoner.Instance.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            string userFact2 = "(0,-4)";
            var userEqExpr2 = HCIReasoner.Instance.HCILoad(userFact2) as AGShapeExpr;
            Assert.NotNull(userEqExpr2);
            state = HCIReasoner.Instance.Query(userEqExpr2, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));
        }

        [Test]
        public void Test_Problem_05_Tutor_03()
        {
            Init5_Tutor();

            /////////////////////////////////////////////////////
            string message;
            object output;

            QueryFeedbackState state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedHint);

            var tuple3 = output as Tuple<object, object, object>;
            Assert.NotNull(tuple3);
            var strategyLst = tuple3.Item2 as List<string>;
            Assert.NotNull(strategyLst);
            Assert.True(strategyLst.Count == 3);

            //////////////////////////////////////////////////

            /*
             * User Input: 4x+y+4=0
             * Expect result: Correct Track
             #1#
            string userFact0 = "4x+y+4=0";
            var userEqExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as AGEquationExpr;
            Assert.NotNull(userEqExpr0);
            state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvingPartialProblem));

            ///////////////////////////////////////////////////

            string userFact1 = "m=-4";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as AGEquationExpr;
            Assert.NotNull(userEqExpr1);
            state = HCIReasoner.Instance.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvingPartialProblem));
        }

        [Test]
        public void Test_Problem_05_Tutor_04()
        {
            Init5_Tutor();

            /////////////////////////////////////////////////////
            string message;
            object output;

            QueryFeedbackState state;
            Tuple<object, object, object> tuple3;
            List<string> strategyLst;

            /*
             * User Input: 4x+y+4=0
             * Expect result: Correct Track
             #1#
            string userFact0 = "2y+2x-y+2x+4=0";
            var userEqExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as AGEquationExpr;
            Assert.NotNull(userEqExpr0);

            var shapeExpr0 = userEqExpr0 as AGShapeExpr;
            Assert.Null(shapeExpr0);

            state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            ///////////////////////////////////////////////////

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedHint);

            tuple3 = output as Tuple<object, object, object>;
            Assert.NotNull(tuple3);
            strategyLst = tuple3.Item2 as List<string>;
            Assert.NotNull(strategyLst);
            Assert.True(strategyLst.Count == 3);
        }*/



        #endregion

        #region Problem 6

        /*public void Init6_Tutor()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 6;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();
        }

        [Test]
        public void Test_Problem_06_Tutor_01()
        {
            Init6_Tutor();

            /////////////////////////////////////////////////////
            string message;
            object output;
            QueryFeedbackState state;

            /*
             * User Input: m = (y-3)/(4-2)
             * Expect result: Correct Track, //TODO this should match.
             #1#
            Expr expr1 = ExprMock.Mock5();
            var userEqExpr3 = HCIReasoner.Instance.HCILoad(expr1) as AGEquationExpr;
            Assert.NotNull(userEqExpr3);

            state = HCIReasoner.Instance.Query(userEqExpr3, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyWrong));

            /*
             * User Input : (y-3)/(4-2) = 5
             * Expect result: Correct Track
             * 
             #1#
            Expr expr = ExprMock.Mock4();
            var userEqExpr2 = HCIReasoner.Instance.HCILoad(expr) as AGEquationExpr;
            Assert.NotNull(userEqExpr2);

            state = HCIReasoner.Instance.Query(userEqExpr2, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /*
             * User Input : y-3=10
             * Expect result: Correct Track
             * 
             #1#
            string userFact1 = "y-3=10";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as AGEquationExpr;
            Assert.NotNull(userEqExpr1);

            state = HCIReasoner.Instance.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /*
             * User Input : y=3+10
             * Expect result: Correct Track
             * 
             #1#

            string userFact0 = "y=3+10";
            var userEqExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as AGEquationExpr;
            Assert.NotNull(userEqExpr0);

            state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));
        }

        [Test]
        public void Test_Problem_06_Tutor_02()
        {
            Init6_Tutor();

            /////////////////////////////////////////////////////
            string message;
            object output;
            QueryFeedbackState state;

            /*
             * User Input: lineG
             * Expect result: Correct Track, //TODO this should match.
             #1#
            /*  string expr1 = "lineG";
              var userEqExpr3 = HCIReasoner.Instance.HCILoad(expr1) as AGQueryExpr;
              Assert.NotNull(userEqExpr3);

              state = HCIReasoner.Instance.Query(userEqExpr3, out message, out output);
              Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
              Assert.NotNull(message);
              Assert.True(message.Equals(AGTutorMessage.VerifyWrong));#1#

            /*            /*
                        * User Input: lineSegG
                        * Expect result: Correct Track, //TODO this should match.
                        #2#
                        string expr2 = "lineSegG";
                        var userEqExpr4 = HCIReasoner.Instance.HCILoad(expr2);
                        Assert.Null(userEqExpr4);#1#
        }


        [Test]
        public void Test_Problem_06_Tutor_03()
        {
            Init6_Tutor();

            /////////////////////////////////////////////////////
            string message;
            object output;

            QueryFeedbackState state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedHint);

            var tuple3 = output as Tuple<object, object, object>;
            Assert.NotNull(tuple3);
            var strategyLst = tuple3.Item2 as List<string>;
            Assert.NotNull(strategyLst);
            Assert.True(strategyLst.Count == 2);

            //////////////////////////////////////////////////

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedAnswer);

            //////////////////////////////////////////////////

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedHint);
        }*/


        #endregion

        #region Problem 10

       /* public void Init10_Tutor()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 10;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();
        }

        [Test]
        public void Test_Problem_10_Tutor_01()
        {
            Init10_Tutor();

            /////////////////////////////////////////////////////
            string message;
            object output;
            QueryFeedbackState state;

            /*
             * User Input: -4x-y+5=0
             * Expect result: Correct Track, //TODO this should match.
             #1#
            string input0 = "-4x-y+5=0";
            var userEqExpr3 = HCIReasoner.Instance.HCILoad(input0) as AGEquationExpr;
            Assert.NotNull(userEqExpr3);

            state = HCIReasoner.Instance.Query(userEqExpr3, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvingPartialProblem));





        }*/


        #endregion

        #region Problem 16

  /*      public void Init16_Tutor()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 16;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();
        }

        [Test]
        public void Test_Problem_16_Tutor_01()
        {
            Init16_Tutor();

            /////////////////////////////////////////////////////
            string message;
            object output;
            QueryFeedbackState state;

            /*
             * User Input: A(-1,2)
             * Expect result: Correct Track, //TODO this should match.
             #1#
            string txt = "A(-1,2)";
            var userEqExpr3 = HCIReasoner.Instance.HCILoad(txt) as AGShapeExpr;
            Assert.NotNull(userEqExpr3);

            state = HCIReasoner.Instance.Query(userEqExpr3, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /*
             * User Input: (2,5)
             * Expect result: Correct Track
             #1#
            txt = "(2,5)";
            var userEqExpr2 = HCIReasoner.Instance.HCILoad(txt) as AGShapeExpr;
            Assert.NotNull(userEqExpr2);

            state = HCIReasoner.Instance.Query(userEqExpr2, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvedProblem));

            txt = "x=(-1+5)/2";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(txt) as AGEquationExpr;
            Assert.NotNull(userEqExpr1);

            state = HCIReasoner.Instance.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));


            txt = "x=(5-1)/2";
            var userEqExpr5 = HCIReasoner.Instance.HCILoad(txt) as AGEquationExpr;
            Assert.NotNull(userEqExpr5);

            state = HCIReasoner.Instance.Query(userEqExpr5, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));
        }*/


        #endregion

        #region Problem 28

        /*public void Init28_Tutor()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 28;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();
        }

        [Test]
        public void Test_Problem_28_Tutor()
        {
            Init28_Tutor();

            /////////////////////////////////////////////////////
            string message;
            object output;
            QueryFeedbackState state;

            /*
             * User Input: (2,-1)
             * Expect result: Correct Track
             #1#

            string userFact0 = "(2,-1)";
            var userEqExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as AGShapeExpr;
            Assert.NotNull(userEqExpr0);

            state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /*
             * User Input: d=5.8
             * Expect result: Solved Problem
             #1#
            const string userFact1 = "d=5.8";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as AGEquationExpr;
            Assert.NotNull(userEqExpr1);

            state = HCIReasoner.Instance.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvedProblem));
        }*/


        #endregion

        #region Problem 29

        /*public void Init29_Tutor()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 29;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();
        }

        [Test]
        public void Test_Problem_29_Tutor()
        {
            Init29_Tutor();

            /////////////////////////////////////////////////////
            string message;
            object output;
            QueryFeedbackState state;

            /*
             * User Input: m= -1/0.5
             * Expect result: Correct Track
             #1#

            Expr userFact0 = ExprMock.Mock6();
            var userEqExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as IKnowledge;
            Assert.NotNull(userEqExpr0);

            state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));
        }*/


        #endregion

        #region Problem 96

      /*  public void Init_96_Tutor()
        {
            const int problemIndex = 96;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();
        }

        [Test]
        public void Test_Problem_96_Tutor()
        {
            Init_96_Tutor();

            /////////////////////////////////////////////////////

            string message;
            object output;
            QueryFeedbackState state;

            /*
             * User Input: x+1
             * Expect result: Correct Track
             #1#

            string userFact0 = "x+1";
            var userEqExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as IKnowledge;
            Assert.NotNull(userEqExpr0);

            state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvedProblem));

            /*  /////////////////////////////////////////////////////
              HCIReasoner.Instance.Reset();
              Reasoner.Instance.Reset();#1#
        }*/


        #endregion

        #region Problem 97

        /*    [SetUp]
        public void Init()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 97;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);

            //have the user authoring data
            Assert.True(HCIReasoner.Instance.UserGraph.Nodes.Count == 2);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);
        }

        [Test]
        public void Test_Problem_97_Demonstration()
        {
            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();
        }

        [Test]
        public void Test_Problem_97_Tutoring()
        {
            HCIReasoner.TutorMode = true;
            HCIReasoner.Instance.InitMode();

            const string userFact = "-3+2x=2x-3";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(userFact) as AGEquationExpr;
            Assert.NotNull(userEqExpr1);

            string message;
            object output;

            QueryFeedbackState state = HCIReasoner.Instance.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            const string userFact2 = "1-4+x+x=2x-3";
            var userEqExpr2 = HCIReasoner.Instance.HCILoad(userFact2) as AGEquationExpr;
            Assert.NotNull(userEqExpr2);

            state = HCIReasoner.Instance.Query(userEqExpr2, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            Assert.True(Reasoner.Instance.RelationGraph.Nodes.Count == 1);

            const string userFact3 = "x+x-3";
            var userEqExpr3 = HCIReasoner.Instance.HCILoad(userFact3) as IKnowledge;
            Assert.NotNull(userEqExpr3);

            state = HCIReasoner.Instance.Query(userEqExpr3, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /////////////////////////////////////////////////////

            //Re-check through back-end Solver
            /*  state = HCIReasoner.Instance.Verify(userEqExpr3, out message, out output);
              Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
              Assert.NotNull(message);
              Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));#1#
        }*/
        /*public void Init_97_Tutor()
        {
            const int problemIndex = 97;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();
        }

        [Test]
        public void Test_Problem_97_Tutor_01()
        {
            Init_97_Tutor();

            /////////////////////////////////////////////////////

            string message;
            object output;
            QueryFeedbackState state;

            /*
             * User Input: x+x-2
             *
             #1#
            string userFact1 = "x+x-2";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as IKnowledge;
            Assert.NotNull(userEqExpr1);

            state = HCIReasoner.Instance.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyWrong));

            if (message.Equals(AGTutorMessage.VerifyWrong))
            {
                state = HCIReasoner.Instance.Query(null, out message, out output);
                Assert.True(state == QueryFeedbackState.TutorQueryProcessedHint);
            }
            /*            /////////////////////////////////////////////////////
                        HCIReasoner.Instance.Reset();
                        Reasoner.Instance.Reset();#1#
        }

        [Test]
        public void Test_Problem_97_Tutor_02()
        {
            Init_97_Tutor();

            /////////////////////////////////////////////////////

            string message;
            object output;
            QueryFeedbackState state;

            /*
             * User Input: x+x-4+1
             * Expect result: Correct Track
             #1#

            string userFact0 = "x+x-4+1";
            var userEqExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as IKnowledge;
            Assert.NotNull(userEqExpr0);

            state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /*
             * User Input: x+x-2
             *
             #1#
            string userFact1 = "x+x-2";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as IKnowledge;
            Assert.NotNull(userEqExpr1);

            state = HCIReasoner.Instance.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyWrong));

            /* /////////////////////////////////////////////////////
             HCIReasoner.Instance.Reset();
             Reasoner.Instance.Reset();#1#
        }s*/

        #endregion

        #region Problem 99

        [Test]
        public void Test_Problem_99_Demonstration()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 99;

            var tutor = new HCIReasoner();

            bool result = tutor.InitProblem(problemIndex);
            Assert.True(result);
            //have the user authoring data
            Assert.True(tutor.UserGraph.Nodes.Count == 2);
            Assert.Null(tutor.ObjectGraph);

            HCIReasoner.TutorMode = false;
            tutor.InitMode();
			///////////////////////////////////////////////////////

            const string fact1 = "1+2+3=";
            var queryExpr = tutor.HCILoad(fact1) as AGQueryExpr;
            Assert.NotNull(queryExpr);
            queryExpr.RetrieveRenderKnowledge();
            Assert.True(queryExpr.RenderKnowledge.Count == 1);

            var agEquationExpr = queryExpr.RenderKnowledge[0] as AGEquationExpr;
            Assert.True(agEquationExpr != null);
            agEquationExpr.IsSelected = true; // user select knowledge answer

            //select query knowledge
            tutor.QueriedKnowledge = queryExpr;
            Assert.NotNull(tutor.ObjectGraph);
            Assert.True(tutor.ObjectGraph.Nodes.Count == 2);

            //query its internal procedures
            string message;
            object output;
            QueryFeedbackState state = tutor.Query(null, out message, out output);
            Assert.NotNull(output);
            Assert.True(state == QueryFeedbackState.DemonQueryStarted);
            Assert.True(tutor.TraceLeftCount == 2);

            state = tutor.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.DemonQueryProcessed);
            Assert.True(tutor.TraceLeftCount == 1);

            ////////////////////////////////////////////////////////////////////

            const string userFact00 = "3+3";
            var userEqExpr00 = tutor.HCILoad(userFact00, null, true) as IKnowledge;
            Assert.NotNull(userEqExpr00);
            state = tutor.Query(userEqExpr00, out message, out output);
            Assert.True(state == QueryFeedbackState.DemonQueryVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            const string userFact0 = "5+1=6";
            var userEqExpr0 = tutor.HCILoad(userFact0, null, true) as IKnowledge;
            Assert.NotNull(userEqExpr0);
            state = tutor.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.DemonQueryVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyWrong));

/*            const string userFact1 = "6";
            var userEqExpr1 = tutor.HCILoad(userFact1, null, true) as IKnowledge;
            Assert.NotNull(userEqExpr1);
            state = tutor.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.DemonQueryVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));*/

           /* const string userFact = "3+3=6";
            var userEqExpr = HCIReasoner.Instance.HCILoad(userFact, null, userInput) as IKnowledge;
            Assert.NotNull(userEqExpr);

            state = HCIReasoner.Instance.Query(userEqExpr, out message, out output);
            Assert.True(state == QueryFeedbackState.DemonQueryVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.DemonQueryProcessed);

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.DemonQueryEnded);

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.DemonQueryEnded);
            Assert.Null(output);

            state = HCIReasoner.Instance.Query(userEqExpr, out message, out output);
            Assert.True(state == QueryFeedbackState.DemonQueryVerify);
            Assert.Null(output);

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.DemonQueryProcessed);*/

        }

        [Test]
        public void Test_Problem_99_Tutoring()
        {
            HCIReasoner.TutorMode = true;
            var tutor = new HCIReasoner();
            tutor.InitProblem(99);

            //drag input
            const string fact1 = "1+2+3=";
            var queryExpr = tutor.HCILoad(fact1, null, true) as AGQueryExpr;
            Assert.NotNull(queryExpr);

            //In tutor mode, it does not matter if the user selects 
            //query knowledge or not.
            //HCIReasoner.Instance.QueriedKnowledge = queryExpr;
            //query its internal procedures
            string message;
            object output;

            QueryFeedbackState state = tutor.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedHint);

            state = tutor.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedAnswer);

            state = tutor.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedHint);

            state = tutor.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedAnswer);

            state = tutor.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryEnded);


            const string userFact = "1+5=6";
            var userEqExpr = tutor.HCILoad(userFact) as AGEquationExpr;
            Assert.NotNull(userEqExpr);

            state = tutor.Query(userEqExpr, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            const string userFact1 = "5+1=6";
            var userEqExpr1 = tutor.HCILoad(userFact1) as AGEquationExpr;
            Assert.NotNull(userEqExpr1);

            state = tutor.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            const string userFact2 = "3+4=6";
            var userEqExpr2 = tutor.HCILoad(userFact2) as AGEquationExpr;
            Assert.NotNull(userEqExpr2);

            state = tutor.Query(userEqExpr2, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyWrong));

            const string userFact3 = "x-x+6=6";
            var userEqExpr3 = tutor.HCILoad(userFact3) as AGEquationExpr;
            Assert.NotNull(userEqExpr3);

            state = tutor.Query(userEqExpr3, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));
        }

        #endregion
    }
}
