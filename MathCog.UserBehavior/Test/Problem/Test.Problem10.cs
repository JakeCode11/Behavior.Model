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
using MathCog;
using starPadSDK.MathExpr;

namespace UserModeling
{
    using NUnit.Framework;
    using MathCog.UserModeling;

    [TestFixture]
    public partial class TestProblems
    {
        /*
         * Line A contains the point (0,5) and is perpendicular to the line B 4y=x, what is the slope (use notation m) of line A? what is the general form of line A?
         * 
         */

        public void Init10_Tutor()
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
             */
            string input0 = "-4x-y+5=0";
            var userEqExpr3 = HCIReasoner.Instance.HCILoad(input0) as AGEquationExpr;
            Assert.NotNull(userEqExpr3);

            state = HCIReasoner.Instance.Query(userEqExpr3, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvingPartialProblem));





        }
    }
}
