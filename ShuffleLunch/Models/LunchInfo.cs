﻿using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleLunch.Models
{
	class LunchInfo
	{
		private List<Desk> _deskList;
		private List<Person> _personList;

		public LunchInfo()
		{
			_deskList = new List<Desk>();
			_personList = new List<Person>();

		}

		public bool Get()
		{

			var dlg = new OpenFileDialog();
			dlg.DefaultExt = ".js";
			dlg.Filter = "js (.js)|*.js";

			var result = dlg.ShowDialog();
			if (result == true)
			{
				// Open document
				var filename = dlg.FileName;

				using (var stream = new FileStream(filename, FileMode.Open))
				using (var file = new StreamReader(stream))
				{
					var jsonData = JsonConvert.DeserializeObject<Rootobject>(file.ReadToEnd());
					_deskList = jsonData.desks.ToList<Desk>();
					_personList = jsonData.persons.ToList<Person>();
					return true;
				}
			}

			return false;
		}

		public List<Desk> DeskList()
		{
			return _deskList;
		}

		public List<Person> PersonList()
		{
			return _personList;
		}
	}

	public class Rootobject
	{
		public Desk[] desks { get; set; }
		public Person[] persons { get; set; }
	}

	public class Desk
	{
		public string name { get; set; }
		public int max { get; set; }
		public int min { get; set; }
	}

	public class Person
	{
		public string name { get; set; }
		public string desk { get; set; }
		public string image { get; set; }
	}
}