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
    }
}
