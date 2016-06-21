using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using starPadSDK.MathExpr;

namespace MathCog.Data.Test
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            string _path = @"C:\\1-Production\\MathApollo\\Math-Visual2\\LogFiles\\Data";
            string fileName = "Log_UniModal_01_404_2016621114711.bin";
            string path = System.IO.Path.Combine("@", _path, fileName);

            //x^2+y^2=5^2
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(path,
                                      FileMode.Open,
                                      FileAccess.Read,
                                      FileShare.Read);
            var lst = formatter.Deserialize(stream) as List<TimeSeriesAlgebraData>;
            stream.Close();
            Assert.NotNull(lst);
            Assert.True(lst.Count == 3);
        }
    }
}
