using System;
using System.Linq;

namespace Lab1.iOS
{
    using System.Collections.Generic;
    using Foundation;
    using UIKit;
    using Views;
    using Models;

    public class MovieListSource : UITableViewSource 
	{
		public readonly NSString MovieListCellId = new NSString("MovieListCell");
        private List<MovieDTO> _movieList;
		private Action<int> _onSelectedPerson;

		public MovieListSource(List <MovieDTO> movieList, Action<int> onSelectedPerson)
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
            cell.UpdateCell(_movieList[row].Title, _movieList[row].Year, getFirstThreeCastMembers(_movieList[row].Cast));
			return cell;
		}

        public string getFirstThreeCastMembers(List<string> castMembers)
        {
            string result = "";
            if (castMembers != null)
            {
                int count = Math.Min(castMembers.Count, 3);
                List<string> temp = (from member in castMembers
                                     select member).Take(count).ToList();
                result = string.Join(", ", temp);
            }
            return result;
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
