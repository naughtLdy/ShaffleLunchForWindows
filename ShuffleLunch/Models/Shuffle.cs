using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShuffleLunch.Models
{
	class Shuffle
	{
		private List<ShuffleResult> _shuffleResult;

		public Shuffle ()
		{
			_shuffleResult = new List<ShuffleResult>();
		}

		public bool shuffle (List<Desk> deskList, List<PersonAndDesk> personAndDeskList)
		{
			for (int i = 0; i < deskList.Count; i++)
			{
				if (deskList[i].name == "random")
				{
					continue;
				}
				var shuffleResult = new ShuffleResult();
				shuffleResult.deskName = deskList[i].name;
				shuffleResult.deskMax = deskList[i].max;
				shuffleResult.person = new List<Person>();
				_shuffleResult.Add(shuffleResult);
			}

			foreach (var personAndDesk in personAndDeskList)
			{
				if (personAndDesk.selectDesk != 0)
				{
					_shuffleResult[personAndDesk.selectDesk - 1].person.Add(
						new Person
						{
							name = personAndDesk.name,
							image = personAndDesk.image
						});
				}
			}

			int n = 0;
			foreach (var personAndDesk in personAndDeskList.OrderBy(_ => Guid.NewGuid()).ToList<PersonAndDesk>())
			{
				if (personAndDesk.selectDesk == 0) {
					if (_shuffleResult[n].person.Count >= _shuffleResult[n].deskMax)
					{
						n++;
					}
					_shuffleResult[n].person.Add(
						new Person
						{
							name = personAndDesk.name,
							image = personAndDesk.image
						});
				}
			}

			return true;
		}

		public List<ShuffleResult> Get()
		{
			return _shuffleResult;
		}
	}

	public class ShuffleResult
	{
		public string deskName { get; set; }
		public int deskMax { get; set; }
		public List<Person> person { get; set; }
	}
}
