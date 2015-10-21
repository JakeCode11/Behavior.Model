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
         * Problem 6: A line passes through points (2,3) and (4,y), the slope of this line is 5. What is the y-intercept of the line?
         */

        public void Init6_Tutor()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 6;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            bool tutorMode = true;
            HCIReasoner.Instance.InitMode(tutorMode);
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
             */
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
             */
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
             */
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
             */

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
             */
          /*  string expr1 = "lineG";
            var userEqExpr3 = HCIReasoner.Instance.HCILoad(expr1) as AGQueryExpr;
            Assert.NotNull(userEqExpr3);

            state = HCIReasoner.Instance.Query(userEqExpr3, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyWrong));*/

/*            /*
            * User Input: lineSegG
            * Expect result: Correct Track, //TODO this should match.
            #1#
            string expr2 = "lineSegG";
            var userEqExpr4 = HCIReasoner.Instance.HCILoad(expr2);
            Assert.Null(userEqExpr4);*/
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
        }

    }
}
