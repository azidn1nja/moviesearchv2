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

		public MovieListSource(List <MovieInfo> movieList)
		{
			this._movieList = movieList;
		}

		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{ 
			var cell = (CustomCell)tableView.DequeueReusableCell(MovieListCellId);
			if (cell == null)
			{
				cell = new CustomCell(MovieListCellId);
			}            

			int row = indexPath.Row;
            cell.UpdateCell(_movieList[row].Title, _movieList[row].Year, _movieList[row].Cast);
			return cell;
		}

		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return _movieList.Count;
		}

	}
}
