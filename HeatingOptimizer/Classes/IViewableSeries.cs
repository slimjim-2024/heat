using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HeatingOptimizer.Optimizer;
using HeatingOptimizer.ViewModels;
using LiveChartsCore;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace HeatingOptimizer
{

    public interface IViewableSeries 
    {
        protected internal ObservableCollection<ISeries> Series {get; set;} 
        protected internal string Name {get; set;}
        public ObservableCollection<ICartesianAxis> XAxes {get; set;}
        protected internal ObservableCollection<ICartesianAxis> YAxes { get; set; }
        void GenerateGraph(List<ProductionUnit> selectedProductionUnits, List<TimeFrame> timeFrames,
            in Dictionary<string, List<Result>> results);
    }
}
