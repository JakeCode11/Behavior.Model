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
         * A line passes through points (-1,2) and (1,y), 
         * the slope of this line is 2. What is the value of y? 
         * What is the y-intercept of the line? 
         * (Use notation m to represent line slope and k as y-intercept) 
         */
        public static BehaviorGraph Problem55()
        {
            const string input1 = "(-1,2)";
            const string input2 = "(1,y)";
            const string input3 = "m=2";
            const string query0 = "y";

            const string query = "k";

            Reasoner.Instance.Load(input1);
            Reasoner.Instance.Load(input2);
            Reasoner.Instance.Load(input3);

            var agQueryExpr0 = Reasoner.Instance.Load(query0) as AGQueryExpr;
            Assert.NotNull(agQueryExpr0);
            Assert.True(agQueryExpr0.RenderKnowledge == null);
            agQueryExpr0.RetrieveRenderKnowledge();
            Assert.True(agQueryExpr0.RenderKnowledge != null);
            Assert.True(agQueryExpr0.RenderKnowledge.Count == 3);

            var answerExpr0 = agQueryExpr0.RenderKnowledge[2] as AGPropertyExpr;
            Assert.NotNull(answerExpr0);
            Assert.True(answerExpr0.Goal.ToString().Equals("y=6"));
            Assert.Null(answerExpr0.AutoTrace);
            answerExpr0.IsSelected = true;
            answerExpr0.GenerateSolvingTrace();
            Assert.NotNull(answerExpr0.AutoTrace);


            var agQueryExpr = Reasoner.Instance.Load(query) as AGQueryExpr;
            Assert.NotNull(agQueryExpr);
            Assert.True(agQueryExpr.RenderKnowledge == null);
            agQueryExpr.RetrieveRenderKnowledge();
            Assert.True(agQueryExpr.RenderKnowledge != null);
            Assert.True(agQueryExpr.RenderKnowledge.Count == 1);

            var answerExpr1 = agQueryExpr.RenderKnowledge[0] as AGPropertyExpr;
            Assert.NotNull(answerExpr1);
            Assert.Null(answerExpr1.AutoTrace);
            answerExpr1.IsSelected = true;
            answerExpr1.GenerateSolvingTrace();
            Assert.NotNull(answerExpr1.AutoTrace);

            var graph = new BehaviorGraph();

            graph.Insert(answerExpr0.AutoTrace);
            var answerNode0 = graph.SearchInnerLoopNode(answerExpr0);
            Assert.NotNull(answerNode0);
            graph.SolvingCache.Add(answerNode0, false);

            graph.Update(answerExpr1.AutoTrace);
            var answerNode1 = graph.SearchInnerLoopNode(answerExpr1);
            Assert.NotNull(answerNode1);
            graph.SolvingCache.Add(answerNode1, false);

            return graph;
        }
    }
}