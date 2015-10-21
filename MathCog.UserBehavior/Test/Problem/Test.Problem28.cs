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

using MathCog;

namespace UserModeling
{
    using NUnit.Framework;
    using MathCog.UserModeling;

    [TestFixture]
    public partial class TestProblems
    {
        /*
         * Problem 28: There are two points A(2,y) and B(-1,4). 
         * The y-coordinate of point A is -1. 
         * What is the distance betweeen these two points?
         * (Use notation d to represent distance and round to 1 decimal place.)
         */
        public void Init28_Tutor()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 28;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            bool tutorMode = true;
            HCIReasoner.Instance.InitMode(tutorMode);
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
             */

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
             */
            const string userFact1 = "d=5.8";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as AGEquationExpr;
            Assert.NotNull(userEqExpr1);

            state = HCIReasoner.Instance.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvedProblem));
        }

    }
}
