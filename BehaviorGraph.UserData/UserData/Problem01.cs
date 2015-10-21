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

    /*
     * Problem 1: Find the distance betweeen A(2,0) and B(5,4)?
     */

    public static partial class ProblemAuthoringModel
    {
        public static BehaviorGraph Problem1()
        {
            // general solving ability
            const string input1 = "A(2,0)";
            const string input2 = "B(5,4)";
            const string query = "d=";

            Reasoner.Instance.Load(input1);
            Reasoner.Instance.Load(input2);

            var obj = Reasoner.Instance.Load(query);
            Assert.NotNull(obj);
            var agQueryExpr = obj as AGQueryExpr;
            Assert.NotNull(agQueryExpr);
            Assert.True(agQueryExpr.RenderKnowledge == null);
            agQueryExpr.RetrieveRenderKnowledge();
            Assert.True(agQueryExpr.RenderKnowledge != null);
            Assert.True(agQueryExpr.RenderKnowledge.Count == 1);

            var answerExpr = agQueryExpr.RenderKnowledge[0] as AGPropertyExpr;
            Assert.NotNull(answerExpr);
            Assert.True(answerExpr.Goal.Rhs.Equals(5.0));
            Assert.Null(answerExpr.AutoTrace);

            answerExpr.IsSelected = true;
            answerExpr.GenerateSolvingTrace();
            Assert.NotNull(answerExpr.AutoTrace);
            int count = answerExpr.RetrieveStepsNumbers();
            var graph = new BehaviorGraph();
            graph.Insert(answerExpr.AutoTrace);

            var answerNode = graph.SearchInnerLoopNode(answerExpr);
            Assert.NotNull(answerNode);
            graph.SolvingCache.Add(answerNode, false);

            return graph;
        }
    }
}
