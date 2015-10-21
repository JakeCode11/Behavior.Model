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
         * Line A contains the point (2,3) and is perpendicular to the line B x-2y+1=0, what is the slope (use notation m) of line A? what is the general form of line A?
         */
        public static BehaviorGraph Problem56()
        {
            const string input1 = "(2,3)";
            const string input2 = "x-2y+1=0";
            const string constraint0 = "m1=";
            const string input3 = "m1*m=-1";
            const string query1 = "lineG";
            const string query2 = "m=";

            Reasoner.Instance.Load(input1);
            Reasoner.Instance.Load(input2);
            Reasoner.Instance.Load(constraint0);
            Reasoner.Instance.Load(input3);

            /////////////////////////////////////////////////////////////////
            var queryExpr1 = Reasoner.Instance.Load(query2) as AGQueryExpr;
            Assert.NotNull(queryExpr1);
            Assert.True(queryExpr1.RenderKnowledge == null);
            queryExpr1.RetrieveRenderKnowledge();
            Assert.True(queryExpr1.RenderKnowledge != null);
            Assert.True(queryExpr1.RenderKnowledge.Count == 2);

            var answerExpr1 = queryExpr1.RenderKnowledge[1] as AGPropertyExpr;
            Assert.NotNull(answerExpr1);
            Assert.True(answerExpr1.Goal.Rhs.Equals(-2));
            Assert.Null(answerExpr1.AutoTrace);
            answerExpr1.IsSelected = true;
            answerExpr1.GenerateSolvingTrace();
            Assert.NotNull(answerExpr1.AutoTrace);
            //////////////////////////////////////////////////////////////////

            var queryExpr2 = Reasoner.Instance.Load(query1) as AGQueryExpr;
            Assert.NotNull(queryExpr2);
            Assert.True(queryExpr2.RenderKnowledge == null);
            queryExpr2.RetrieveRenderKnowledge();
            Assert.True(queryExpr2.RenderKnowledge != null);
            Assert.True(queryExpr2.RenderKnowledge.Count == 2);

            var answerExpr2 = queryExpr2.RenderKnowledge[1] as AGShapeExpr;
            Assert.NotNull(answerExpr2);
            Assert.True(answerExpr2.ShapeSymbol.ToString().Equals("2x+y-7=0"));
            Assert.Null(answerExpr2.AutoTrace);
            answerExpr2.IsSelected = true;
            answerExpr2.GenerateSolvingTrace();
            Assert.NotNull(answerExpr2.AutoTrace);

            Assert.True(answerExpr1.AutoTrace.Count == 5);
            Assert.True(answerExpr2.AutoTrace.Count == 6);

            var graph = new BehaviorGraph();
            graph.Insert(answerExpr1.AutoTrace);
            var answerNode = graph.SearchInnerLoopNode(answerExpr1.Goal);
            Assert.NotNull(answerNode);
            graph.SolvingCache.Add(answerNode, false);
            graph.Update(answerExpr2.AutoTrace);
            var answerNode1 = graph.SearchInnerLoopNode(answerExpr2.ShapeSymbol);
            Assert.NotNull(answerNode1);
            graph.SolvingCache.Add(answerNode1, false);
            return graph;
        }
    }
}