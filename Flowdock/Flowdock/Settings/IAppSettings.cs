using Flowdock.Client.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Settings {
	public interface IAppSettings
	{
		string Username { get; set; }
		string Password { get; set; }
		Flow CurrentFlow { get; set; }
	}
}
