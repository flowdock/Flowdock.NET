using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Client.Domain {
	public class User {
		public int Id { get; set; }
		public string Nick { get; set; }
		public string Email { get; set; }
		public Uri Avatar { get; set; }
		public string Status { get; set; }
		public bool Disabled { get; set; }
		public long LastActivity { get; set; }
		public long LastPing { get; set; }
	}
}