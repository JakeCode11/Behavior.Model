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
    using NUnit.Framework;

    [TestFixture]
    public class TestProblem97
    {
        /*
         * Simplify the expression x+1+x-4?
         * 
         */
        [SetUp]
        public void Init()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 97;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);

            //have the user authoring data
            Assert.True(HCIReasoner.Instance.UserGraph.Nodes.Count == 2);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);
        }

        [Test]
        public void Test_Problem_97_Demonstration()
        {
            bool tutorMode = false;
            HCIReasoner.Instance.InitMode(tutorMode);
        }

        [Test]
        public void Test_Problem_97_Tutoring()
        {
            bool tutorMode = true;
            HCIReasoner.Instance.InitMode(tutorMode);
            Assert.True(tutorMode);

            const string userFact = "-3+2x=2x-3";
            var userEqExpr1 = HCIReasoner.Instance.HCILoad(userFact) as AGEquationExpr;
            Assert.NotNull(userEqExpr1);

            string message;
            object output;

            QueryFeedbackState state = HCIReasoner.Instance.Query(userEqExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            const string userFact2 = "1-4+x+x=2x-3";
            var userEqExpr2 = HCIReasoner.Instance.HCILoad(userFact2) as AGEquationExpr;
            Assert.NotNull(userEqExpr2);

            state = HCIReasoner.Instance.Query(userEqExpr2, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            Assert.True(Reasoner.Instance.RelationGraph.Nodes.Count == 1);

            const string userFact3 = "x+x-3";
            var userEqExpr3 = HCIReasoner.Instance.HCILoad(userFact3) as IKnowledge;
            Assert.NotNull(userEqExpr3);

            state = HCIReasoner.Instance.Query(userEqExpr3, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));

            /////////////////////////////////////////////////////
            
            //Re-check through back-end Solver
          /*  state = HCIReasoner.Instance.Verify(userEqExpr3, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.VerifyCorrect));*/
        }
    }
}