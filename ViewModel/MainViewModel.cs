using ClassLibrary;
using System;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;
using System.Windows.Input;

namespace ViewModel
{
    public interface IErrorReporter
    {
        void ReportError(string message);
    }
    public class MainViewModel : ViewModelBase
    {
        public Chart chart { get; set; }
        public ChartData chartData { get; set; }
        public SplinesData sData { get; set; }
        
        private readonly IErrorReporter errorReporter;

        private void handler_button_calc_Click(object arg)
        {
            try
            {
                sData.md.calc_grid();
                RaisePropertyChanged(nameof(sData));
            }
            catch (Exception ex)
            {
                errorReporter.ReportError(ex.Message);
            }
        }
        private void handler_button_draw_Click(object arg)
        {
            try
            {
                sData.calculate_splines();
                DrawChart();
                RaisePropertyChanged(nameof(chartData));
            }
            catch (Exception ex)
            {
                errorReporter.ReportError(ex.Message);
            }
        }
        public MainViewModel(IErrorReporter errorReporter)
        {
            this.errorReporter = errorReporter;
            MeasuredData md = new MeasuredData();
            SplineParameters sp = new SplineParameters();
            sData = new SplinesData(new MeasuredData(), new SplineParameters());

            chart = new Chart();
            chartData = new ChartData(sData);

            button_calc_Click = new RelayCommand(handler_button_calc_Click);

            button_draw_Click = new RelayCommand(handler_button_draw_Click);
        }
        public void DrawChart()
        {
            chartData.DrawChart(chart);
        }
        public ICommand button_calc_Click { get; private set; }
        public ICommand button_draw_Click { get; private set; }

        public WindowsFormsHost MyWindowsFormsHost
        {
            get { return new WindowsFormsHost() { Child = chart }; }
        }
    }
}
