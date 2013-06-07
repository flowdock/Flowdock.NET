using Flowdock.Client.Domain;

namespace Flowdock.Settings {
	public interface IAppSettings
	{
		string Username { get; set; }
		string Password { get; set; }
		Flow CurrentFlow { get; set; }
	}
}
