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
         * There are two points A(-3,v) and B(-1,4). 
         * The y-coordinate of point A is 2. 
         * What is the distance betweeen these two points? 
         * (Use notation d to represent distance and round to 1 decimal place.)
         * 
         */

        public static BehaviorGraph Problem59()
        {
            const string input1 = "A(-3,v)";
            const string input2 = "B(-1,4)";
            const string input3 = "v=2";
            const string query = "d=";

            Reasoner.Instance.Load(input1);
            Reasoner.Instance.Load(input2);
            Reasoner.Instance.Load(input3);
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
            graph.Insert(answerExpr1.AutoTrace);
            var answerNode1 = graph.SearchInnerLoopNode(answerExpr1);
            Assert.NotNull(answerNode1);
            graph.SolvingCache.Add(answerNode1, false);
            return graph;
        }
    }
}