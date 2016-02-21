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

using MathCog;

namespace MathCogUserData
{
    using NUnit.Framework;

    [TestFixture]
    public partial class ProblemAuthoringModelTest
    {
        /*
         * Problem 1: Find the distance betweeen A(2,0) and B(5,4)?
         */
        [Test]
        public void Test_Problem_01()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(1);
            Assert.True(graph.Nodes.Count == 4);
            Assert.True(graph.SolvingCache.Count == 1);
            Reasoner.Instance.Reset();
        }

        /*
        * There exists two points A(2,4) and B(5,v), the distance between A and B is 5. 
        *  What is the value of v?
        */
        [Test]
        public void Test_Problem_02()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(2);
            Assert.True(graph.SolvingCache.Count == 2);

            Reasoner.Instance.Reset();
        }

        /*
         * There is a line, the slope of it is 3, the y-intercept of it is 2. 
         * What is the slope intercept form of this line? 
         * What is the general form of this line?
         */
        [Test]
        public void Test_Problem_04()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(4);
            Assert.True(graph.SolvingCache.Count == 2);

            Reasoner.Instance.Reset();
        }

        /*
             * Given an equation 2y+2x-y+2x+4=0, graph this equation's corresponding shape?
             * What is the slope of this line? 
             */
        [Test]
        public void Test_Problem_05()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(5);
            //Assert.True(graph.Nodes.Count == 7);
            Assert.True(graph.SolvingCache.Count == 2);
            Reasoner.Instance.Reset();
        }

        /*
         * Problem 6: A line passes through points (2,3) and (4,y), the slope of this line is 5. What is the y-intercept of the line?
         */
        [Test]
        public void Test_Problem_06()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(6);
            Assert.True(graph.SolvingCache.Count == 2);
            Reasoner.Instance.Reset();
        }

        /*
        * Problem 10: A line contains the point (0,5) and is perpendicular to the line 4y=x, what is the slope of this line? what is the general form of this line?
        */
        [Test]
        public void Test_Problem_10()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(10);
            Assert.True(graph.SolvingCache.Count == 2);
            Reasoner.Instance.Reset();
        }

        /*
         * Line A passes through (-1,2) and (5,8). 
         * Line B is parallel to line A, and also cross point (1,0), 
         * what is the general form of line B?
         */
        [Test]
        public void Test_Problem_11()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(11);
            Assert.True(graph.SolvingCache.Count == 1);
            Reasoner.Instance.Reset();
        }

        /*
        * Find the midpoint of the line joining A(-2,2) and B(4,6).
        */
        [Test]
        public void Test_Problem_16()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(16);
            Assert.True(graph.SolvingCache.Count == 1);
            Reasoner.Instance.Reset();
        }

        /*
         * Problem 28: There are two points A(2,y) and B(-1,4). The y-coordinate of point A is -1. What is the distance betweeen these two points?
         */
        [Test]
        public void Test_Problem_28()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(28);
            Assert.True(graph.SolvingCache.Count == 1);
            Reasoner.Instance.Reset();
        }

        /*
        * Problem 29: There are two points A(2,y) and B(-1,4). The y-coordinate of point A is -1. What is the distance betweeen these two points?
        */
        [Test]
        public void Test_Problem_29()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(29);
            Assert.True(graph.SolvingCache.Count == 1);
            Reasoner.Instance.Reset();
        }

        /*
        * Problem 51: Given two points B(3,6) and C(7,3), 
        * what is the distance between these two points? 
        * (Use notation d to represent distance)
        */
        [Test]
        public void Test_Problem_51()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(51);
            Assert.True(graph.Nodes.Count == 4);
            Assert.True(graph.SolvingCache.Count == 1);
            Reasoner.Instance.Reset();
        }

        /*
         * Problem 51: Given two points B(3,6) and C(7,3), 
         * what is the distance between these two points? 
         * (Use notation d to represent distance)
         */
        [Test]
        public void Test_Problem_52()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(52);
            Assert.True(graph.SolvingCache.Count == 2);

            Reasoner.Instance.Reset();
        }

        /*
         * There is a line, the slope of it is 5, the y-intercept of it is 1. What is the slope intercept form of this line? What is the general form of this line? (Use notation m to represent line slope and k to represent y-intercept)
         * 
         */
        [Test]
        public void Test_Problem_53()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(53);
            Assert.True(graph.SolvingCache.Count == 2);

            Reasoner.Instance.Reset();
        }

        /*
         * Given an equation 2+3y+2x-y+2x+5=4, 
         * graph this equation's corresponding shape? 
         * If it is a line, what is the slope of this line? 
         * (Use notation m to represent line slope)
         * 
         */
        [Test]
        public void Test_Problem_54()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(54);
            //Assert.True(graph.Nodes.Count == 7);
            Assert.True(graph.SolvingCache.Count == 2);
            Reasoner.Instance.Reset();
        }

        /*
         * A line passes through points (-1,2) and (1,y), 
         * the slope of this line is 2. What is the value of y? 
         * What is the y-intercept of the line? 
         * (Use notation m to represent line slope and k as y-intercept) 
         */

        [Test]
        public void Test_Problem_55()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(55);
            Assert.True(graph.SolvingCache.Count == 2);
            Reasoner.Instance.Reset();
        }

        /*
         * Line A contains the point (2,3) and is perpendicular to the line B x-2y+1=0, what is the slope (use notation m) of line A? what is the general form of line A?
         */
        [Test]
        public void Test_Problem_56()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(56);
            Assert.True(graph.SolvingCache.Count == 2);
            Reasoner.Instance.Reset();
        }

        [Test]
        public void Test_Problem_57()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(57);
            Assert.True(graph.SolvingCache.Count == 1);
            Reasoner.Instance.Reset();
        }

        [Test]
        public void Test_Problem_58()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(58);
            Assert.True(graph.SolvingCache.Count == 1);
            Reasoner.Instance.Reset();
        }

        /*
         * There are two points A(-3,v) and B(-1,4). The y-coordinate of point A is 2. What is the distance betweeen these two points? (Use notation d to represent distance and round to 1 decimal place.)
         */
        [Test]
        public void Test_Problem_59()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(59);
            Assert.True(graph.SolvingCache.Count == 1);
            Reasoner.Instance.Reset();
        }

        /*
        *Line A passes through two points (v,7) and (4,5). The line is perpendicular to line B in which the slope of line B is 2. What is the value of v? (Use notation m, m1 to represent line slope) 
        * 
        */
        [Test]
        public void Test_Problem_60()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(60);
            Assert.True(graph.SolvingCache.Count == 1);
            Reasoner.Instance.Reset();
        }

        /*
        * Simplify the expression x+2-1?
        *
        */
        [Test]
        public void Test_Problem_96()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(96);
            Assert.True(graph.SolvingCache.Count == 1);
            Reasoner.Instance.Reset();
        }

        /*
         * Simplify the expression x+1+x-4?
         * 
         */
        [Test]
        public void Test_Problem_97()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(97);
            Assert.True(graph.SolvingCache.Count == 1);
            Reasoner.Instance.Reset();
        }

        /*
        * Simplify the expression 1+2-3?
        */
        [Test]
        public void Test_Problem_98()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(98);
            Assert.True(graph.SolvingCache.Count == 1);
            Reasoner.Instance.Reset();
        }

        /*
        * Simplify the expression 1+2+3?
        */
        [Test]
        public void Test_Problem_99()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(99);
            //Assert.True(graph.SolvingCache.Count == 1);
            Reasoner.Instance.Reset();
        }

        [Test]
        public void Test_Problem_61()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(61);
            Assert.True(graph.SolvingCache.Count == 2);
            Reasoner.Instance.Reset();
        }

        [Test]
        public void Test_Problem_62()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(62);
            Assert.True(graph.SolvingCache.Count == 2);
            Reasoner.Instance.Reset();
        }

        [Test]
        public void Test_Problem_63()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(63);
            Assert.True(graph.SolvingCache.Count == 2);
            Reasoner.Instance.Reset();
        }

        [Test]
        public void Test_Problem_64()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(64);
            Assert.True(graph.SolvingCache.Count == 2);
            Reasoner.Instance.Reset();
        }

        [Test]
        public void Test_Problem_65()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(65);
            Assert.True(graph.SolvingCache.Count == 2);
            Reasoner.Instance.Reset();
        }

        [Test]
        public void Test_Problem_66()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(66);
            Assert.True(graph.SolvingCache.Count == 2);
            Reasoner.Instance.Reset();
        }

        [Test]
        public void Test_Problem_67()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(67);
            Assert.True(graph.SolvingCache.Count == 2);
            Reasoner.Instance.Reset();
        }

        [Test]
        public void Test_Problem_68()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(68);
            Assert.True(graph.SolvingCache.Count == 2);
            Reasoner.Instance.Reset();
        }
    }
}
