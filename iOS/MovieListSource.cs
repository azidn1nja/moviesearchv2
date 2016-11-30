using System;
namespace Lab1.iOS
{
	using System.Collections.Generic;
	using Foundation;
	using UIKit;
	public class MovieListSource : UITableViewSource 
	{
		public readonly NSString NameListCellId = new NSString("NameListCell");

        private List<string> _movieList;
		public MovieListSource(List <string> movieList)
		{
			this._movieList = movieList;
		}
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{ 
			var cell = tableView.DequeueReusableCell(this.NameListCellId);
			if (cell == null)
			{
				cell = new UITableViewCell(UITableViewCellStyle.Default, (NSString)this.NameListCellId);
			}            

			int row = indexPath.Row;
			cell.TextLabel.Text = this._movieList[row];
			return cell;
		}
		public override nint RowsInSection(UITableView tableview, nint section)
		{
			return _movieList.Count;
		}

	}
}
