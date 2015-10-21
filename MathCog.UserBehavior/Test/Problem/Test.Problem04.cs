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

namespace UserModeling
{
    using MathCog;
    using MathCog.UserModeling;
    using NUnit.Framework;

    [TestFixture]
    public partial class TestProblems
    {
        /*
         * There is a line, the slope of it is 3, the y-intercept of it is 2. 
         * What is the slope intercept form of this line? 
         * What is the general form of this line?
         */

        #region Tutoring

        public void Init4_Tutor()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 4;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            bool tutorMode = true;
            HCIReasoner.Instance.InitMode(tutorMode);
        }

        [Test]
        public void Test_Problem_04_Tutor_0()
        {
            Init4_Tutor();

            /////////////////////////////////////////////////////

            /*
             * User Input: y=3x+2
             * Expect result: Correct Track
             */
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
             */
            string userFact1 = "3x-y+2=0";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as AGEquationExpr;
            Assert.NotNull(userEqExpr1);
            state = HCIReasoner.Instance.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvedProblem));

          /*  /////////////////////////////////////////////////////
            HCIReasoner.Instance.Reset();
            Reasoner.Instance.Reset();*/
        }

        [Test]
        public void Test_Problem_04_Tutor_1()
        {
            Init4_Tutor();
            /////////////////////////////////////////////////////
            /*
             * User Input: k=2
             * Expect result: Correct Track
             */
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
             */

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
             */
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
             */

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
             */
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
            Reasoner.Instance.Reset();*/
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
             */
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
         */
        public void Init4_WorkedExample()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 4;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);
            bool tutorMode = false;
            HCIReasoner.Instance.InitMode(tutorMode);
        }

        public void Test_Problem_04_WorkedExample_0()
        {
            Init4_WorkedExample();

            /*  ////////////////////////////////////////////////////////////////////

              HCIReasoner.Instance.Reset();
              Reasoner.Instance.Reset();*/
        }


        #endregion
    }
}
