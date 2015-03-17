using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalServiceInterface.Time
{
    public interface ITimeService
    {
        /// <summary>
        /// Indica si el servicio esta disponible en este momento.
        /// </summary>
        /// <returns></returns>
        bool ServiceAvailable();

        /// <summary>
        /// Devuelve la fecha en formato de ticks.
        /// </summary>
        /// <returns></returns>
        long GetTime();

        /// <summary>
        /// Devuelve la fecha UTC en formato en funcion de la cultura seleccionada.
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        string GetTime(System.Globalization.CultureInfo culture);

        /// <summary>
        /// Devuelve un obtejo DateTime
        /// </summary>
        /// <returns></returns>
        DateTime GetDateTime();

        /// <summary>
        /// Devuelve el retraso estimado segun la triangulacion con la lista de satelites. 
        /// </summary>
        /// <returns></returns>
        float[] GetLag(string[] sats);
    }
}
