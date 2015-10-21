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
using System.Collections.Generic;
using CSharpLogic;
using MathCog;

namespace UserModeling
{
    using NUnit.Framework;
    using MathCog.UserModeling;

    [TestFixture]
    public partial class TestProblems
    {
        /*
         * Given an equation 2y+2x-y+2x+4=0, graph this equation's corresponding shape?
         * What is the slope of this line? 
         */
        public void Init5_Tutor()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 5;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            bool tutorMode = true;
            HCIReasoner.Instance.InitMode(tutorMode);
        }

        [Test]
        public void Test_Problem_05_Tutor_01()
        {
            Init5_Tutor();

            /////////////////////////////////////////////////////
            
            /*
             * User Input: 4x+y+4=0
             * Expect result: Correct Track
             */
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
             */
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
             */

            /*
             * User Input: (0,-4)
             * 
             * Expect result: Correct Track 
             */
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
             */
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
             */

       /*     /////////////////////////////////////////////////////
            HCIReasoner.Instance.Reset();
            Reasoner.Instance.Reset();*/
        }

        [Test]
        public void Test_Problem_05_Tutor_02()
        {
            Init5_Tutor();

            /////////////////////////////////////////////////////

            /*
              * User Input: y+4x+4=0
              * Expect result: Correct Track
              */
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
             */
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
             */
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
        }

        
    }
}
