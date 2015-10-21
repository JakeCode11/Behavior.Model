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

using System;
using System.Linq;

namespace MathCogUserData
{
    using MathCog;
    using NUnit.Framework;
    using UserModeling;

    public static partial class ProblemAuthoringModel
    {
        public static BehaviorGraph Problem29()
        {
            const string input1 = "(4,3)";
            const string input2 = "(2,v)";
            const string input4 = "m1=0.5";
            const string input3 = "m*m1=-1";
            const string query = "v";

            Reasoner.Instance.Load(input1);
            Reasoner.Instance.Load(input2);
            Reasoner.Instance.Load(input4);
            Reasoner.Instance.Load(input3);

            var queryExpr2 = Reasoner.Instance.Load(query) as AGQueryExpr;
            Assert.NotNull(queryExpr2);
            Assert.True(queryExpr2.RenderKnowledge == null);
            queryExpr2.RetrieveRenderKnowledge();
            Assert.True(queryExpr2.RenderKnowledge != null);
            Assert.True(queryExpr2.RenderKnowledge.Count == 2);

            var answerExpr2 = queryExpr2.RenderKnowledge[1] as AGPropertyExpr;
            Assert.NotNull(answerExpr2);
            Assert.True(answerExpr2.Goal.ToString().Equals("v=7"));
            Assert.Null(answerExpr2.AutoTrace);
            answerExpr2.IsSelected = true;
            answerExpr2.GenerateSolvingTrace();
            Assert.NotNull(answerExpr2.AutoTrace);

            //////////////////////////////////////////////////////////

            //Reasoner.Instance.Reset();
            var graph = new BehaviorGraph();
            graph.Insert(answerExpr2.AutoTrace);

            var answerNode = graph.SearchInnerLoopNode(answerExpr2.Goal);
            Assert.NotNull(answerNode);
            graph.SolvingCache.Add(answerNode, false);

            return graph;
        }
    }
}
