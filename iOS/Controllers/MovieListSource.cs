using System;
namespace Lab1.iOS
{
    using System.Collections.Generic;
    using Foundation;
    using UIKit;
    using Lab1.iOS.Views;
    using Models;

    public class MovieListSource : UITableViewSource 
	{
		public readonly NSString MovieListCellId = new NSString("MovieListCell");
        private List<MovieInfo> _movieList;
		private Action<int> _onSelectedPerson;

		public MovieListSource(List <MovieInfo> movieList, Action<int> onSelectedPerson)
		{
			this._movieList = movieList;
			this._onSelectedPerson = onSelectedPerson;  
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{ 
			var cell = (CustomCell)tableView.DequeueReusableCell(MovieListCellId);
			if (cell == null)
			{
				cell = new CustomCell(MovieListCellId);
			}            

			int row = indexPath.Row;
			cell.UpdateCell(_movieList[row].Title, _movieList[row].Year, _movieList[row].Cast, _movieList[row].PosterPath);
			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return _movieList.Count;
		}

		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			tableView.DeselectRow(indexPath, true);
			this._onSelectedPerson(indexPath.Row);
		}

	}
}
