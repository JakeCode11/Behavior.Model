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

namespace Loader
{
    using System.Collections.Generic;

    public class MathProblem
    {
        public int Index { get; set; }
        public string OriginalProblem { get; set; }
        public ProblemType Type { get; set; }
        public List<ProblemWord> Words { get; set; } //Segmentation

        public MathProblem(int index, string originProblem, ProblemType pt)
        {
            Index = index;
            OriginalProblem = originProblem;
            Type = pt;
            Words = new List<ProblemWord>();
        }

        public enum ProblemType
        {
            Arithmetic, 
            Algebra, 
            Geometry,
            AlgebraGeoemtry,
            AlgebraWord
        }
    }

    public class ProblemWord
    {
        public int Id { get; set; } //json: id
        public string Word { get; set; } //json: Tag
        public InteractionType InteractType { get; set; }

        //InteractionType.Input  : string
        //InteractionType.Concept: Tuple<string, string>
        public ProblemWord(int id, string word, InteractionType interactType, object dragObject)
        {
            Id = id;
            Word = word;
            InteractType = interactType;
            DragObject = dragObject;
        }

        public object DragObject { get; set; }

        public ProblemWord(int index, string tag, InteractionType it)
        {
            Id = index;
            Word = tag;
            InteractType = it;
        }

        public enum InteractionType
        {
            Concept,
            Input,
            Hybrid,
            None
        }
    }
}
