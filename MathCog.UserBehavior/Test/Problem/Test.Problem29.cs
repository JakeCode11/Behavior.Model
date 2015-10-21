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
         * Line A passes through two points (4,3) and (2,v). 
         * The line is perpendicular to line B in which the slope of line B is 1/2. 
         * What is the value of v? 
         * (Use notation m to represent line B's slope and m1 as line A's)
         * 
         */
        public void Init29_Tutor()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 29;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            bool tutorMode = true;
            HCIReasoner.Instance.InitMode(tutorMode);
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
             */

            Expr userFact0 = ExprMock.Mock6();
            var userEqExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as IKnowledge;
            Assert.NotNull(userEqExpr0);

            state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));
        }

    }
}
