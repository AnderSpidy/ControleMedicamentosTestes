using ControleMedicamentos.Dominio.ModuloMedicamento;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControleMedicamentos.Dominio.Tests.ModuloMedicamento
{
    [TestClass]
    public class MedicamentoTest
    {
        [TestMethod]
        public void escrever_Medicamento()
        {
            //arrange
            var medicamento = new Medicamento("Doril", "Tomou a dor Sumiu", "abc-235", System.DateTime.Today);
            //action
            string escritoMedicamento = medicamento.ToString();

            //assert

            Assert.AreEqual("Doril - Tomou a dor Sumiu - abc-235", escritoMedicamento);
        }
    }
}
