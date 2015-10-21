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
        public static BehaviorGraph Problem16()
        {
            const string input1 = "A(-1,2)";
            const string input2 = "B(5,8)";
            const string query0 = "MidP=";
            Reasoner.Instance.Load(input1);
            Reasoner.Instance.Load(input2);
            var agQueryExpr = Reasoner.Instance.Load(query0) as AGQueryExpr;
            Assert.NotNull(agQueryExpr);
            agQueryExpr.RetrieveRenderKnowledge();
            Assert.True(agQueryExpr.RenderKnowledge != null);
            Assert.True(agQueryExpr.RenderKnowledge.Count == 1);

            var answerExpr2 = agQueryExpr.RenderKnowledge[0] as AGShapeExpr;
            Assert.NotNull(answerExpr2);
            Assert.True(answerExpr2.ShapeSymbol.ToString().Equals("(2,5)"));
            Assert.Null(answerExpr2.AutoTrace);
            answerExpr2.IsSelected = true;
            answerExpr2.GenerateSolvingTrace();
            Assert.NotNull(answerExpr2.AutoTrace);

            //////////////////////////////////////////////////////////

            //Reasoner.Instance.Reset();
            var graph = new BehaviorGraph();
            graph.Insert(answerExpr2.AutoTrace);

            var answerNode = graph.SearchInnerLoopNode(answerExpr2.ShapeSymbol);
            Assert.NotNull(answerNode);
            graph.SolvingCache.Add(answerNode, false);

            return graph;
        }
    }
}