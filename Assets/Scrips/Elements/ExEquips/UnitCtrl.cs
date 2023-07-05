using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elements
{
    public partial class UnitCtrl
    {
        public Action<eStateIconType, bool> OnChangeStatePassiveEnable { get; set; }
    }
}
