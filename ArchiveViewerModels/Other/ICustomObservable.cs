using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveViewerModels.Other
{
    public interface ICustomObservable
    {
        void Register(ICustomObserver observer);
    }
}
