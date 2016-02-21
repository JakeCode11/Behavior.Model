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
using System.Collections.Generic;
using UserModeling;

namespace MathCogUserData
{
    public class BehaviorUserModelLoader
    {
        private static BehaviorUserModelLoader _instance;

        public static BehaviorUserModelLoader Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BehaviorUserModelLoader();
                }
                return _instance;
            }
        }

        private BehaviorUserModelLoader()
        {
        }

        public BehaviorGraph LoadProblem(int problemIndex)
        {
            //TODO
            switch (problemIndex)
            {
                case 1:
                    return ProblemAuthoringModel.Problem1();
                case 2:
                    return ProblemAuthoringModel.Problem2();
                case 3:
                case 4:
                    return ProblemAuthoringModel.Problem4();
                case 5:
                    return ProblemAuthoringModel.Problem5();
                case 6:
                    return ProblemAuthoringModel.Problem6();
                case 10:
                    return ProblemAuthoringModel.Problem10();
                case 11:
                    return ProblemAuthoringModel.Problem11();
                case 16:
                    return ProblemAuthoringModel.Problem16();
                case 28:
                    return ProblemAuthoringModel.Problem28();
                case 29:
                    return ProblemAuthoringModel.Problem29();
                case 51:
                    return ProblemAuthoringModel.Problem51();
                case 52:
                    return ProblemAuthoringModel.Problem52();
                case 53:
                    return ProblemAuthoringModel.Problem53();
                case 54:
                    return ProblemAuthoringModel.Problem54();
                case 55:
                    return ProblemAuthoringModel.Problem55();
                case 56:
                    return ProblemAuthoringModel.Problem56();
                case 57:
                    return ProblemAuthoringModel.Problem57();
                case 58:
                    return ProblemAuthoringModel.Problem58();
                case 59:
                    return ProblemAuthoringModel.Problem59();
                case 60:
                    return ProblemAuthoringModel.Problem60();
                case 96:
                    return ProblemAuthoringModel.Problem96();
                case 97:
                    return ProblemAuthoringModel.Problem97();
                case 98:
                    return ProblemAuthoringModel.Problem98();
                case 99:
                    return ProblemAuthoringModel.Problem99();

                case 61:
                    return ProblemAuthoringModel.Problem61();
                case 62:
                    return ProblemAuthoringModel.Problem62();
                case 63:
                    return ProblemAuthoringModel.Problem63();
                case 64:
                    return ProblemAuthoringModel.Problem64();
                case 65:
                    return ProblemAuthoringModel.Problem65();
                case 66:
                    return ProblemAuthoringModel.Problem66();
                case 67:
                    return ProblemAuthoringModel.Problem67();
                case 68:
                    return ProblemAuthoringModel.Problem68();

                default:
                    return null;
            }
            return null;
        }
    }

}