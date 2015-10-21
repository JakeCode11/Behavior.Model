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
    using System;
    using MathCog;
    using NUnit.Framework;
    using UserModeling;

    public static partial class ProblemAuthoringModel
    {
        /*
         * Line A passes through (-1,2) and (5,8). 
         * Line B is parallel to line A, and also cross point (1,0), 
         * what is the general form of line B?
         */
        public static BehaviorGraph Problem11()
        {
            const string input1 = "(-1,2)";
            const string input2 = "(5,8)";
            const string query1 = "m1";
            const string input3 = "m1=m";
            const string query2 = "lineG";
            const string input4 = "(1,0)";

            Reasoner.Instance.Load(input1);
            Reasoner.Instance.Load(input2);
            Reasoner.Instance.Load(query1);
            Reasoner.Instance.Load(input3);
            var queryExpr2 = Reasoner.Instance.Load(query2) as AGQueryExpr;

            Assert.NotNull(queryExpr2);
            Assert.True(queryExpr2.RenderKnowledge == null);
            queryExpr2.RetrieveRenderKnowledge();
            Assert.True(queryExpr2.RenderKnowledge != null);
            Assert.True(queryExpr2.RenderKnowledge.Count == 1);

            Reasoner.Instance.Load(input4);

            /////////////////////////////////////////////////////////

            Assert.NotNull(queryExpr2);
            queryExpr2.RetrieveRenderKnowledge();
            Assert.True(queryExpr2.RenderKnowledge != null);
            Assert.True(queryExpr2.RenderKnowledge.Count == 5);

            var answerExpr2 = queryExpr2.RenderKnowledge[4] as AGShapeExpr;
            Assert.NotNull(answerExpr2);
            Assert.True(answerExpr2.ShapeSymbol.ToString().Equals("x-y-1=0"));
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