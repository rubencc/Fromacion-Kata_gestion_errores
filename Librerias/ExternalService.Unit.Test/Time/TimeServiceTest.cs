using ExternalService.Time;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExternalService.Unit.Test.Time
{
    [TestClass]
    public class TimeServiceTest
    {
        private TimeService service;

        [TestInitialize]
        public void Test_Setup()
        {
            this.service = new TimeService();
        }

        [TestMethod]
        public void Comprobar_si_el_servicio_esta_disponible()
        {
            bool result = this.service.ServiceAvailable();

            Assert.IsNotNull(result, "Nulo");
        }

        [TestMethod]
        public void Obtener_tiempo_en_formato_tick()
        {
            long time = this.service.GetTime();

            long timeNow = DateTime.Now.Ticks;

            Assert.IsNotNull(time, "Nulo");
            Assert.IsTrue(time <= timeNow, "El servicio se ejecuta en el futuro");
        }

        [TestMethod]
        public void Obtener_tiempo_en_funcion_de_la_cultura_fr()
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("fr-FR");
            Thread.CurrentThread.CurrentCulture = culture;

            string time = this.service.GetTime(culture);
            this.Assert_Culture(time, culture);

        }

        [TestMethod]
        public void Obtener_tiempo_en_funcion_de_la_cultura_es()
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("es-ES");
            Thread.CurrentThread.CurrentCulture = culture;

            string time = this.service.GetTime(culture);
            this.Assert_Culture(time, culture);

        }

        [TestMethod]
        public void Obtener_tiempo_en_funcion_de_la_cultura_en()
        {
            CultureInfo culture = CultureInfo.CreateSpecificCulture("en-EN");
            Thread.CurrentThread.CurrentCulture = culture;
            try
            {
                string time = this.service.GetTime(culture);
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex, "Nulo");
                Assert.IsInstanceOfType(ex, typeof(InvalidTimeZoneException));
            }

        }

        [TestMethod]
        public void Obtenet_objeto_datetime()
        {
            try
            {
                for (int i = 0; i < 3; i++)
                {
                    DateTime time = this.service.GetDateTime();
                    long timeNow = DateTime.Now.Ticks;
                    Assert.IsTrue(time.Ticks <= timeNow, "El servicio se ejecuta en el futuro");
                }
            }
            catch (Exception ex)
            {
                Assert.IsNotNull(ex, "Nulo");
                Assert.IsTrue(ex.GetType() == typeof(ArithmeticException));
            }
        }

        [TestMethod]
        public void Obtener_Lag_hispasat()
        {
            try
            {
                string[] sats = new string[] { "XRT1033", "hispasat", "pegaso", "astra" };
                float[] lag = this.service.GetLag(sats);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(UnauthorizedAccessException));
            }

        }

        [TestMethod]
        public void Obtener_Lag_dos_satelites()
        {
            try
            {
                string[] sats = new string[] { "pegaso", "astra" };
                float[] lag = this.service.GetLag(sats);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InsuficientConstelationMembersException));
            }

        }

        [TestMethod]
        public void Obtener_Lag_tres_satelites()
        {
            try
            {
                string[] sats = new string[] { "XRT1033", "pegaso", "astra" };
                for (int i = 0; i < 100; i++)
                    this.service.GetLag(sats);
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(InsufficientMemoryException));
            }

        }

        private void Assert_Culture(string time, CultureInfo culture)
        {
            Assert.IsNotNull(time, "Nulo");
            Assert.IsTrue(time != String.Empty, "La fecha esta vacia");

            DateTime date = DateTime.Parse(time, culture);
            long timeNow = DateTime.Now.Ticks;
            Assert.IsTrue(date.Ticks <= timeNow, "El servicio se ejecuta en el futuro");

        }
    }
}
