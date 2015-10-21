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

namespace MathCog.UserModeling
{
    using System.Collections.Generic;
    using NUnit.Framework;

    [TestFixture]
    public class TestProblem99
    {
        /*
         * Simplify the expression 1+2+3?
         */
        [SetUp]
        public void Init()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 99;

            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            Assert.True(result);
            //have the user authoring data
            Assert.True(HCIReasoner.Instance.UserGraph.Nodes.Count == 2);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);
        }

        [Test]
        public void Test_Problem_99_Demonstration()
        {
            bool tutorMode = false;
            HCIReasoner.Instance.InitMode(tutorMode);

            bool userInput = false;

            const string fact1 = "1+2+3=";
            var queryExpr = HCIReasoner.Instance.HCILoad(fact1, null, userInput) as AGQueryExpr;
            Assert.NotNull(queryExpr);
            queryExpr.RetrieveRenderKnowledge();
            Assert.True(queryExpr.RenderKnowledge.Count == 1);

            var agEquationExpr = queryExpr.RenderKnowledge[0] as AGEquationExpr;
            Assert.True(agEquationExpr != null);
            agEquationExpr.IsSelected = true; // user select knowledge answer

            //select query knowledge
            HCIReasoner.Instance.QueriedKnowledge = queryExpr;
            Assert.NotNull(HCIReasoner.Instance.ObjectGraph);
            Assert.True(HCIReasoner.Instance.ObjectGraph.Nodes.Count == 2);

            ////////////////////////////////////////////////////////////////////

            //query its internal procedures
            string message;
            object output;
            QueryFeedbackState state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.NotNull(output);
            Assert.True(state == QueryFeedbackState.DemonQueryStarted);
            Assert.True(HCIReasoner.Instance.TraceLeftCount == 2);

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.DemonQueryProcessed);
            Assert.True(HCIReasoner.Instance.TraceLeftCount == 1);

            userInput = true;

            const string userFact00 = "3+3";
            var userEqExpr00 = HCIReasoner.Instance.HCILoad(userFact00, null, userInput) as IKnowledge;
            Assert.NotNull(userEqExpr00);

            state = HCIReasoner.Instance.Query(userEqExpr00, out message, out output);
            Assert.True(state == QueryFeedbackState.DemonQueryVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            const string userFact0 = "5+1=6";
            var userEqExpr0= HCIReasoner.Instance.HCILoad(userFact0, null, userInput) as IKnowledge;
            Assert.NotNull(userEqExpr0);

            state = HCIReasoner.Instance.Query(userEqExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.DemonQueryVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyWrong));

            const string userFact = "3+3=6";
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
            Assert.True(state == QueryFeedbackState.DemonQueryProcessed);
        }

        [Test]
        public void Test_Problem_99_Tutoring()
        {
            bool tutorMode = true;
            HCIReasoner.Instance.InitMode(tutorMode);
            bool userInput = tutorMode;

            //drag input
            const string fact1 = "1+2+3=";
            var queryExpr = HCIReasoner.Instance.HCILoad(fact1, null, userInput) as AGQueryExpr;
            Assert.NotNull(queryExpr);

            //In tutor mode, it does not matter if the user selects 
            //query knowledge or not.
            //HCIReasoner.Instance.QueriedKnowledge = queryExpr;
            //query its internal procedures
            string message;
            object output;
            QueryFeedbackState state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryStarted);
            var strategies = output as List<string>;
            Assert.NotNull(strategies);
            Assert.True(strategies.Count == 1);

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedHint);

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedAnswer);

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedHint);

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedAnswer);

            state = HCIReasoner.Instance.Query(null, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryEnded);

            const string userFact = "1+5=6";
            var userEqExpr = HCIReasoner.Instance.HCILoad(userFact) as AGEquationExpr;
            Assert.NotNull(userEqExpr);

            state = HCIReasoner.Instance.Query(userEqExpr, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            const string userFact1 = "5+1=6";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as AGEquationExpr;
            Assert.NotNull(userEqExpr1);

            state = HCIReasoner.Instance.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            const string userFact2 = "3+4=6";
            var userEqExpr2 = HCIReasoner.Instance.HCILoad(userFact2) as AGEquationExpr;
            Assert.NotNull(userEqExpr2);

            state = HCIReasoner.Instance.Query(userEqExpr2, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyWrong));

            const string userFact3 = "x-x+6=6";
            var userEqExpr3 = HCIReasoner.Instance.HCILoad(userFact3) as AGEquationExpr;
            Assert.NotNull(userEqExpr3);

            state = HCIReasoner.Instance.Query(userEqExpr3, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));
        }
    }
}
