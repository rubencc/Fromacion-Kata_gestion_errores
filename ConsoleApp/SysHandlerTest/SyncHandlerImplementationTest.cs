using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp.Handler;
using ExternalService;

namespace SysHandler.Unit.Test
{
    [TestClass]
    public class SyncHandlerImplementationTest
    {

        private SyncHandlerImplementation handler;

        [TestInitialize]
        public void Setup_Test()
        {
            TimeService service = new TimeService();
            this.handler = new SyncHandlerImplementation(service);
        }

        [TestMethod]
        public void Obtener_fecha_para_los_rusos()
        {
            string russianDate = this.handler.GetTimeForРоскосмос();

            Assert.IsFalse(String.IsNullOrEmpty(russianDate), "Fecha nula o vacia");
        }

        [TestMethod]
        public void Obtener_fecha_para_los_japoneses()
        {
            string japanDate = this.handler.GetTimeForJAXA();

            Assert.IsFalse(String.IsNullOrEmpty(japanDate), "Fecha nula o vacia");
        }

        [TestMethod]
        public void Obtener_fecha_para_los_yankies()
        {
            string usaDate = this.handler.GetTimeForNASA();

            Assert.IsFalse(String.IsNullOrEmpty(usaDate), "Fecha nula o vacia");
        }

        [TestMethod]
        public void Obtener_fecha_para_los_europeos()
        {
            string usaDate = this.handler.GetTimeForESA();

            Assert.IsFalse(String.IsNullOrEmpty(usaDate), "Fecha nula o vacia");
        }

        [TestMethod]
        public void Obtener_objeto_datetime()
        {
            DateTime ? date = null;
            for(int i= 0; i < 10; i++)
                date = this.handler.GetDateTimeObject();

            Assert.IsNotNull(date, "Objeto nulo");
        }

        [TestMethod]
        public void Obtener_semanas_hasta_final_de_mision()
        {
            decimal weeks = this.handler.WeeksUntilFinishMission();

            Assert.IsTrue(weeks > 0, "Semanas igual a cero");
        }

        [TestMethod]
        public void Sincronizar()
        {
            bool result = this.handler.SyncWithExternalClock();

            long lastSyncTime = this.handler.LastSyncTime;

            Assert.IsTrue(result, "Fallo en la sincronizacion");
            Assert.IsTrue(lastSyncTime != 0, "No se ha podido sincronizar");
        }
    }
}
