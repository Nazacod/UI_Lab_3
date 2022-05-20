using System;
using System.Linq;
using ViewModel;
using Xunit;

namespace ViewModelTests
{
    public class MainViewModelTests
    {
        [Fact]
        public void ErrorScenario_calc()
        {
            var t = new TestErrorReporter();
            var mvm = new MainViewModel(t);
            //cnt_nodes should be positive
            mvm.sData.md.cnt_nodes = -1;
            mvm.button_calc_Click.Execute(null);

            Assert.True(t.WasError);
        }

        [Fact]
        public void ErrorScenario_draw()
        {
            var t = new TestErrorReporter();
            var mvm = new MainViewModel(t);
            //derivatives_spline1 should be array of doubles
            mvm.sData.sp.derivatives_spline1 = null;
            mvm.button_draw_Click.Execute(null);

            Assert.True(t.WasError);
        }
        public class TestErrorReporter : IErrorReporter
        {
            public bool WasError { get; private set; }
            public void ReportError(string message) => WasError = true;
        }
    }
}
