using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathLogger
{
    public class DataLoader
    {
        //Load both Geometry XML file and Expr Data file

        public void Test()
        {
/*            var drawing = GeometryGrid.CurrentDrawing;
            var figures = drawing.GetSpecifiedFigures();

            if (figures == null) return;

            var editor = AlgebraGrid.AlgebraicInkCanvas.AGEditor as AGAlgebraEditor;
            if (editor == null) throw new Exception("Initilization failed for editor");

            var objs = new List<object>();

            int figureIndex = 0;
            int exprIndex = 0;
            var ranges = editor.MathRecognizer.Ranges;

            while (true)
            {
                if (figureIndex < figures.Count() && exprIndex < ranges.Count)
                {
                    var currFigure = figures[figureIndex];
                    var currRange = ranges[exprIndex];

                    int result = DateTime.Compare(currFigure.InputTime, currRange.Timer);

                    if (result < 0)
                    {
                        objs.Add(currFigure.ShapeSymbol);
                        figureIndex++;
                    }
                    else
                    {
                        if (!currRange.Parse.parseError)
                        {
                            objs.Add(currRange.Parse.expr);
                        }
                        exprIndex++;
                    }
                }
                else if (figureIndex < figures.Count())
                {
                    var currFigure = figures[figureIndex];
                    objs.Add(currFigure.ShapeSymbol);
                    figureIndex++;
                }
                else if (exprIndex < ranges.Count)
                {
                    var currRange = ranges[exprIndex];
                    if (!currRange.Parse.parseError)
                    {
                        objs.Add(currRange.Parse.expr);
                    }
                    exprIndex++;
                }
                else
                {
                    break;
                }
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, objs);
            stream.Close();*/
        }

    }
}
