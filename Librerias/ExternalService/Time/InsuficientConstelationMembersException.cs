using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalService.Time
{
    public class InsuficientConstelationMembersException : Exception
    {

        public String Mensaje { get { return "La constelacion de satelites es insuficiente para triangular"; } }
        public InsuficientConstelationMembersException()
        {
        }
    }
}
