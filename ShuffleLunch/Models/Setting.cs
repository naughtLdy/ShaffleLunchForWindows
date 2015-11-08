using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleLunch.Models
{
	class Setting
	{
		private static Setting m_Instance;

		private DefaultParameter _defaultParameter;

		public int FontSizeDesk
		{
			get { return _defaultParameter.Fontsize.desk; }
		}

		public int FontSizePerson
		{
			get { return _defaultParameter.Fontsize.person; }
		}

		public int ImageWidth
		{
			get { return _defaultParameter.Image.width; }
		}

		public int ImageHeight
		{
			get { return _defaultParameter.Image.height; }
		}

		public int ShuffleImageWidth
		{
			get { return _defaultParameter.ShuffleImage.width; }
		}

		public int ShuffleImageHeight
		{
			get { return _defaultParameter.ShuffleImage.height; }
		}

		private Setting()
		{
			_defaultParameter = new DefaultParameter();
        }

		public static Setting Instance
		{
			get { return m_Instance ?? (m_Instance = new Setting()); }
		}

		public bool Get()
		{
			var filename = "DefaultParameter.json";
			using (var stream = new FileStream(filename, FileMode.Open))
			using (var file = new StreamReader(stream))
			{
				_defaultParameter = JsonConvert.DeserializeObject<DefaultParameter>(file.ReadToEnd());
			}

			return true;
		}
	}

	public class DefaultParameter
	{
		public Fontsize Fontsize { get; set; }
		public Image Image { get; set; }
		public Shuffleimage ShuffleImage { get; set; }
	}

	public class Fontsize
	{
		public int desk { get; set; }
		public int person { get; set; }
	}

	public class Image
	{
		public int width { get; set; }
		public int height { get; set; }
	}

	public class Shuffleimage
	{
		public int width { get; set; }
		public int height { get; set; }
	}

}
