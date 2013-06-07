using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.Services.Progress {
	public interface IProgressService {
		void Show();
		void Show(string text);
		void Hide();

		bool IsVisible { get; }
	}
}
