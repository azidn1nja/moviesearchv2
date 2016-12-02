using Lab1.Models;
using MovieDownload;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1.iOS.Controllers
{
    public static class ImageSaver
    {
        private static StorageClient storage = new StorageClient();
        private static ImageDownloader downloader = new ImageDownloader(storage);
        private static CancellationToken token = new CancellationToken();

        public static async Task getMoviePosters(List<MovieDTO> _movies)
        {
            string localpath;
            foreach (MovieDTO movie in _movies)
            {
                if (movie.PosterPath != null)
                {
                    localpath = downloader.LocalPathForFilename(movie.PosterPath);
                    if (!File.Exists(localpath))
                    {
                        await downloader.DownloadImage(movie.PosterPath, localpath, token);
                    }
                    movie.PosterPath = localpath;
                }
            }
        }
    }
}
