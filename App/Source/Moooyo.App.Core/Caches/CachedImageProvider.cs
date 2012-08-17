using System;
using MonoTouch;
using MonoTouch.Foundation;
using MonoTouch.CoreData;
using MonoTouch.UIKit;

namespace Moooyo.App.Core.Caches
{
	/// <summary>
	/// Cached image provider.
	/// </summary>
	public class CachedImageProvider
	{
		/// <summary>
		/// Gets the cached image.
		/// </summary>
		/// <returns>
		/// The cached image.
		/// </returns>
		/// <param name='filename'>
		/// Filename.
		/// </param>
		public static UIImage GetCachedImage(string filename)
		{
			CachedImage img = new CachedImage(filename);
			if (img.ImageData != null) return UIImage.LoadFromData(NSData.FromArray(img.ImageData));

			//downloading image
			CBB.NetworkingHelper.DownloadImage downloadImage = new CBB.NetworkingHelper.DownloadImage(
				Api.Photos.Get+"/"+filename
				);

			//save the image to cache table
			img = new CachedImage();
			img.Name = filename;
			img.TimeStamp = DateTime.Now.ToString();
			img.ImageData = downloadImage.GetImageStream();

			img.Save();
			downloadImage = null;

			return UIImage.LoadFromData(NSData.FromArray(img.ImageData));
		}
	}
}

