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
         * Problem 1: Find the distance betweeen A(2,0) and B(5,4)?
         */
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

        /*
         * There exists two points A(2,4) and B(5,v), the distance between A and B is 5. 
         *  What is the value of v?
         */

        public static BehaviorGraph Problem2()
        {
            const string input1 = "A(2,4)";
            const string input2 = "B(5,v)";
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
            Assert.True(answerExpr.Goal.Rhs.Equals(8));
            Assert.Null(answerExpr.AutoTrace);

            answerExpr.IsSelected = true;
            answerExpr.GenerateSolvingTrace();
            Assert.NotNull(answerExpr.AutoTrace);

            var answerExpr1 = agQueryExpr.RenderKnowledge[0] as AGPropertyExpr;
            Assert.NotNull(answerExpr1);
            Assert.True(answerExpr1.Goal.Rhs.Equals(0));
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


        /*
         * There is a line, the slope of it is 3, the y-intercept of it is 2. What is the slope intercept form of this line? What is the general form of this line? (Use notation m to represent line slope and k to represent y-intercept)
         * 
         */
        public static BehaviorGraph Problem4()
        {
            const string input1 = "m=3";
            const string input2 = "k=2";
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

        public static BehaviorGraph Problem6()
        {
            const string input1 = "(2,3)";
            const string input2 = "(4,y)";
            const string input3 = "m=5";
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
            Assert.True(answerExpr0.Goal.ToString().Equals("y=13"));
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

        /*
         * Problem 10: A line contains the point (0,5) and is perpendicular to the line 4y=x, what is the slope of this line? what is the general form of this line?
         */
        public static BehaviorGraph Problem10()
        {
            const string input1 = "(0,5)";
            const string input2 = "4y=x";
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
            Assert.True(answerExpr1.Goal.Rhs.Equals(-4));
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
            Assert.True(answerExpr2.ShapeSymbol.ToString().Equals("4x+y-5=0"));
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

        public static BehaviorGraph Problem28()
        {
            const string input1 = "A(2,y)";
            const string input2 = "B(-1,4)";
            const string input3 = "y=-1";
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

        /*
         * Problem 51: Given two points B(3,6) and C(7,3), 
         * what is the distance between these two points? 
         * (Use notation d to represent distance)
         */
        public static BehaviorGraph Problem51()
        {
            // general solving ability
            const string input1 = "B(3,6)";
            const string input2 = "C(7,3)";
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

        /*
         *
         * Given an equation 2+3y+2x-y+2x+5=4, graph this equation's corresponding shape? If it is a line, what is the slope of this line? (Use notation m to represent line slope)
         * 
         */
        public static BehaviorGraph Problem54()
        {
            const string input1 = "2+3y+2x-y+2x+5=4";
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

        /*
         * Line A passes through (2,2) and (0,-1). Line B is parallel to line A, and also crosses point (2,0), what is the general form of line B? (use notation m, m1 to represent line slope)
         * 
         * */

        public static BehaviorGraph Problem57()
        {
            const string input1 = "(2,2)";
            const string input2 = "(0,-1)";
            const string query1 = "m1";
            const string input3 = "m1=m";
            const string query2 = "lineG";
            const string input4 = "(2,0)";

            Reasoner.Instance.Load(input1);
            Reasoner.Instance.Load(input2);
            Reasoner.Instance.Load(query1);
            Reasoner.Instance.Load(input3);
            var queryExpr2 = Reasoner.Instance.Load(query2) as AGQueryExpr;

            Assert.NotNull(queryExpr2);
            Assert.True(queryExpr2.RenderKnowledge == null);
            queryExpr2.RetrieveRenderKnowledge();
            Assert.True(queryExpr2.RenderKnowledge != null);
            Assert.True(queryExpr2.RenderKnowledge.Count == 2);

            Reasoner.Instance.Load(input4);

            /////////////////////////////////////////////////////////

            Assert.NotNull(queryExpr2);
            queryExpr2.RetrieveRenderKnowledge();
            Assert.True(queryExpr2.RenderKnowledge != null);
            Assert.True(queryExpr2.RenderKnowledge.Count == 6);

            var answerExpr2 = queryExpr2.RenderKnowledge[5] as AGShapeExpr;
            Assert.NotNull(answerExpr2);
            Assert.True(answerExpr2.ShapeSymbol.ToString().Equals("1.5x-y-3=0"));
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

        /*
        * Find the midpoint of the line joining C(3,1) and D(-1,3).
        * 
        */
        public static BehaviorGraph Problem58()
        {
            const string input1 = "C(3,1)";
            const string input2 = "D(-1,3)";
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
            Assert.True(answerExpr2.ShapeSymbol.ToString().Equals("(1,2)"));
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

        /*
         *Line A passes through two points (v,7) and (4,5). The line is perpendicular to line B in which the slope of line B is 2. What is the value of v? (Use notation m, m1 to represent line slope) 
         * 
         */
        public static BehaviorGraph Problem60()
        {
            const string input1 = "(v,7)";
            const string input2 = "(4,5)";
            const string input4 = "m1=2";
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
            Assert.True(answerExpr2.Goal.ToString().Equals("v=0"));
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

        public static BehaviorGraph Problem96()
        {
            const string fact1 = "x+2-1=";
            var queryExpr = Reasoner.Instance.Load(fact1) as AGQueryExpr;
            Assert.NotNull(queryExpr);
            queryExpr.RetrieveRenderKnowledge();
            Assert.True(queryExpr.RenderKnowledge.Count == 1);

            var agEquationExpr = queryExpr.RenderKnowledge[0] as AGEquationExpr;
            Assert.True(agEquationExpr != null);
            agEquationExpr.IsSelected = true;

            agEquationExpr.GenerateSolvingTrace();
            Assert.True(agEquationExpr.AutoTrace != null);
            Assert.True(agEquationExpr.AutoTrace.Count == 1);

            var graph = new BehaviorGraph();
            graph.Insert(agEquationExpr.AutoTrace);
            var answerNode1 = graph.SearchInnerLoopNode(agEquationExpr.Equation.Rhs);
            Assert.NotNull(answerNode1);
            graph.SolvingCache.Add(answerNode1, false);
            return graph;
        }

        /*
        * Simplify the expression x+1+x-4?
        * 
        */
        public static BehaviorGraph Problem97()
        {
            const string fact1 = "x+1+x-4=";
            var queryExpr = Reasoner.Instance.Load(fact1) as AGQueryExpr;
            Assert.NotNull(queryExpr);
            queryExpr.RetrieveRenderKnowledge();
            Assert.True(queryExpr.RenderKnowledge.Count == 1);

            var agEquationExpr = queryExpr.RenderKnowledge[0] as AGEquationExpr;
            Assert.True(agEquationExpr != null);
            agEquationExpr.IsSelected = true;

            agEquationExpr.GenerateSolvingTrace();
            Assert.True(agEquationExpr.AutoTrace != null);
            Assert.True(agEquationExpr.AutoTrace.Count == 1);

            var graph = new BehaviorGraph();
            graph.Insert(agEquationExpr.AutoTrace);
            var answerNode1 = graph.SearchInnerLoopNode(agEquationExpr.Equation.Rhs);
            Assert.NotNull(answerNode1);
            graph.SolvingCache.Add(answerNode1, false);
            return graph;

            /*   ////////////////Authoring Below //////////////////////////////

               Debug.Assert(graph.Nodes.Count == 2);

               //add another strategy to solve the same problem
               //so it add onto the outer loop side
               //x+1+x-4 -> 1-4+x+x -> -3+2x -> 2x-3

               var x = new Var('x');
               var innerTerm = new Term(Expression.Multiply, new List<object>() {2, x});
               var rhs = new Term(Expression.Add, new List<object>() { innerTerm, -3 });
               var eq1Lhs = new Term(Expression.Add, new List<object>() {x, 1, x, -4});
               var eq1 = new Equation(eq1Lhs, rhs);
               var eq2Lhs = new Term(Expression.Add, new List<object>() {1, -4, x, x});
               var eq2 = new Equation(eq2Lhs, rhs);
               var eq3Lhs = new Term(Expression.Add, new List<object>() {-3, innerTerm});
               var eq3 = new Equation(eq3Lhs, rhs);
               var eq4 = new Equation(rhs, rhs);

               var ts1 = new TraceStep(eq1, eq2, "TODO", "TODO");
               var ts1Expr = new TraceStepExpr(ts1);

               var ts2 = new TraceStep(eq2, eq3, "TODO", "TODO");
               var ts2Expr = new TraceStepExpr(ts2);

               var ts3 = new TraceStep(eq3, eq4, "TODO", "TODO");
               var ts3Expr = new TraceStepExpr(ts3);

               var lst = new List<TraceStepExpr>() {ts1Expr, ts2Expr, ts3Expr};

               var tuple = new Tuple<object, object>(AlgebraRule.AlgebraicStrategy, lst);
               var lstTuple = new List<Tuple<object, object>>() { tuple };

               graph.Update(lstTuple);*/

        }

        public static BehaviorGraph Problem98()
        {
            const string fact1 = "1+2-3=";
            var queryExpr = Reasoner.Instance.Load(fact1) as AGQueryExpr;
            Assert.NotNull(queryExpr);
            queryExpr.RetrieveRenderKnowledge();
            Assert.True(queryExpr.RenderKnowledge.Count == 1);

            var agEquationExpr = queryExpr.RenderKnowledge[0] as AGEquationExpr;
            Assert.True(agEquationExpr != null);
            agEquationExpr.IsSelected = true;

            agEquationExpr.GenerateSolvingTrace();
            Assert.True(agEquationExpr.AutoTrace != null);
            Assert.True(agEquationExpr.AutoTrace.Count == 1);

            var graph = new BehaviorGraph();
            graph.Insert(agEquationExpr.AutoTrace);
            var answerNode1 = graph.SearchInnerLoopNode(agEquationExpr.Equation.Rhs);
            Assert.NotNull(answerNode1);
            graph.SolvingCache.Add(answerNode1, false);
            return graph;
        }

        public static BehaviorGraph Problem99()
        {
            const string fact1 = "1+2+3=";
            var queryExpr = Reasoner.Instance.Load(fact1) as AGQueryExpr;
            Assert.NotNull(queryExpr);
            queryExpr.RetrieveRenderKnowledge();
            Assert.True(queryExpr.RenderKnowledge.Count == 1);

            var agEquationExpr = queryExpr.RenderKnowledge[0] as AGEquationExpr;
            Assert.True(agEquationExpr != null);
            agEquationExpr.IsSelected = true;

            agEquationExpr.GenerateSolvingTrace();
            Assert.True(agEquationExpr.AutoTrace != null);
            Assert.True(agEquationExpr.AutoTrace.Count == 1);

            var graph = new BehaviorGraph();
            graph.Insert(agEquationExpr.AutoTrace);
            var answerNode1 = graph.SearchInnerLoopNode(agEquationExpr.Equation.Rhs);
            Assert.NotNull(answerNode1);
            graph.SolvingCache.Add(answerNode1, false);
            return graph;


            /*
                        //////////////////////////////////////////////////////////////

                        Reasoner.Instance.Reset();
            
                        //////////////////////////////////////////////////////////////

                        var graph = new BehaviorGraph();
                        graph.Insert(agEquationExpr.AutoTrace);

                        ////////////////Authoring Below //////////////////////////////
            
           
                        Debug.Assert(graph.Nodes.Count == 2);
                        Debug.Assert(graph.Nodes[0].OutEdges.Count == 1);

                        //add another strategy to solve the same problem
                        //so it add onto the outer loop side

                        var term1 = new Term(Expression.Add, new List<object> {1,2,3});
                        var eq1 = new Equation(term1, 6);

                        var term2 = new Term(Expression.Add, new List<object>() {1, 5});
                        var eq2 = new Equation(term2, 6);

                        var metaRule = AlgebraRule.Rule(AlgebraRule.AlgebraRuleType.Associative);
                        var appliedRule = AlgebraRule.Rule(AlgebraRule.AlgebraRuleType.Associative, eq1, eq2);
                        var ts1 = new TraceStep(eq1, eq2, metaRule, appliedRule);
                        var ts1Expr = new TraceStepExpr(ts1);
                        var eq3 = new Equation(6, 6);
                        var ts2 = new TraceStep(eq2, eq3, "TODO", "TODO");
                        var ts2Expr = new TraceStepExpr(ts2);
                        var lst = new List<TraceStepExpr>() {ts1Expr, ts2Expr};
                        var tuple = new Tuple<object, object>(ArithRule.ArithmeticStrategy, lst);
                        var lstTuple = new List<Tuple<object, object>>() {tuple};

                        graph.Update(lstTuple);*/

        }

        //TODO: Add backend reasoning

        public static BehaviorGraph Problem61()
        {
            return Problem99();
        }

        public static BehaviorGraph Problem62()
        {
            return Problem99();
        }

        public static BehaviorGraph Problem63()
        {
            return Problem99();
        }

        public static BehaviorGraph Problem64()
        {
            return Problem99();
        }

        public static BehaviorGraph Problem65()
        {
            return Problem99();
        }

        public static BehaviorGraph Problem66()
        {
            return Problem99();
        }

        public static BehaviorGraph Problem67()
        {
            return Problem99();
        }

        public static BehaviorGraph Problem68()
        {
            return Problem99();
        }
    }
}
