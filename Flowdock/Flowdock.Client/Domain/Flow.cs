using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Client.Domain {
	public class Flow {
		public string Id { get; set; }
		public string Name { get; set; }
		public string Organization { get; set; }
		public int UnreadMentions { get; set; }
		public bool Open { get; set; }
		public bool Joined { get; set; }
		public Uri Url { get; set; }
		public Uri WebUrl { get; set; }
		public Uri JoinUrl { get; set; }
		public string AccessMode { get; set; }
		public List<User> Users { get; set; }
	}
}