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
         * Simplify the expression x+1+x-4?
         * 
         */
        public void Init_97_Tutor()
        {
            const int problemIndex = 97;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            bool tutorMode = true;
            HCIReasoner.Instance.InitMode(tutorMode);
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
             */
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
            Reasoner.Instance.Reset();*/
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
             */

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
             */
            string userFact1 = "x+x-2";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as IKnowledge;
            Assert.NotNull(userEqExpr1);

            state = HCIReasoner.Instance.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyWrong));

           /* /////////////////////////////////////////////////////
            HCIReasoner.Instance.Reset();
            Reasoner.Instance.Reset();*/
        }
    }
}