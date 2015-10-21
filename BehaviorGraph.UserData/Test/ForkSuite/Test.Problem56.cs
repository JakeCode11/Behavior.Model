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
         * Line A contains the point (2,3) and is perpendicular to the line B x-2y+1=0, what is the slope (use notation m) of line A? what is the general form of line A?
         */
        [Test]
        public void Test_Problem_56()
        {
            var graph = BehaviorUserModelLoader.Instance.LoadProblem(56);
            Assert.True(graph.SolvingCache.Count == 2);
            Reasoner.Instance.Reset();
        }
    }
}
