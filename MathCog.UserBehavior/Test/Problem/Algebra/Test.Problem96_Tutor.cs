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

namespace UserModeling
{
    using NUnit.Framework;
    using MathCog.UserModeling;
    using MathCog;

    [TestFixture]
    public partial class TestProblems
    {
        /*
         * Simplify the expression x+2-1?
         *
         */
        public void Init_96_Tutor()
        {
            const int problemIndex = 96;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            bool tutorMode = true;
            HCIReasoner.Instance.InitMode(tutorMode);
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
             */

            string userFact0 = "x+1";
            var userEqExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as IKnowledge;
            Assert.NotNull(userEqExpr0);

            state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvedProblem));

          /*  /////////////////////////////////////////////////////
            HCIReasoner.Instance.Reset();
            Reasoner.Instance.Reset();*/
        }
    }
}
