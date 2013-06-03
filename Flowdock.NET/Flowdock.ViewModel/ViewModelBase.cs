using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Flowdock.ViewModel {
	public abstract class ViewModelBase : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;
		
		public bool IsDesignTime {
			get {
				return DesignerProperties.IsInDesignTool;
			}
		}

		protected virtual void OnPropertyChanged<T>(params Expression<Func<T>>[] propertyExpressions) {
			if (PropertyChanged != null) {
				foreach (var propertyExpression in propertyExpressions) {
					var body = propertyExpression.Body as MemberExpression;
					if (body == null)
						throw new ArgumentException("'propertyExpression' should be a member expression");

					PropertyChanged(this, new PropertyChangedEventArgs(body.Member.Name));
				}
			}
		}
	}
}
