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

namespace MathCogUserData
{
    using MathCog;
    using NUnit.Framework;
    using UserModeling;

    public static partial class ProblemAuthoringModel
    {
        /*
         * There is a line, the slope of it is 5, the y-intercept of it is 1. What is the slope intercept form of this line? What is the general form of this line? (Use notation m to represent line slope and k to represent y-intercept)
         * 
         */
        public static BehaviorGraph Problem53()
        {
            const string input1 = "m=5";
            const string input2 = "k=1";
            Reasoner.Instance.Load(input1);
            Reasoner.Instance.Load(input2);

            //Question 1:
            const string query1 = "lineS=";
            var obj1 = Reasoner.Instance.Load(query1);
            Assert.NotNull(obj1);
            var agQueryExpr1 = obj1 as AGQueryExpr;
            Assert.NotNull(agQueryExpr1);
            Assert.True(agQueryExpr1.RenderKnowledge == null);
            agQueryExpr1.RetrieveRenderKnowledge();
            Assert.True(agQueryExpr1.RenderKnowledge != null);
            Assert.True(agQueryExpr1.RenderKnowledge.Count == 1);

            var answerExpr1 = agQueryExpr1.RenderKnowledge[0] as AGShapeExpr;
            Assert.NotNull(answerExpr1);
            Assert.Null(answerExpr1.AutoTrace);
            answerExpr1.IsSelected = true;
            answerExpr1.GenerateSolvingTrace();
            Assert.NotNull(answerExpr1.AutoTrace);

            //////////////////////////////////////////////////////////
            //Question 2:
            const string query2 = "lineG=";
            var obj = Reasoner.Instance.Load(query2);
            Assert.NotNull(obj);
            var agQueryExpr2 = obj as AGQueryExpr;
            Assert.NotNull(agQueryExpr2);
            Assert.True(agQueryExpr2.RenderKnowledge == null);
            agQueryExpr2.RetrieveRenderKnowledge();
            Assert.True(agQueryExpr2.RenderKnowledge != null);
            Assert.True(agQueryExpr2.RenderKnowledge.Count == 1);

            var answerExpr2 = agQueryExpr2.RenderKnowledge[0] as AGShapeExpr;
            Assert.NotNull(answerExpr2);
            Assert.Null(answerExpr2.AutoTrace);
            answerExpr2.IsSelected = true;
            answerExpr2.GenerateSolvingTrace();
            Assert.NotNull(answerExpr2.AutoTrace);

            //Reasoner.Instance.Reset();
            var graph = new BehaviorGraph();
            graph.Insert(answerExpr1.AutoTrace);

            var answerNode1 = graph.SearchInnerLoopNode(answerExpr1);
            Assert.NotNull(answerNode1);
            graph.SolvingCache.Add(answerNode1, false);

            //Assert.True(graph.Nodes.Count == 3);

            graph.Update(answerExpr2.AutoTrace);
            var answerNode2 = graph.SearchInnerLoopNode(answerExpr2);
            Assert.NotNull(answerNode2);
            graph.SolvingCache.Add(answerNode2, false);

            return graph;
        }
    }
}
