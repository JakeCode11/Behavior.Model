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
         * There exists two points A(-2,4) and B(2,v), 
         * the distance between A and B is 5. What is the value of v? 
         * (Use notation d to represent distance)
         * 
         */

        public static BehaviorGraph Problem52()
        {
            const string input1 = "A(-2,4)";
            const string input2 = "B(2,v)";
            const string input3 = "d=5";
            const string query = "v=";

            Reasoner.Instance.Load(input1);
            Reasoner.Instance.Load(input2);
            Reasoner.Instance.Load(input3);
            var obj = Reasoner.Instance.Load(query);

            Assert.NotNull(obj);
            var agQueryExpr = obj as AGQueryExpr;
            Assert.NotNull(agQueryExpr);
            Assert.True(agQueryExpr.RenderKnowledge == null);
            agQueryExpr.RetrieveRenderKnowledge();
            Assert.True(agQueryExpr.RenderKnowledge != null);
            Assert.True(agQueryExpr.RenderKnowledge.Count == 2);

            var answerExpr = agQueryExpr.RenderKnowledge[1] as AGPropertyExpr;
            Assert.NotNull(answerExpr);
            Assert.True(answerExpr.Goal.Rhs.Equals(7));
            Assert.Null(answerExpr.AutoTrace);

            answerExpr.IsSelected = true;
            answerExpr.GenerateSolvingTrace();
            Assert.NotNull(answerExpr.AutoTrace);

            var answerExpr1 = agQueryExpr.RenderKnowledge[0] as AGPropertyExpr;
            Assert.NotNull(answerExpr1);
            Assert.True(answerExpr1.Goal.Rhs.Equals(1));
            Assert.Null(answerExpr1.AutoTrace);
            answerExpr1.IsSelected = true;
            answerExpr1.GenerateSolvingTrace();
            Assert.NotNull(answerExpr1.AutoTrace);

            //Reasoner.Instance.Reset();
            var graph = new BehaviorGraph();
            graph.Insert(answerExpr.AutoTrace);

            var answerNode = graph.SearchInnerLoopNode(answerExpr.Goal);
            Assert.NotNull(answerNode);
            graph.SolvingCache.Add(answerNode, false);

            // Assert.True(graph.Nodes.Count == 3);

            graph.Update(answerExpr1.AutoTrace);
            var answerNode1 = graph.SearchInnerLoopNode(answerExpr1.Goal);
            Assert.NotNull(answerNode1);
            graph.SolvingCache.Add(answerNode1, false);

            return graph;
        }
    }
}
