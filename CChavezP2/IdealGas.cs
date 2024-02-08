using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CChavezP2
{
    internal class IdealGas
    {
        //  The ideal gas class will have private fields for:
        //  mass, volume, temp, molecular weight, and pressure.             
        private double mass = 0;
        private double vol = 0;
        private double temp = 0;
        private double molecularWeight = 0;
        private double pressure = 0;


        //  Write public Get and Set methods for mass, volume, temp and molecular weight.
        public double GetMass(){return mass;}
        public void SetMass(double _mass) { this.mass = _mass; Calc(); }
        public double GetVolume(){ return vol; }
        public void SetVolume(double _vol) { this.vol = _vol; Calc(); }
        public double GetTemp() { return temp; }
        public void SetTemp(double _temp) { this.temp = _temp; Calc(); }
        public double GetMolecularWeight() { return molecularWeight; }
        public void SetMolecularWeight(double _molecularWeight) { this.molecularWeight = _molecularWeight; Calc(); }
        //  Write only a Get method for pressure.
        public double GetPresure() { return pressure; }

        //  Write a private void and parameterless Calc method in the class
        //  that will use the private fields for mass, volume, temp and molecular
        //  weight to calculate pressure. Call this Calc() method from each of
        //  the set methods for mass, volume, temp and molecular weight

        private void Calc()
        {
            double pressureOutput;
            double n = mass / molecularWeight; // number of moles calulation
            const double r = 8.3145; // constant value using metric units
            double kTemp = temp + 273.15; // convert Celcius to Kelvin for the calulation
            pressureOutput = n * r * kTemp / vol;
            this.pressure = pressureOutput;
        }

    }
}
