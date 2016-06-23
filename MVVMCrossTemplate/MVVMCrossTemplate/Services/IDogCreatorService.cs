using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCrossTemplate.Services
{
    public interface IDogCreatorService
    {
         Dog CreateNewDog(string extra = "");
    }
}
