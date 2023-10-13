using DynamicData;
using Newtonsoft.Json;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Wpf.Models;

namespace Wpf.ViewModels;

public class MainViewModel : ReactiveObject
{
	private readonly ReadOnlyObservableCollection<Point2DWrapper> _points;
	public ReadOnlyObservableCollection<Point2DWrapper> Points => _points;
	public SourceCache<Point2DWrapper, Point2DWrapper> SourceCachePoints { get; private set; }
	public ReactiveCommand<Unit, Unit> AddPoint { get; }
	public ReactiveCommand<Point2DWrapper, Unit> DeletePoint { get; }
	public Interaction<Unit, Point2D[]> OpenFileDialog { get; } = new();
	public Interaction<Unit, string> SaveFileDialog { get; } = new();
    public ReactiveCommand<Unit, Unit> OpenFile { get; }
    public ReactiveCommand<Unit, Unit> SaveFile { get; }

    public MainViewModel()
	{
        SourceCachePoints = new SourceCache<Point2DWrapper, Point2DWrapper>(p => p);
		SourceCachePoints.Connect().Bind(out _points).Subscribe();

		AddPoint = ReactiveCommand.Create(() =>
		{
			SourceCachePoints.AddOrUpdate(new Point2DWrapper(new Models.Point2D())
			{
				AddPoint = AddPoint,
				DeletePoint = DeletePoint
			});
		});

		DeletePoint = ReactiveCommand.Create<Point2DWrapper>(p =>
		{
			SourceCachePoints.RemoveKey(p);
		}, SourceCachePoints.CountChanged.Select(c => c > 1));

        SourceCachePoints.AddOrUpdate(new Point2DWrapper(new Point2D())
		{
			AddPoint = AddPoint,
			DeletePoint = DeletePoint
		});

		OpenFile = ReactiveCommand.CreateFromTask(OpenFileImpl);
		SaveFile = ReactiveCommand.CreateFromTask(SaveFileImpl);
    }

	public async Task OpenFileImpl()
    {
		var points = await OpenFileDialog.Handle(Unit.Default);
		SourceCachePoints.Clear();

		foreach (var p in points)
		{
			SourceCachePoints.AddOrUpdate(new Point2DWrapper(p)
			{
				AddPoint = AddPoint,
				DeletePoint = DeletePoint
			});
		}
    }

    public async Task SaveFileImpl()
    {
		var filename = await SaveFileDialog.Handle(Unit.Default);
		File.WriteAllText(filename, JsonConvert.SerializeObject(_points.Select(p => p.WrappedObject)));
	}
}