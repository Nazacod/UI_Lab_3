using System;
using System.Linq;
using Xunit;
using ClassLibrary;

namespace ClassLibraryTests
{
    public class MeasuredDataTests
    {
        [Fact]
        public void ConstructorMeasuredDataTest()
        {
            // MeasuredData(int cnt_nodes_, double left, double right, SPf function_)
            var md = new MeasuredData(5, 0, 1, SPf.Func);
            Assert.Equal(5, md.cnt_nodes);
            Assert.Equal(0, md.limits[0]);
            Assert.Equal(1, md.limits[1]);
            Assert.Equal(SPf.Func, md.function);

            Assert.Equal($"x[0]={0:F3}, y[0]={0:F3}", md.viewXY[0]);
        }

        [Fact]
        public void ConstructorSplineParametersTest()
        {
            // SplineParameters(int cnt_nodes_, double l1, double r1, double l2, double r2)
            var sp = new SplineParameters(100, 1.0, 2.0, -10.0, 20.0);
            Assert.Equal(100, sp.cnt_nodes);
            Assert.Equal(1.0, sp.derivatives_spline1[0]);
            Assert.Equal(2.0, sp.derivatives_spline1[1]);
            Assert.Equal(-10.0, sp.derivatives_spline2[0]);
            Assert.Equal(20.0, sp.derivatives_spline2[1]);
        }

        [Fact]
        public void ConstructorSplinesDataTest()
        {
            // SplinesData(MeasuredData md_, SplineParameters sp_)
            var md = new MeasuredData(5, 0, 1, SPf.Func);
            var sp = new SplineParameters(100, 1.0, 2.0, -10.0, 20.0);
            var sd = new SplinesData(md, sp);

            //Test sd.md
            Assert.Equal(5, sd.md.cnt_nodes);
            Assert.Equal(0, sd.md.limits[0]);
            Assert.Equal(1, sd.md.limits[1]);
            Assert.Equal(SPf.Func, sd.md.function);

            Assert.Equal($"x[0]={0:F3}, y[0]={0:F3}", sd.md.viewXY[0]);

            //Test sd.sp
            Assert.Equal(100, sd.sp.cnt_nodes);
            Assert.Equal(1.0, sd.sp.derivatives_spline1[0]);
            Assert.Equal(2.0, sd.sp.derivatives_spline1[1]);
            Assert.Equal(-10.0, sd.sp.derivatives_spline2[0]);
            Assert.Equal(20.0, sd.sp.derivatives_spline2[1]);
        }

        [Fact]
        public void SplinesData_calculate_splines_Test()
        {
            //Test linear spline
            var md = new MeasuredData(10000, 0, 1, SPf.linear);
            var sp = new SplineParameters(10000, 1.0, 1.0, 1.0, 1.0);
            var sd = new SplinesData(md, sp);

            //Spline's derivatives equal 1 for y=x
            Assert.Equal(1, sd.derivatives_spline1[1], 3);
            Assert.Equal(1, sd.derivatives_spline1[3], 3);

            Assert.Equal(1, sd.derivatives_spline2[1], 3);
            Assert.Equal(1, sd.derivatives_spline2[3], 3);

            //Test values y for y=x on [0, 1] with step=1/10_000
            Assert.Equal(0, sd.values_spline1[0]);
            Assert.Equal(0.5, sd.values_spline1[5000]);

            Assert.Equal(0, sd.values_spline2[0]);
            Assert.Equal(0.5, sd.values_spline2[5000]);
        }
    }
}
