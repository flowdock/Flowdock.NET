using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flowdock.Navigation;
namespace MoqaLate.Autogenerated
{
public partial class NavigationManagerMoqaLate : INavigationManager
{


// -------------- GoToFlows ------------


        private int _goToFlowsNumberOfTimesCalled;        

        

        public virtual bool GoToFlowsWasCalled()
{
   return _goToFlowsNumberOfTimesCalled > 0;
}


public virtual bool GoToFlowsWasCalled(int times)
{
   return _goToFlowsNumberOfTimesCalled == times;
}


public virtual int GoToFlowsTimesCalled()
{
   return _goToFlowsNumberOfTimesCalled;
}




        public void GoToFlows()
        {
            _goToFlowsNumberOfTimesCalled++;            

            
        }
}
}