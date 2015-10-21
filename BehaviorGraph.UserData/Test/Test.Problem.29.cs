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
         * Problem 28: There are two points A(2,y) and B(-1,4). The y-coordinate of point A is -1. What is the distance betweeen these two points?
         */
        [Test]
        public void Test_Problem_29()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(29);
            Assert.True(graph.SolvingCache.Count == 1);
            Reasoner.Instance.Reset();
        }
    }
}
