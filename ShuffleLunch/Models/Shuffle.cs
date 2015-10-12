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

			// 座席ランダムの為の乱数生成
			// 一度のランダム生成だと規則性が出るので二段階で乱数を生成する
			var r = new Random();
			var r2 = new Random(r.Next());

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



			foreach (var personAndDesk in personAndDeskList)
			{
				if (personAndDesk.selectDesk == 0) {
					var n = r2.Next(1, deskList.Count);
					if (_shuffleResult[n - 1].person.Count >= _shuffleResult[n - 1].deskMax)
					{
						continue;
					}
					_shuffleResult[n - 1].person.Add(
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
