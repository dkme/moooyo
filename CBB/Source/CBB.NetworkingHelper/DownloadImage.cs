using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Net;

namespace CBB.NetworkingHelper
{
	public class DownloadImage {
	  private string imageUrl;
	  private byte[] imageBytes;
	  public DownloadImage(string imageUrl) {
	    	this.imageUrl = imageUrl;
			Download();
	  }
	  public void Download() {
	    try {
	      	WebClient client = new WebClient();
			imageBytes = client.DownloadData(imageUrl);
	    }
	    catch (Exception e) {
	      Console.WriteLine(e.Message);
	    }
	  }
	  public byte[] GetImageStream() {
	    return imageBytes;
	  }
	}
}

