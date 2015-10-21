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

namespace UserModeling
{
    using MathCog;
    using MathCog.UserModeling;
    using NUnit.Framework;

    [TestFixture]
    public partial class TestProblems
    {
        #region Tutoring

        public void Init52_Tutor()
        {
            //Retrieve authoring behavior graph 
            const int problemIndex = 52;
            bool result = HCIReasoner.Instance.InitProblem(problemIndex);
            //Has authoring behavior data
            Assert.True(result);
            //Assert.True(HCIReasoner.Instance.UserGraph.Nodes.Count == 3);
            Assert.Null(HCIReasoner.Instance.ObjectGraph);

            bool tutorMode = true;
            HCIReasoner.Instance.InitMode(tutorMode);

            //Not this one
            Assert.True(HCIReasoner.Instance.RelationGraph.Nodes.Count == 0);
            //Verifier
            Assert.True(Reasoner.Instance.RelationGraph.Nodes.Count == 6);
        }

        [Test]
        public void Test_Problem_52_Tutor_0()
        {
            Init52_Tutor();

            /*
             * d^2=(2+2)^2+(4-v)^2
             * Failed 
             */ 
        }



        #endregion
    }
}
