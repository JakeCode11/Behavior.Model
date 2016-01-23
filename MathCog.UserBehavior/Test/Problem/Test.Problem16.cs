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
         * Find the midpoint of the line joining A(-1,2) and B(5,8).
         */

        public void Init16_Tutor()
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
             */
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
             */
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
        }
    }
}
