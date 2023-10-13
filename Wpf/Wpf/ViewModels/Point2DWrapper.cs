using ReactiveUI;
using System.Reactive;
using Wpf.Models;

namespace Wpf.ViewModels;

public class Point2DWrapper : ReactiveObject
{
    private Point2D _point;

	public double X
	{
		get => _point.X;
		set
		{
			if ((_point.X - value) > 1E-14) return;

			_point.X = value;
			this.RaisePropertyChanged();
		}
	}
    public double Y
    {
        get => _point.Y;
        set
        {
            if ((_point.Y - value) > 1E-14) return;

            _point.Y = value;
            this.RaisePropertyChanged();
        }
    }

	public ReactiveCommand<Unit, Unit>? AddPoint { get; set; }
	public ReactiveCommand<Point2DWrapper, Unit>? DeletePoint { get; set; }
    public Point2D WrappedObject => _point;

    public Point2DWrapper(Point2D point)
    {
        _point = point;
    }
}