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
        public static BehaviorGraph Problem5()
        {
            const string input1 = "2y+2x-y+2x+4=0";
            const string query1 = "graph=";
            Reasoner.Instance.Load(input1);
            var obj = Reasoner.Instance.Load(query1);
            var obj1 = Reasoner.Instance.Load(query1);

            var queryExpr1 = obj1 as AGQueryExpr;
            Assert.NotNull(queryExpr1);
            //Hidden Selected Render Knowledge 
            queryExpr1.RetrieveRenderKnowledge();
            Assert.True(queryExpr1.RenderKnowledge.Count == 1);

            var answerExpr1 = queryExpr1.RenderKnowledge[0] as AGShapeExpr;
            Assert.NotNull(answerExpr1);
            Assert.Null(answerExpr1.AutoTrace);
            answerExpr1.IsSelected = true;
            answerExpr1.GenerateSolvingTrace();
            Assert.NotNull(answerExpr1.AutoTrace);

            const string input2 = "m=";
            var obj2 = Reasoner.Instance.Load(input2);
            Assert.NotNull(obj2);
            var agQueryExpr2 = obj2 as AGQueryExpr;
            Assert.NotNull(agQueryExpr2);
            agQueryExpr2.RetrieveRenderKnowledge();
            Assert.True(agQueryExpr2.RenderKnowledge.Count == 1);
            var answerExpr2 = agQueryExpr2.RenderKnowledge[0] as AGPropertyExpr;
            Assert.True(answerExpr2 != null);
            //Query answer
            answerExpr2.IsSelected = true;
            answerExpr2.GenerateSolvingTrace();
            Assert.True(answerExpr2.AutoTrace != null);

            //Reasoner.Instance.Reset();
            var graph = new BehaviorGraph();
            graph.Insert(answerExpr1.AutoTrace);

            var answerNode1 = graph.SearchInnerLoopNode(answerExpr1);
            Assert.NotNull(answerNode1);
            graph.SolvingCache.Add(answerNode1, false);

            graph.Update(answerExpr2.AutoTrace);
            var answerNode2 = graph.SearchInnerLoopNode(answerExpr2);
            Assert.NotNull(answerNode2);
            graph.SolvingCache.Add(answerNode2, false);


            return graph;
        }
    }
}
