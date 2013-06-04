using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Domain {
	public class Message {
		public int Id { get; set; }
		public string App { get; set; }
		public string Event { get; set; }
		public List<string> Tags { get; set; }
		public string Uuid { get; set; }
		public string Flow { get; set; }
		public string Content { get; set; }
		public long Sent { get; set; }
		public int User { get; set; }
	}
}
