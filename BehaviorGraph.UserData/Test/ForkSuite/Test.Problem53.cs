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
    }
}
