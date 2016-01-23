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
    using MathCog;
    using MathCog.UserModeling;
    using NUnit.Framework;

    [TestFixture]
    public partial class TestProblems
    {
        /*
         * There exists two points A(2,4) and B(5,v), the distance between A and B is 5. 
         *  What is the value of v?
         */

        #region Tutoring

        public void Init2_Tutor()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 2;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            //Assert.True(HCIReasoner.Instance.UserGraph.Nodes.Count == 3);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();

            //Not this one
            Assert.True(HCIReasoner.Instance.RelationGraph.Nodes.Count == 0);
            //Verifier
            Assert.True(Reasoner.Instance.RelationGraph.Nodes.Count == 6);
        }

        [Test]
        public void Test_Problem_02_Tutor_0()
        {
            Init2_Tutor();

            ///////////////////////////////////////////////////////
            /*
             * User Input: v = 8
             * Expect result: Correct Track
             */
            string userFact0 = "v=8";
            var userPropExpr0 = HCIReasoner.Instance.HCILoad(userFact0) as AGPropertyExpr;
            Assert.NotNull(userPropExpr0);
            string message;
            object output;
            QueryFeedbackState state = HCIReasoner.Instance.Query(userPropExpr0, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvingPartialProblem));

            /*
            * User Input: v = 0
            * Expect result: Correct Track
            */
            string userFact1 = "v=0";
            var userPropExpr1 = HCIReasoner.Instance.HCILoad(userFact1) as AGPropertyExpr;
            Assert.NotNull(userPropExpr1);
            state = HCIReasoner.Instance.Query(userPropExpr1, out message, out output);
            Assert.True(state == QueryFeedbackState.TutorQueryProcessedVerify);
            Assert.NotNull(message);
            Assert.True(message.Equals(AGTutorMessage.SolvedProblem));

           /* /////////////////////////////////////////////////////
            HCIReasoner.Instance.Reset();
            Reasoner.Instance.Reset();*/
        }

        #endregion

        #region Worked Example

        public void Init2_WorkedExample()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 2;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            Assert.True(HCIReasoner.Instance.UserGraph.Nodes.Count == 3);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();
        }

        [Test]
        public void Test_Problem_02_WorkedExample_0()
        {
            Init2_WorkedExample();

            const string input1 = "A(2,4)";
            const string input2 = "B(5,v)";
            const string input3 = "d=5";
            const string query = "v=";

            HCIReasoner.Instance.HCILoad(input1);
            HCIReasoner.Instance.HCILoad(input2);
            HCIReasoner.Instance.HCILoad(input3);
            HCIReasoner.Instance.HCILoad(query);

            var agShapeExpr = Reasoner.Instance.SearchKnowledge(input2) as AGShapeExpr;
            Assert.NotNull(agShapeExpr);

            agShapeExpr.RetrieveRenderKnowledge();
            Assert.True(agShapeExpr.RenderKnowledge != null);
            Assert.True(agShapeExpr.RenderKnowledge.Count == 2);

            var agShapeExpr1 = agShapeExpr.RenderKnowledge[1] as AGShapeExpr;
            Assert.True(agShapeExpr1 != null);
            agShapeExpr1.IsSelected = true; // user select knowledge answer

            //select query knowledge
            HCIReasoner.Instance.QueriedKnowledge = agShapeExpr;
            Assert.NotNull(HCIReasoner.Instance.ObjectGraph);
            Assert.True(HCIReasoner.Instance.ObjectGraph.Nodes.Count == 4);


            /* ////////////////////////////////////////////////////////////////////

             HCIReasoner.Instance.Reset();
             Reasoner.Instance.Reset();*/
        }

        [Test]
        public void Test_Problem_02_WorkedExample_1()
        {
            #region Same as previous test case, init

            Init2_WorkedExample();

            HCIReasoner.TutorMode = false;
            HCIReasoner.Instance.InitMode();

            const string input1 = "A(2,4)";
            const string input2 = "B(5,v)";
            const string input3 = "d=5";
            const string query = "v=";

            HCIReasoner.Instance.HCILoad(input1);
            HCIReasoner.Instance.HCILoad(input2);
            HCIReasoner.Instance.HCILoad(input3);
            HCIReasoner.Instance.HCILoad(query);

            var agShapeExpr = Reasoner.Instance.SearchKnowledge(input2) as AGShapeExpr;
            Assert.NotNull(agShapeExpr);

            agShapeExpr.RetrieveRenderKnowledge();
            Assert.True(agShapeExpr.RenderKnowledge != null);
            Assert.True(agShapeExpr.RenderKnowledge.Count == 2);

            var agShapeExpr1 = agShapeExpr.RenderKnowledge[1] as AGShapeExpr;
            Assert.True(agShapeExpr1 != null);
            agShapeExpr1.IsSelected = true; // user select knowledge answer

            //select query knowledge
            HCIReasoner.Instance.QueriedKnowledge = agShapeExpr;
            Assert.NotNull(HCIReasoner.Instance.ObjectGraph);
            Assert.True(HCIReasoner.Instance.ObjectGraph.Nodes.Count == 4);

            #endregion

            //Problem trace navigation
            //TODO

            /*            //query its internal procedures
                        string message;
                        object output;
                        QueryFeedbackState state = HCIReasoner.Instance.Query(null, out message, out output);
                        Assert.True(state == QueryFeedbackState.DemonQueryStarted);
                        //Assert.True(HCIReasoner.Instance.TraceLeftCount == 1);

                        //TODO trace issue
                        var tuple4 = output as Tuple<object, object, object, object>;

                        state = HCIReasoner.Instance.Query(null, out message, out output);
                        Assert.True(HCIReasoner.Instance.TraceLeftCount == 0);

                        Assert.NotNull(tuple4);
                        var agExpr = tuple4.Item2 as AGShapeExpr;
                        Assert.NotNull(agExpr);
                        Assert.True(agExpr.ShapeSymbol.ToString().Equals("B(5,8)"));

                        ////////////////////////////////////////////////////////////////
                        agShapeExpr1.IsSelected = false;
                        HCIReasoner.Instance.CurrentStateNode = null;
                        ////////////////////////////////////////////////////////////////

                        var agShapeExpr0 = agShapeExpr.RenderKnowledge[0] as AGShapeExpr;
                        Assert.True(agShapeExpr0 != null);
                        agShapeExpr0.IsSelected = true; // user select knowledge answer

                        //select query knowledge
                        HCIReasoner.Instance.QueriedKnowledge = agShapeExpr;
                        Assert.NotNull(HCIReasoner.Instance.ObjectGraph);
                        Assert.True(HCIReasoner.Instance.ObjectGraph.Nodes.Count == 2);

                        //query its internal procedures
                        state = HCIReasoner.Instance.Query(null, out message, out output);
                        Assert.True(state == QueryFeedbackState.DemonQueryStarted);
                        Assert.True(HCIReasoner.Instance.TraceLeftCount == 1);

                        state = HCIReasoner.Instance.Query(null, out message, out output);
                        Assert.True(HCIReasoner.Instance.TraceLeftCount == 0);
                        tuple4 = output as Tuple<object, object, object, object>;
                        Assert.NotNull(tuple4);
                        agExpr = tuple4.Item2 as AGShapeExpr;
                        Assert.NotNull(agExpr);
                        Assert.True(agExpr.ShapeSymbol.ToString().Equals("B(5,0)"));*/


            /*  ////////////////////////////////////////////////////////////////////

              HCIReasoner.Instance.Reset();
              Reasoner.Instance.Reset();*/
        }



        #endregion

    }
}
