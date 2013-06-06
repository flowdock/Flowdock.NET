using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Client.Domain {
	public class MessageContent {
		public string Title { get; set; }
		public string Text { get; set; }
		public string Type { get; set; }
		public string Description { get; set; }
		public List<string> Add { get; set; }
		public List<string> Remove { get; set; }
		public int Message { get; set; }
		public string UpdatedContent { get; set; }
		public long LastActivity { get; set; }
	}
}
